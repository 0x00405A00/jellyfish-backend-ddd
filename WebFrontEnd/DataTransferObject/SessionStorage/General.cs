using System.Text.Json.Serialization;
using WebFrontEnd.Const;

namespace WebFrontEnd.DataTransferObject.SessionStorage
{
    [Serializable]
    public class General : SessionPageDTO
    {
        [JsonPropertyName("last_route")]
        public string LastRoute { get; set; } = RouteConst.Routes.Home;
    }
}
