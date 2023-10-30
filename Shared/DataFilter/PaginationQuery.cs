using Shared.DataFilter.Infrastructure;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shared.DataFilter
{
    public static class PaginationQuery
    {
        public static IQueryable<T> CustomPagination<T>(this IQueryable<T> query, int? page = 0, int? pageSize = null)
        {
            return CustomPaginationExt(query, page, pageSize) ;
        }
        public static IQueryable<T> CustomPaginationExt<T>(IQueryable<T> query, int? page = 0, int? pageSize = null)
        {
            if (page != null)
            {
                query = query.Skip(((int)page - 1) * (int)pageSize);
            }
            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }
            return query;
        }
        public static IQueryable<T> GetSortedEntities<T>(this IQueryable<T> query, List<ColumnSorting> sortings)
        {
            foreach (var sorting in sortings)
            {
                if (!string.IsNullOrEmpty(sorting.field))
                {
                    var property = typeof(T).GetProperty(sorting.field);

                    if (property != null)
                    {
                        if (sorting.desc)
                        {
                            query = query.OrderByDescending(e => property.GetValue(e, null));
                        }
                        else
                        {
                            query = query.OrderBy(e => property.GetValue(e, null));
                        }
                    }
                }
            }

            return query;
        }
    }
}
