using Shared.DataFilter.Infrastructure;
using Shared.DataFilter.Presentation;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.DataFilter
{
    public static class ColumnSearchAggregateDTOExtension
    {
        public static ColumnSearchAggregateDTO GetFiltersFromSearchParams<TPresentationModel,TEntity>(this SearchParams searchParams)
        {
            List<Domain.Error.Error> errors = new List<Domain.Error.Error>();
            if (searchParams == null)
            {
                return new ColumnSearchAggregateDTO();
            }
            var presentationDtoProps = typeof(TPresentationModel).GetProperties()
                .Where(x => x.GetCustomAttribute<JsonPropertyNameAttribute>() != null);

            List<ColumnFilterGroup> columnFilterGroups = new List<ColumnFilterGroup>();
            JsonSerializerOptions? serilizerOptions = new JsonSerializerOptions { MaxDepth = 10, WriteIndented = true };
            if (!String.IsNullOrEmpty(searchParams.filters))
            {
                try
                {
                    List<ColumnFilterGroup> value = JsonSerializer.Deserialize<List<ColumnFilterGroup>>(searchParams.filters,serilizerOptions);
                    if(value is null || value.Count < 1 || value.First().Filters.Count < 1)
                    {
                        throw new Exception("without groups request");//e.g. '[{"field":"email","value":"francesc3o2@hotmail.com"}]' insted of '[{"group":"and","filters":[{"field":"deleted_time","value":null,"op":"eq"}]},{"group":"and","filters":[{"field":"email","value":"genevieve56@gmail.com","op":"eq"}]}]'
                    }
                    columnFilterGroups.AddRange(value);
                }
                catch (Exception _ex)
                {
                    try
                    {
                        List<ColumnFilter> columnFilters = new List<ColumnFilter>();
                        var value = JsonSerializer.Deserialize<List<ColumnFilter>>(searchParams.filters);
                        columnFilters.AddRange(value);
                        columnFilterGroups.Add(new ColumnFilterGroup(columnFilters, OperatorGroupNames.AND));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                bool validDataInGroups =columnFilterGroups.Where(x=>x.Filters.Any()).Any();
                if (!validDataInGroups)
                {
                    return new ColumnSearchAggregateDTO();
                }
                //MANDANTORY-> Change ColumnFilter field+value to TDBEntity Structure, weil sich Filter nur auf die DTOs sich nach außen hin propagiert werden (eigentlich auf das JsonPropertyNameAttribute) beziehen aber eigentlich in der Linq Query zur Anwendung kommen, welche sich wiederum auf die Database Entities beschränkt.
                //TPresentationModel.PropertyName == TDBEntity.PropertyName, d.h. siehe unten: columnFilters[index].field=property.Name;
                for(int f=0;f< columnFilterGroups.Count;f++)
                {
                    bool foundFilterFieldNameInDtoAsProp = false;
                    var columnFilterGroup= columnFilterGroups[f];
                    columnFilterGroups[f].Group = columnFilterGroups[f].Group.ToLower();
                    columnFilterGroups[f].OpGroup = columnFilterGroups[f].GetOperatorFromOp();
                    foreach (var property in presentationDtoProps)
                    {
                        var jsonProp = property.GetCustomAttribute<JsonPropertyNameAttribute>();
                        ColumnFilter foundInQueryFilter = columnFilterGroup.Filters.Find(x => x.field == jsonProp.Name);
                        if (foundInQueryFilter != null)
                        {
                            int index = columnFilterGroup.Filters.IndexOf(foundInQueryFilter);
                            if (index != -1)
                            {
                                columnFilterGroup.Filters[index].field = property.Name;
                                columnFilterGroup.Filters[index].FoundFieldInDto = true;
                            }
                        }
                    }

                    for (int i = 0; i < columnFilterGroup.Filters.Count; i++)
                    {
                        var filter = columnFilterGroup.Filters[i];
                        filter.Op = filter.GetOperatorFromOp();
                        if (filter.IsInvalidOperator)
                        {
                            errors.Add(new Domain.Error.Error($"field: {filter.field} with given operator {columnFilterGroup.Filters[i].op} is not valid"));
                        }
                        if(!filter.FoundFieldInDto)
                        {
                            errors.Add(new Domain.Error.Error($"field with given name '{filter.field}' is not existing"));
                        }
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
                            columnSorting[index].FoundFieldInDto = true;
                        }
                    }
                }
                for (int i = 0; i < columnSorting.Count; i++)
                {
                    var sorting = columnSorting[i];
                    if (!sorting.FoundFieldInDto)
                    {
                        errors.Add(new Domain.Error.Error($"field with given name '{sorting.field}' is not existing"));
                    }
                }
            }

            return new ColumnSearchAggregateDTO(searchParams, columnFilterGroups, columnSorting,errors);
        }
    }
}
