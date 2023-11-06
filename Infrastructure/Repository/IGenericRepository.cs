﻿using Domain.Primitives;
using Infrastructure.Abstractions;
using Infrastructure.Repository.Primitives;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<TDbEntity>
        where TDbEntity : DatabaseEntityModel

    {
        public void Add(TDbEntity entity);
        public void Attach(TDbEntity entity);
        public void Remove(TDbEntity entity);
        public void Update(TDbEntity entity);
        public TDbEntity Get(Expression<Func<TDbEntity, bool>> expression);
        public ICollection<TDbEntity> List(Expression<Func<TDbEntity, bool>> expression = null);
        public ICollection<TDbEntity> List(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public int CountMax();

        public Task AddAsync(TDbEntity entity);
        public void RemoveAsync(TDbEntity entity);
        public void UpdateAsync(TDbEntity entity);
        public Task<TDbEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression);
        public Task<ICollection<TDbEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression = null);
        public Task<ICollection<TDbEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public Task<int> CountMaxAsync();
    }
    public interface IGenericRepository<TEntity,TDbEntity>
        where TEntity : Entity
        where TDbEntity : DatabaseEntityModel

    {
        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public TEntity Get(Expression<Func<TDbEntity, bool>> expression);
        public ICollection<TEntity> List(Expression<Func<TDbEntity, bool>> expression = null);
        public ICollection<TEntity> List(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public int CountMax();

        public Task AddAsync(TEntity entity);
        public void RemoveAsync(TEntity entity);
        public void UpdateAsync(TEntity entity);
        public Task<TEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression);
        public Task<ICollection<TEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression = null);
        public Task<ICollection<TEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(Expression<Func<TDbEntity, bool>> expression = null);
        public Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public Task<int> CountMaxAsync();
    }
}