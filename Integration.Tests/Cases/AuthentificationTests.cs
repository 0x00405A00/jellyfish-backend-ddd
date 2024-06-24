using Application.CQS.Auth.Command.CreateAuth;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi;

namespace Integration.Tests.Cases
{
    public class AuthentificationTests : BaseIntergrationTest
    {
        public AuthentificationTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory) : base(customWebApplicationFactory)
        {

        }
        [Fact]
        public async Task CreateAuthentification_SuccessfullyAuthentification()
        {
            //Arrange
            var command = new CreateAuthCommand("root@localhost.local", "root@localhost.local", IPAddress.Parse("127.0.0.1"), 1234, IPAddress.Parse("127.0.0.1"), 12345, "NOONE");

            //Act
            var response = await Sender.Send(command);

            //Assert
            var token = response.Value.Token;
            var authFromDb = await DBContext.Auths.Where(x => x.Token == token).ToListAsync();
            Assert.NotNull(authFromDb);
        }
        [Fact]
        public async Task CreateAuthentification_FailureAuthentification()
        {
            //Arrange
            var command = new CreateAuthCommand("idontknow@web.de", "root@localhost.local", IPAddress.Parse("127.0.0.1"), 1234, IPAddress.Parse("127.0.0.1"), 12345, "NOONE");

            //Act
            var response = await Sender.Send(command);

            //Assert
            var token = response.Value?.Token;
            var authFromDb = await DBContext.Auths.Where(x => x.Token == token)?.ToListAsync();
            Assert.Empty(authFromDb);
            Assert.NotNull(response.Errors.Any(x => x.Message == "username or password wrong"));
        }
    }
}
