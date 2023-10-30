using Shared.DataFilter.Presentation;
using Shared.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;
using System.Xml;

namespace Shared.DataFilter.Infrastructure
{
    public class ColumnSearchAggregateDTO
    {
        public SearchParams SearchParams { get; private set; }=new SearchParams();
        public List<ColumnFilter> Filters { get; private set; } = new List<ColumnFilter>();
        public List<ColumnSorting> Sorting { get; private set; } = new List<ColumnSorting>();
        public bool HasPaginationParamsSet
        {
            get
            {
                return SearchParams.page_offset!=-1 || SearchParams.page_size != -1;
            }
        }
        public ColumnSearchAggregateDTO(SearchParams searchParams,List<ColumnFilter> columnFilters,List<ColumnSorting> columnSortings)
        {
            Filters = columnFilters;
            Sorting = columnSortings;
            SearchParams = searchParams;
        }
        public Expression<Func<T, bool>> GetExpression<T>(string className)
            where T : class
        {

            Expression<Func<T, bool>> filters = null;
            //First, we are checking our SearchTerm. If it contains information we are creating a filter.
            /*var searchTerm = "";
            if (!string.IsNullOrEmpty(searchParam.SearchTerm))
            {
                searchTerm = searchParam.SearchTerm.Trim().ToLower();
                filters = x => x.user_name.ToLower().Contains(searchTerm);
            }*/
            // Then we are overwriting a filter if columnFilters has data.
            if (Filters.Count > 0)
            {
                filters = CustomExpressionFilter<T>.CustomFilter(Filters, className);
            }
            return filters;
        }

    }

}
