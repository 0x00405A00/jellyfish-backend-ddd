using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject.Object
{
    public class Pagination
    {

        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        [JsonPropertyName("next_page")]
        public int NextPage { get; set; }
        [JsonPropertyName("prev_page")]
        public int PrevPage { get; set; }
    }
}
