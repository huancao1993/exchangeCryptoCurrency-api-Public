using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Authen.Repository.Entity;
using Trading.Authen.Services.Dto.Users;

namespace Trading.Authen.Api.Interfaces
{
    public interface IUserService
    {
            Task<Users> Authenticate(string username, string password);
            IEnumerable<Users> GetAll();
            Users GetById(int id);
            Task<UserModel> GetByIdAsync(int id);
            Task<Users> GetByName(string name);
            Task<Users> Create(Users user, string password);
            void Update(Users user, string password = null);
            void Delete(int id);
           // Task<bool> Deletes(DeleteModel model);
            Task<bool> ChangePassWord(string userName ,string newPassword,string oldPassWord);
         //   Task<bool> UpdatePassword(UpdatePassword model);

    }
}
