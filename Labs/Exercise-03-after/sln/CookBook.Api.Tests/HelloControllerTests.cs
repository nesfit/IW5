using CookBook.Api.Tests.Fixtures;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CookBook.Api.Tests
{
    public class HelloControllerTests : IntegrationTest
    {
        public HelloControllerTests(ApiWebApplicationFactory fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task SayYourName_Should_Return_My_Name()
        {
            // Act
            var response = await _client.GetAsync("api/hello");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("My name is Jozin");

            //response.Content.ReadAsStringAsync().Result.Should().Be("My name is Karel"); // Shorter notation of the lines above.
        }
    }
}
