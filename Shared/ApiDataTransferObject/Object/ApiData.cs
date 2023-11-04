using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject.Object
{
    public class ApiData<T>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("value")]
        public T Value { get; set; }
        public ApiData()
        {

        }
    }
}
