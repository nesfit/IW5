using CookBook.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Api.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class HelloController : ControllerBase
    {
        private readonly IServerService _serverService;

        public HelloController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpGet]
        public ActionResult<string> SayYourName()
        {
            var name = _serverService.GetServerName();

            return Ok($"My name is {name}");
        }
    }
}