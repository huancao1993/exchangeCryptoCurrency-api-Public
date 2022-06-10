using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Trading.Authen.Api.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Trading.Authen.Api.Interfaces;
using System.Threading.Tasks;
using Trading.Authen.Api.Base;
using System.ComponentModel.DataAnnotations;
using Trading.Authen.Services.Dto.Users;
using Trading.Authen.Repository.Entity;

namespace Trading.Authen.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : MainController
    {
        private IUserService _userService;
        //private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async  Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var user = await _userService.Authenticate(model.Email, model.Password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = BuildClaimIdentity(user),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return MOk(new LoginOutputModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }
  
        [NonAction]
        public ClaimsIdentity BuildClaimIdentity(Users users)
        {
            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Email, users.Email));
            Claims.Add(new Claim(ClaimTypes.Name, users.Email));
            if (users.Email.ToLower()=="admin")
            {
                Claims.Add(new Claim(ClaimTypes.Role,"admin"));
                return new ClaimsIdentity(Claims);
            }
            foreach (var permission in users.Permissions)
            {
                Claims.Add(new Claim(ClaimTypes.Role,permission.RoleAction.Code));
            }
            return new ClaimsIdentity(Claims);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
           // map model to entity
           var user = _mapper.Map<Users>(model);
            if (!user.IdAvatar.HasValue)
            {
                user.IdAvatar = Guid.NewGuid();
            }
           // create user
           await _userService.Create(user, model.Password);
           return MOk();
        }
        /// <summary>
        /// người dùng thay đổi mật khẩu 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UserChangePassword")]
        public async Task<IActionResult> UserChangePassword([FromBody] ChangePasswordModel model)
        {
             var result =  await  _userService.ChangePassWord(User.Identity.Name, model.NewPassWord,model.OldPassWord);
             return MOk(result);
        }
        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UserUpdate")]
        public IActionResult Update([FromBody] UpdateUsersModel model)
        {
            var user = _mapper.Map<Users>(model);
            _userService.Update(user);
            return MOk();
        }
        [Authorize(Roles = PermissionBusiness.Roles.AccountRole.View)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return MOk(model);
        }

        [Authorize(Roles = PermissionBusiness.Roles.AccountRole.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            var model = _mapper.Map<UserModel>(user);
            return MOk(model);
        }
        /// <summary>
        /// Lấy thông tin tài khoản đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserinfo()
        {
            var user = await _userService.GetByName(User.Identity.Name);
            var model = _mapper.Map<UserModel>(user);
            return MOk(model);
        }

        /// <summary>
        /// Lấy thông tin tài khoản đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfoByUserName/{name}")]
        public async Task<IActionResult> GetUserInfoByUsername(string name)
        {
            var user = await _userService.GetByName(name);
            var model = _mapper.Map<UserModel>(user);
            return MOk(model);
        }
        [Authorize(Roles = PermissionBusiness.Roles.AccountRole.Delete)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return MOk();
        }

        //[Authorize(Roles = PermissionBusiness.Roles.AccountRole.Delete)]
        //[HttpDelete("Deletes")]
        //public async Task<IActionResult> Deletes([FromBody] DeleteModel deleteModel)
        //{
        //   var result =  await _userService.Deletes(deleteModel);
        //    return MOk(result);
        //}

        //[Authorize(Roles = PermissionBusiness.Roles.AccountRole.View)]
        //[HttpGet("Search")]
        //public async Task<IActionResult> Search([FromQuery] SearchUser model)
        //{
        //    //var query = _userService(model);
        //    return await ResultPagination<Users, UserModel>(model, query);
        //}
    }
}
