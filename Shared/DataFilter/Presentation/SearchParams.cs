namespace Shared.DataFilter.Presentation
{
    [Serializable]
    public class SearchParams : PaginationParams
    {
        public string? order_by { get; set; }
        public string? filters { get; set; }
    }
}
