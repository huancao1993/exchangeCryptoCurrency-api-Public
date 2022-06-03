using System;
using System.Collections.Generic;
using System.Text;
using Trading.Repository.Entity;
using Trading.Repository.Interfaces;
using Trading.Repository.Repositories.Generics;

namespace Trading.Repository.Repositories
{
    public class RolesRepository : Repository<Roles>, IRolesRepository
    {
        public RolesRepository(TradingDbAuthenContext dbContext) : base(dbContext)
        {
        }
    }
}
