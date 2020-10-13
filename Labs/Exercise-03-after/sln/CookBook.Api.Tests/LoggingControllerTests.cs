using CookBook.Api.Controllers.v3;
using CookBook.Api.DateTimeProvider;
using CookBook.Api.Tests.Fixtures;
using CookBook.Api.Tests.Logger;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CookBook.Api.Tests
{
    public class LoggingControllerTests : IntegrationTest
    {
        public LoggingControllerTests(ApiWebApplicationFactory fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task LogSomething_Should_ResultInOK()
        {
            // Act
            var response = await _client.GetAsync("api/logging");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void LogSomething_Should_Log_Expected_Messages()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LoggingController>>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.Now).Returns(new DateTime(2020, 10, 13, 10, 0, 0));
            var sut = new LoggingController(loggerMock.Object, dateTimeProviderMock.Object);

            // Act
            sut.LogSomething();

            // Assert
            loggerMock.VerifyLogging("Something", LogLevel.Information)
                .VerifyLogging("Log something called in 10/13/2020 10:00:00", LogLevel.Warning);
        }
    }
}
