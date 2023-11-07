using Shared.DataFilter.Infrastructure;
using Shared.DataFilter.Presentation;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.DataFilter
{
    public static class ColumnSearchAggregateDTOExtension
    {
        public static ColumnSearchAggregateDTO GetFiltersFromSearchParams<TPresentationModel,TDBEntity>(this SearchParams searchParams)
        {
            List<Domain.Error.Error> errors = new List<Domain.Error.Error>();
            if (searchParams == null)
            {
                return null;
            }
            var presentationDtoProps = typeof(TPresentationModel).GetProperties()
                .Where(x => x.GetCustomAttribute<JsonPropertyNameAttribute>() != null);
            List<ColumnFilter> columnFilters = new List<ColumnFilter>();
            if (!String.IsNullOrEmpty(searchParams.filters))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(searchParams.filters));
                }
                catch (Exception ex)
                {
                    columnFilters = new List<ColumnFilter>();
                }
                //MANDANTORY-> Change ColumnFilter field+value to TDBEntity Structure, weil sich Filter nur auf die DTOs sich nach außen hin propagiert werden (eigentlich auf das JsonPropertyNameAttribute) beziehen aber eigentlich in der Linq Query zur Anwendung kommen, welche sich wiederum auf die Database Entities beschränkt.
                //TPresentationModel.PropertyName == TDBEntity.PropertyName, d.h. siehe unten: columnFilters[index].field=property.Name;
                foreach (var property in presentationDtoProps)
                {
                    var jsonProp = property.GetCustomAttribute<JsonPropertyNameAttribute>();
                    ColumnFilter foundInQueryFilter = columnFilters.Find(x => x.field == jsonProp.Name);
                    if(foundInQueryFilter != null)
                    {
                        int index=columnFilters.IndexOf(foundInQueryFilter);
                        if(index != -1)
                        {
                            columnFilters[index].field=property.Name;
                        }
                    }
                }
                for(int i=0; i<columnFilters.Count; i++)
                {
                    columnFilters[i].Op = columnFilters[i].GetOperatorFromOp();
                    if(columnFilters[i].IsInvalidOperator)
                    {
                        errors.Add(new Domain.Error.Error($"field: {columnFilters[i].field} with given operator {columnFilters[i].op} is not valid"));
                    }
                }
            }
            List<ColumnSorting> columnSorting = new List<ColumnSorting>();
            if (!String.IsNullOrEmpty(searchParams.order_by))
            {
                try
                {
                    columnSorting.AddRange(JsonSerializer.Deserialize<List<ColumnSorting>>(searchParams.order_by));
                }
                catch (Exception)
                {
                    columnSorting = new List<ColumnSorting>();
                }
                foreach (var property in presentationDtoProps)
                {
                    var jsonProp = property.GetCustomAttribute<JsonPropertyNameAttribute>();
                    ColumnSorting foundInQuerySorting = columnSorting.Find(x => x.field == jsonProp.Name);
                    if (foundInQuerySorting != null)
                    {
                        int index = columnSorting.IndexOf(foundInQuerySorting);
                        if (index != -1)
                        {
                            columnSorting[index].field = property.Name;
                        }
                    }
                }
            }




            return new ColumnSearchAggregateDTO(searchParams, columnFilters, columnSorting,errors);
        }
    }
}
