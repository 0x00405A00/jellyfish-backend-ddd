using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject.Object
{
    public class ApiMeta
    {
        [JsonPropertyName("copyright")]
        public string Copyright {  get; set; }
        [JsonPropertyName("authors")]
        public List<string> Authors { get; set; }
        [JsonPropertyName("pagination")]
        public Pagination? Pagination { get; set; }
        public ApiMeta() 
        {

        }
        public static ApiMeta Create()
        {
            return new ApiMeta() {
                Authors = new List<string>
                {
                    "Joel Mika Roos"
                },
                Copyright = "Joel Mika Roos (R) 2023"
            };
        }
    }
}
