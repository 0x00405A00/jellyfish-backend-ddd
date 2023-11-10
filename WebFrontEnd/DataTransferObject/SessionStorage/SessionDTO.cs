using System.Text.Json.Serialization;
using WebFrontEnd.Const;

namespace WebFrontEnd.DataTransferObject.SessionStorage
{
    [Serializable]
    public class SessionDTO
    {
        [JsonPropertyName($"data-{RouteConst.Routes.Users}")]
        public UsersPage UsersPage { get; set; } = new UsersPage();
        [JsonPropertyName($"general")]
        public General General { get; set; } = new General();

    }
}
