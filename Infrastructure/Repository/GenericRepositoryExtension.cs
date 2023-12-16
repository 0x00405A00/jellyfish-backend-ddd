using Domain.Primitives;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace Infrastructure.Repository
{
    public static class GenericRepositoryExtension
    {

        public static IQueryable<TEntity> ExpressionQuery<TEntity>(this DbSet<TEntity> dbSet, ColumnSearchAggregateDTO columnSearchAggregateDTO)
            where TEntity : Entity
        {
            var expression = columnSearchAggregateDTO.GetExpression<TEntity>(nameof(dbSet));
            var value = expression == null ?
                dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().AsQueryable().Where(expression);

            if (columnSearchAggregateDTO.HasPaginationParamsSet)
            {
                value = value.CustomPagination(columnSearchAggregateDTO.SearchParams.page_offset, columnSearchAggregateDTO.SearchParams.page_size);
            }
            return value;
        }
        public static List<TEntity> OrderQuery<TEntity>(this List<TEntity> data, ColumnSearchAggregateDTO columnSearchAggregateDTO)
            where TEntity : Entity
        {
            var value = data;
            if (columnSearchAggregateDTO.Sorting != null && columnSearchAggregateDTO.Sorting.Any())
            {
                value = value.GetSortedEntities<TEntity>(columnSearchAggregateDTO.Sorting);
            }
            return value;
        }
        public static IQueryable<TEntity> ExpressionQuery<TEntity>(this DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> expression = null)
            where TEntity : Entity
        {
            var value = expression == null ?
                dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().AsQueryable().Where(expression);
            return value;
        }

    }
}
