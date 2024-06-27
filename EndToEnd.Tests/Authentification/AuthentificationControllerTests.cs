using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.Testing;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;
using System.Text;
using System.Text.Json;
using WebApi;

namespace EndToEnd.Tests.Authentification
{
    public class AuthentificationControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthentificationControllerTests(CustomWebApplicationFactory<Startup> factory)
        {

            var options = new WebApplicationFactoryClientOptions()
            { 
                AllowAutoRedirect = false,
            };
            _client = factory.CreateClient(options);
            _client.DefaultRequestHeaders.Add("User-Agent", "NETHTTPCL");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            var baseAddr  = _client.BaseAddress;
        }

        [Fact]
        public async Task CreateAuth_ReturnsSuccessfullAuthentification()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/authentification/login");
            var authObject = new UserLoginDTO { Email ="root@localhost.local",Password= "root@localhost.local" };
            string authObjectJson = JsonSerializer.Serialize(authObject);
            var content = new StringContent(authObjectJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            //Act
            var response = await _client.SendAsync(request);

            //Assert 
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseAuthentification = JsonSerializer.Deserialize<AuthDTO>(responseString);

            Assert.NotNull(responseAuthentification);
            Assert.NotEmpty(responseAuthentification.Token);
            Assert.NotEmpty(responseAuthentification.RefreshToken);
            Assert.Equal(responseAuthentification.IsAuthentificated,true);
        }

        [Fact]
        public async Task CreateAuth_ReturnsFailureAuthentification()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/authentification/login");
            var authObject = new UserLoginDTO { Email = "iamnotregistered@web.de", Password = "root@localhost.local" };
            string authObjectJson = JsonSerializer.Serialize(authObject);
            var content = new StringContent(authObjectJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            //Act
            var response = await _client.SendAsync(request);

            //Assert 
            var responseString = await response.Content.ReadAsStringAsync();
            var responseAuthentification = JsonSerializer.Deserialize<ApiDataTransferObject<Result<AuthDTO>>>(responseString);

            Assert.NotNull(responseAuthentification);
            Assert.NotNull(responseAuthentification.Errors.Any(x=>x.Message == "username or password wrong"));
        }

        [Fact]
        public async Task Logout_ReturnsSuccessfullLogout()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/authentification/logout");
            var authObject = new UserLoginDTO { Email = "root@localhost.local", Password = "root@localhost.local" };
            string authObjectJson = JsonSerializer.Serialize(authObject);
            var content = new StringContent(authObjectJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            //Act
            var response = await _client.SendAsync(request);

            //Assert 
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseAuthentification = JsonSerializer.Deserialize<AuthDTO>(responseString);

            Assert.NotNull(responseAuthentification);
            Assert.NotEmpty(responseAuthentification.Token);
            Assert.NotEmpty(responseAuthentification.RefreshToken);
            Assert.Equal(responseAuthentification.IsAuthentificated, true);
        }

        [Fact]
        public async Task Logout_ReturnsFailureLogout()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/authentification/login");
            var authObject = new UserLoginDTO { Email = "iamnotregistered@web.de", Password = "root@localhost.local" };
            string authObjectJson = JsonSerializer.Serialize(authObject);
            var content = new StringContent(authObjectJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            //Act
            var response = await _client.SendAsync(request);

            //Assert 
            var responseString = await response.Content.ReadAsStringAsync();
            var responseAuthentification = JsonSerializer.Deserialize<ApiDataTransferObject<Result<AuthDTO>>>(responseString);

            Assert.NotNull(responseAuthentification);
            Assert.NotNull(responseAuthentification.Errors.Any(x => x.Message == "username or password wrong"));
        }

    }
}
