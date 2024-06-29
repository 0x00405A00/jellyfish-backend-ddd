using Shared.DataTransferObject;
using System.Text;
using System.Text.Json;

namespace EndToEnd.Tests.Shared
{
    public class AuthHelper
    {
        private readonly HttpClient _client;
        public record Authentification(string Token,string RefreshToken);

        public AuthHelper(HttpClient client)
        {
            _client = client;
        }

        public async Task<Authentification> GetTokenAsync(string authEndpoint, string email,string password)
        {
            var request = new HttpRequestMessage().AddUri(HttpMethod.Post,authEndpoint).PrepareRequest();
            var authObject = new { email = email, password = password };
            var content = new StringContent(JsonSerializer.Serialize(authObject), Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseAuthentification = JsonSerializer.Deserialize<AuthDTO>(responseString);
            var authResponseObject = new Authentification(responseAuthentification.Token, responseAuthentification.RefreshToken);
            return authResponseObject;
        }
    }
    public static class AuthHelperExtension
    {
        public static async Task<AuthHelper.Authentification> GetTokenAsync(this HttpClient client,string authEndpoint,string email,string password)
        {
            return await new AuthHelper(client).GetTokenAsync(authEndpoint, email, password);
        }
    }
}
