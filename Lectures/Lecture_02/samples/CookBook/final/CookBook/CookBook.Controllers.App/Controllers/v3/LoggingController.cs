using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace CookBook.Controllers.App.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class LoggingController : ControllerBase
    {
        private const string ApiOperationBaseName = "Logging";
        private readonly ILogger<LoggingController> logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(LogSomething))]
        public void LogSomething()
        {
            logger.LogInformation("Something");
            logger.LogWarning("Log something called in {0}", DateTime.Now);
        }
    }
}
