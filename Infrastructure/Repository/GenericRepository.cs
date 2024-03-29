﻿using Domain.Primitives;
using Domain.Specification;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal abstract class GenericRepository<TEntity> : Domain.Repository.IGenericRepository<TEntity> 
        where TEntity : Entity
    {

        public Expression<Func<TEntity, bool>> DefaultExpression = (entity) => 1 == 1;
        public DbSet<TEntity> DbSet { get; private set; }
        public ApplicationDbContext Context { get; private set; }

        protected GenericRepository(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
            DbSet = Context.Set<TEntity>();
        }
        #region Sync 
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Attach(TEntity entity)
        {
            DbSet.Attach(entity);
        }
        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public Task<int> CountMaxAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? DbSet.CountAsync() : DbSet.CountAsync(expression);
        }
        public int CountMax(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? DbSet.Count() : DbSet.Count(expression);
        }
        #endregion

        #region Async

        public IEnumerable<TEntity> GetBySpecification(ISpecification<TEntity> specification)
        {
            return DbSet.AsNoTracking()
                .Where(specification.IsSatisfiedBy)
                .ToList();
        }
        public async virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var value = await DbSet.AsNoTracking()
                                   .FirstOrDefaultAsync(expression);
            return value!;
        }

        public async virtual Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression)
        {
            var value = DbSet.ExpressionQuery(expression);

            return await value.ToListAsync();
        }
        public async virtual Task<ICollection<TEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var value = DbSet.ExpressionQuery(columnSearchAggregateDTO);

            return await value.ToListAsync();
        }

        public virtual async Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(Expression<Func<TEntity, bool>> expression = null)
        {
            int count = await CountMaxAsync();
            var value = await this.ListAsync(expression);
            var data = value;
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<TEntity>>(data,ref meta);
        }

        public virtual async Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            int count = await CountMaxAsync();
            var value = await ListAsync(columnSearchAggregateDTO);
            var data = value;
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<TEntity>>(data, ref meta);
        }

        #endregion
    }
}
