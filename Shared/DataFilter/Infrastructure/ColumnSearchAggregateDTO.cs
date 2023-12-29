using Domain.Errors;
using Shared.DataFilter.Presentation;
using Shared.Linq;
using System.Linq.Expressions;

namespace Shared.DataFilter.Infrastructure
{
    public class ColumnSearchAggregateDTO
    {
        public SearchParams SearchParams { get; private set; }=new SearchParams();
        public List<ColumnFilterGroup> FilterGroups { get; private set; } = new List<ColumnFilterGroup>();
        public List<ColumnSorting> Sorting { get; private set; } = new List<ColumnSorting>();
        public List<Error> Errors { get; private set; } = new List<Error>();
        public bool HasErrors=> Errors.Any();
        public bool HasPaginationParamsSet
        {
            get
            {
                return SearchParams.page_offset!=-1 && SearchParams.page_size != -1;
            }
        }
        public string ErrorsToString
        {
            get
            {
                string str = null;
                foreach (var err in Errors)
                {
                    str += $"{err.Message}"+ Environment.NewLine;
                }
                return str;
            }
        }
        public ColumnSearchAggregateDTO()
        {
                
        }
        public ColumnSearchAggregateDTO(SearchParams searchParams,List<ColumnFilterGroup> columnFilterGroups,List<ColumnSorting> columnSortings,List<Error> errors)
        {
            FilterGroups = columnFilterGroups;
            Sorting = columnSortings;
            SearchParams = searchParams;
            Errors = errors;    
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
            if (FilterGroups.Count > 0)
            {
                filters = CustomExpressionFilter<T>.CustomFilter(FilterGroups, className);
            }
            return filters;
        }

    }

}
