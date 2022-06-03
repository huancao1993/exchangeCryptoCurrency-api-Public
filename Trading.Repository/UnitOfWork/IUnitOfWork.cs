using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Trading.Repository.Interfaces;

namespace Trading.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IUsersRepository UsersRepository { get; }
        IRolesRepository RolesRepository { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
