using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Trading.Authen.Repository.Interfaces;

namespace Trading.Authen.Repository.UnitOfWork
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
