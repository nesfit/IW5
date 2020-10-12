using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CookBook.Api.Tests
{
    public class HelloControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HelloControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task SayYourName_Should_Return_My_Name()
        {
            // Act
            var response = await _client.GetAsync("api/hello");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("My name is Karel");

            //response.Content.ReadAsStringAsync().Result.Should().Be("My name is Karel"); // Shorter notation of the lines above.
        }
    }
}
