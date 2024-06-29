using Domain.ValueObjects;
using EndToEnd.Tests.Shared;
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
            _client = factory.CreateHttpClient(Consts.API.V1.RelativePathBaseAddress);
        }

        [Fact]
        public async Task CreateAuth_ReturnsSuccessfullAuthentification()
        {
            //Arrange
            var request = new HttpRequestMessage().AddUri(HttpMethod.Post, "authentification/login").PrepareRequest();
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
            var request = new HttpRequestMessage().AddUri(HttpMethod.Post, "authentification/login").PrepareRequest();
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
            string email = "root@localhost.local";
            string password = "root@localhost.local";
            var auth = await _client.GetTokenAsync("authentification/login", email, password);

            var request = new HttpRequestMessage().AddUri(HttpMethod.Post, "authentification/logout").PrepareRequest(auth.Token);

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

    }
}
