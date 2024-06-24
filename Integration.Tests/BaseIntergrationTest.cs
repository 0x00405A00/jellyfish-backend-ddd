using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using WebApi;

namespace Integration.Tests
{
    public abstract class BaseIntergrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        protected ApplicationDbContext DBContext { get; }
        protected ISender Sender { get; }
        protected BaseIntergrationTest(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
        {
            _serviceScope = customWebApplicationFactory.Services.CreateScope();
            DBContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Sender = _serviceScope.ServiceProvider.GetRequiredService<ISender>();
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
