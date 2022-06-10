using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Trading.Authen.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Trading.Authen.Repository.UnitOfWork;
using Trading.Authen.Repository.Entity;
using Trading.Authen.Services.Helpers;
using Trading.Authen.Services.Dto.Users;
using AutoMapper;

namespace Trading.Authen.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly  IMapper _mapper;
        //private IEmailService _emailService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Users> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new AuthenException("Username or password is incorrect");

            var user = await _unitOfWork.UsersRepository.Table.Include(p=>p.UserHasRoleGroups).Include(p => p.Permissions).ThenInclude(p=>p.RoleAction).SingleOrDefaultAsync(x => x.Email == email);

            // check if username exists
            if (user == null)
                throw new AuthenException("Username or password is incorrect");

            if (user.Status == 0)
                throw new AuthenException("Account in active");

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenException("Username or password is incorrect"); 

            // authentication successful
            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            return _unitOfWork.UsersRepository.GetAll();
        }

        public Users GetById(int id)
        {
            return  _unitOfWork.UsersRepository.GetEnitityById(id);
        }

        public async Task<Users> Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AuthenException("Password is required");

            if (await _unitOfWork.UsersRepository.Table.FirstOrDefaultAsync(x => x.Email == user.Email) !=null)
                throw new AuthenException("Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _unitOfWork.UsersRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }
        public void Update(Users userParam, string password = null)
        {
            var user =  _unitOfWork.UsersRepository.GetEnitityById(userParam.Id);

            if (user == null)
                throw new AuthenException("User not found");

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;
           
            if (!string.IsNullOrWhiteSpace(userParam.Phone))
            {
                user.Phone = userParam.Phone;
            }
            user.Status = userParam.Status;
            _unitOfWork.Complete();
        }

        

        public void Delete(int id)
        {
            var user = _unitOfWork.UsersRepository.GetEnitityById(id);
          
            if (user != null)
            {
                if (user.Status == 1)throw new AuthenException("Account active is not delete");

                user.IsDeleted = true;
                _unitOfWork.Complete();
            }
        }
        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        public async Task<bool> ChangePassWord(string email, string password,string oldPassWord)
        {
            var user = await _unitOfWork.UsersRepository.Table.SingleOrDefaultAsync(p => p.Email == email);
            if (user==null)
            {
                throw new AuthenException("Username or password is incorrect");
            }
            // check if password is correct
            if (!VerifyPasswordHash(oldPassWord, user.PasswordHash, user.PasswordSalt))
                throw new AuthenException("Username or password is incorrect");

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _unitOfWork.Complete();
            return true;
        }
        public async Task<Users> GetByName(string email)
        {
            var user = await _unitOfWork.UsersRepository.Table.AsNoTracking().SingleOrDefaultAsync(p => p.Email == email);
            return user;
        }

        public async Task<bool> UpdatePassword(UpdatePassword model)
        {
            var user = await _unitOfWork.UsersRepository.Table.SingleOrDefaultAsync(p => p.Email == model.Email);
            if (user==null)
            {
                throw new AuthenException("Username or password is incorrect");
            }
            //// check if password is correct
            //if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            //    throw new AuthenException("Username or password is incorrect");
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _unitOfWork.Complete();
            return true;
        }
        public async Task<bool> ForgotPassword(ForgotPasswordModel model)
        {
            using (var tran = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var user = await _unitOfWork.UsersRepository.Table.SingleOrDefaultAsync(p => p.Email == model.Email);
                    if (user == null)
                    {
                        throw new AuthenException("Username or password is incorrect");
                    }
                    Random random = new Random();
                    string password = "ERP" + random.Next(100000,999999).ToString();
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    await _unitOfWork.CompleteAsync();
                    //var filePath = Path.GetFullPath("Template");
                   // string pathfile = $"{filePath}{(RuntimeEnvironment.OperatingSystemPlatform == Platform.Linux ? "/" : "\\")}{"ForgotPassword.html"}";
                    //if (System.IO.File.Exists(pathfile))
                    //{
                    //    password = File.ReadAllText(pathfile).Replace("{Pass}", password);
                    //}
                    //await _emailService.Send(model.Email, "ForgotPassword", password);
                    await tran.CommitAsync();
                }
                catch(Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
            return true;
        }

        public IQueryable<Users> Search(SearchUser model)
        {
            var user =  _unitOfWork.UsersRepository.Table.Include(p => p.UserHasRoleGroups).ThenInclude(p => p.RoleGroup).AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                user=  user.Where(p => p.Email.Contains(model.Keyword));
            }
            if (model.Status.HasValue)
            {
                user = user.Where(p => p.Status == model.Status);
            }
            return user;
        }
        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user =  await _unitOfWork.UsersRepository.Table.Include(p=>p.UserHasRoleGroups).ThenInclude(p=>p.RoleGroup).SingleOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<UserModel>(user);
        }
       
       
    }
}