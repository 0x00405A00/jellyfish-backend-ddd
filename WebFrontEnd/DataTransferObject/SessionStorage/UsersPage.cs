using System.Text.Json.Serialization;

namespace WebFrontEnd.DataTransferObject.SessionStorage
{
    [Serializable]
    public class UsersPage : SessionPageDTO
    {
        [JsonPropertyName("max_items_per_page")]
        public int MaxItemsPerPage { get; set; } = 10;
    }
}
