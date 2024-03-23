using Domain.Primitives;
using Domain.Specification;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Abstractions
{
    public interface IGenericRepositorySearchExtended<TEntity>
        where TEntity : Entity

    {
        public Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(Expression<Func<TEntity, bool>> expression = null);
        public Task<ICollection<TEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
        public Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO);
    }
}