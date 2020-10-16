using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CookBook.Api.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class HelloController : ControllerBase
    {
        private readonly IOptions<ServerNameOptions> serverNameOptions;

        public HelloController(IOptions<ServerNameOptions> serverNameOptions)
        {
            this.serverNameOptions = serverNameOptions;
        }

        [HttpGet]
        public ActionResult<string> SayYourName()
        {
            var name = serverNameOptions.Value.Name;

            return Ok($"My name is {name}");
        }
    }
}