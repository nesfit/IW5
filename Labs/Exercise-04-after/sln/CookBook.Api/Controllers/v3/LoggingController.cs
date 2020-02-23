using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public LoggingController(ILogger<LoggingController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(LogSomething))]
        public void LogSomething()
        {
            logger.LogInformation("Something");
        }
    }
}
