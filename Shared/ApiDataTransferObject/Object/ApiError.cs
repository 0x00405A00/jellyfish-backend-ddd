using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject.Object
{
    public class ApiError
    {
        [JsonPropertyName("title")]
        public string Title {  get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("details")]
        public string Details {  get; set; }
        [JsonPropertyName("code")]
        public int Code {  get; set; }
        public ApiError()
        {

        }
        public ApiError(string title,string message,string details,int code)
        {
            Title = title;
            Message = message;
            Details = details;
            Code = code;
        }
    }
}
