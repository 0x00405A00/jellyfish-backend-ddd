using Domain.Primitives;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public static class GenericRepositoryExtension
    {

        public static IQueryable<TDbEntity> ExpressionQuery<TDbEntity>(this DbSet<TDbEntity> dbSet, ColumnSearchAggregateDTO columnSearchAggregateDTO)
            where TDbEntity : DatabaseEntityModel
        {
            var expression = columnSearchAggregateDTO.GetExpression<TDbEntity>(nameof(dbSet));
            var value = expression == null ?
                dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().AsQueryable().Where(expression);

            if (columnSearchAggregateDTO.HasPaginationParamsSet)
            {
                value = value.CustomPagination(columnSearchAggregateDTO.SearchParams.page_offset, columnSearchAggregateDTO.SearchParams.page_size);
            }
            if(columnSearchAggregateDTO.Sorting!=null&&columnSearchAggregateDTO.Sorting.Any())
            {
                value = value.GetSortedEntities<TDbEntity>(columnSearchAggregateDTO.Sorting);
            }
            return value;
        }
        public static IQueryable<TDbEntity> ExpressionQuery<TDbEntity>(this DbSet<TDbEntity> dbSet, Expression<Func<TDbEntity, bool>> expression = null)
            where TDbEntity : DatabaseEntityModel
        {
            var value = expression == null ?
                dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().AsQueryable().Where(expression);
            return value;
        }

    }
}
