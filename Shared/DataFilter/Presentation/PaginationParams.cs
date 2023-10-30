using System.Text.Json.Serialization;

namespace Shared.DataFilter.Presentation
{
    [Serializable]
    public class PaginationParams
    {
        [JsonIgnore]
        public const int MaxPageSize = 100;

        private int _pageSize = -1;

        public int page_offset { get; set; } = -1;
        public int page_size { get { return _pageSize; } set { _pageSize = value > MaxPageSize ? MaxPageSize : value; } }
    }
}
