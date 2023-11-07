namespace Shared.DataFilter.Presentation
{
    [Serializable]
    public class SearchParams : PaginationParams
    {
        public string? order_by { get; set; }
        public string? filters { get; set; }//explicit and condition + equal/contains by string
        public string? and_filters { get; set; }//explicit and condition + equal/contains by string
    }
}
