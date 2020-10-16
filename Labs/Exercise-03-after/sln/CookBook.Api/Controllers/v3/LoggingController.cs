using CookBook.Api.DateTimeProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace CookBook.Api.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class LoggingController : ControllerBase
    {
        private const string ApiOperationBaseName = "Logging";
        private readonly ILogger<LoggingController> logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LoggingController(ILogger<LoggingController> logger, IDateTimeProvider dateTimeProvider)
        {
            this.logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(LogSomething))]
        public void LogSomething()
        {
            logger.LogInformation("Something");
            logger.LogWarning("Log something called in {0}", _dateTimeProvider.Now);
        }
    }
}
