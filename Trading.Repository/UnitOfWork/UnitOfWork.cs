
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Trading.Repository.Entity;
using Trading.Repository.Interfaces;
using Trading.Repository.Repositories;

namespace Trading.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradingDbAuthenContext _dbContext;
        private bool _disposed;
        
        public UnitOfWork(TradingDbAuthenContext dbContext)
        {
            _dbContext = dbContext;
            RolesRepository = new RolesRepository(_dbContext);
            UsersRepository = new UsersRepository(_dbContext);
            _disposed = false;
        }

        public IUsersRepository UsersRepository { get; private set; }

        public IRolesRepository RolesRepository { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }


        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async System.Threading.Tasks.Task DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            this._disposed = true;
        }

        protected virtual async System.Threading.Tasks.Task DisposeAsync(bool disposing)
        {
            if (!this._disposed)
            {

                if (disposing)
                {
                    await _dbContext.DisposeAsync();
                }

                this._disposed = true;
            }
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
          return  _dbContext.Database.BeginTransaction();
        }

        public virtual Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }
    }
}
