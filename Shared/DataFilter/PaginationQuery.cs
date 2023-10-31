using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;

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
        public static List<T> GetSortedEntities<T>(this List<T> data, List<ColumnSorting> sortings)
        {

            var queryAsList = data;

            return OrderData(queryAsList,sortings);
        }
        public static List<T> OrderData<T>(List<T> queryAsList, List<ColumnSorting> sortings)
        {

            foreach (var sorting in sortings)
            {
                if (!string.IsNullOrEmpty(sorting.field))
                {
                    var propertyInfo = typeof(T).GetProperty(sorting.field);

                    if (propertyInfo != null)
                    {
                        if (sorting.desc)
                        {
                            queryAsList = queryAsList.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
                        }
                        else
                        {
                            queryAsList = queryAsList.OrderBy(e => propertyInfo.GetValue(e, null)).ToList();
                        }
                    }
                }
            }
            return queryAsList;
        }
    }
}
