using Domain.Primitives;
using Infrastructure.Repository.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<TEntity>
        where TEntity : Entity

    {
        public DbSet<TEntity> DbSet { get; }
        protected ApplicationDbContext Context { get; }

        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public void Attach(TEntity entity);

        public int CountMax(Expression<Func<TEntity, bool>> expression = null);
        public Task<int> CountMaxAsync(Expression<Func<TEntity, bool>> expression = null);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        public Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression = null);
        public Task<ICollection<TEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(Expression<Func<TEntity, bool>> expression = null);
        public Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO);

        public void PublishDomainEvents(TEntity entity, IMediator mediator);

    }
}