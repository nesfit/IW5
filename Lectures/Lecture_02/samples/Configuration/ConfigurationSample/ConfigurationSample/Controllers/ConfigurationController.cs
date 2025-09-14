using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationSample.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly IOptions<ServerNameConfiguration> serverNameOptions;
    private readonly IConfiguration configuration;

    public ConfigurationController(IOptions<ServerNameConfiguration> serverNameOptions, IConfiguration configuration)
    {
        this.serverNameOptions = serverNameOptions;
        this.configuration = configuration;
    }

    [HttpGet]
    public string Get()
    {
        var serverNameConfiguration = serverNameOptions.Value;
        return serverNameConfiguration.Name;
    }

    [HttpGet("root")]
    public ActionResult GetRoot()
    {
        return Ok(configuration.GetChildren());
    }
}