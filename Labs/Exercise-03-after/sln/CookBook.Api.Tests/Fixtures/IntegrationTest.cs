using CookBook.Api.Options;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Xunit;

namespace CookBook.Api.Tests.Fixtures
{
    public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _configuration = new ConfigurationBuilder()
                  .AddJsonFile("integrationsettings.json")
                  .Build();

            // You can use configuration from integrationsettings.json here.
            // Or you can use constructor injection with parameter IOptions<ServerNameOptions> in a class located in test project
            // that will be populated with values from integrationsettings.json file.
            var serverNameOptions = _configuration.GetSection("ServerName").Get<ServerNameOptions>();

            // If needed, reset the DB here.
        }
    }
}
