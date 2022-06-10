using System;
using System.Collections.Generic;
using System.Text;
using Trading.Authen.Repository.Entity;
using Trading.Authen.Repository.Interfaces;
using Trading.Authen.Repository.Repositories.Generics;

namespace Trading.Authen.Repository.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(TradingDbAuthenContext dbContext) : base(dbContext)
        {
        }
    }
}
