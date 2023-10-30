using System.Text.Json.Serialization;

namespace Shared.DataFilter.Presentation
{
    [Serializable]
    public class SearchParams : PaginationParams
    {
        public string? order_by { get; set; }
        public string? search_term { get; set; }
        public string? filters { get; set; }
    }
}
