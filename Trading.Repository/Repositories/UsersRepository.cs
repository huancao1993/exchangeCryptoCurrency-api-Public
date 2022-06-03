using System;
using System.Collections.Generic;
using System.Text;
using Trading.Repository.Entity;
using Trading.Repository.Interfaces;
using Trading.Repository.Repositories.Generics;

namespace Trading.Repository.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(TradingDbAuthenContext dbContext) : base(dbContext)
        {
        }
    }
}
