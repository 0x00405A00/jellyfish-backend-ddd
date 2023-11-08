using Shared.DataFilter.Infrastructure;
using System.Text.Json.Serialization;

namespace Shared.DataFilter.Presentation
{
    [Serializable]
    public class SearchParams : PaginationParams
    {
        [JsonPropertyName("order_by")]
        public string? order_by { get; set; }
        [JsonPropertyName("filters")]
        public string? filters { get; set; }//explicit and condition + equal/contains by string
    }
    [Serializable]
    public class SearchParamsBody : PaginationParams
    {
        [JsonPropertyName("order_by")]
        public List<ColumnSorting>? order_by { get; set; }
        [JsonPropertyName("filters")]
        public List<ColumnFilterGroup>? filters { get; set; }//explicit and condition + equal/contains by string

        [JsonIgnore]
        public SearchParams SearchParams { get; set; } 
    }
}
