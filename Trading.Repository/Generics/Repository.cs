using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trading.Repository.Entity;

namespace Trading.Repository.Repositories.Generics
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly TradingDbAuthenContext _dbContext;

        public IQueryable<TEntity> Table => _dbContext.Set<TEntity>();

        public Repository(TradingDbAuthenContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _dbContext.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        public virtual async Task<TEntity> GetEnitityByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity GetEnitityById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity GetEnitityById(long id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties = null)
        {
            return _dbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return GetQueryable(orderBy: orderBy, includeProperties: includeProperties, skip: skip, take: take);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return await GetQueryableAsync(orderBy: orderBy, includeProperties: includeProperties, skip: skip, take: take);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            return GetQueryable(filter: filter, orderBy: orderBy, includeProperties: includeProperties).FirstOrDefault();
        }

        //public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        //{
        //    return GetQueryableAsync(filter: filter, orderBy: orderBy, includeProperties: includeProperties).FirstOrDefault();
        //}

        protected virtual IEnumerable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties ??= string.Empty;
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.AsEnumerable();
        }

        protected virtual Task<IEnumerable<TEntity>> GetQueryableAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties ??= string.Empty;
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return (Task<IEnumerable<TEntity>>)query.AsEnumerable();
        }

    }
}
