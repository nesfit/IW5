using CookBook.Hosting.ServiceDefaults;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

const string apiClusterName = "api";
const string webClusterName = "web";

var routes = new RouteConfig[]
{
    new()
    {
        Order = 0,
        ClusterId = apiClusterName,
        RouteId = $"{apiClusterName}-api",
        Match = new RouteMatch
        {
            Path = "/api/{*any}",
        },
    },
    new()
    {
        Order = 1,
        ClusterId = apiClusterName,
        RouteId = $"{apiClusterName}-swagger",
        Match = new RouteMatch
        {
            Path = "/swagger/{*any}",
        },
    },
    new()
    {
        Order = 10,
        ClusterId = webClusterName,
        RouteId = webClusterName,
        Match = new RouteMatch
        {
            Path = "{*any}"
        },
    },
};

DestinationConfig CreateDestinationConfig(string destination) => new() { Address = destination };

if (builder.Configuration.GetRequiredSection("services:api:http").Get<string[]>() is not { Length: > 0 } apiAddresses)
{
    throw new ApplicationException("API service configuration is missing!");
}
var apiDestinations = apiAddresses
    .Select((addr, index) => ($"api-{index}", CreateDestinationConfig(addr)))
    .ToDictionary();

if (builder.Configuration.GetRequiredSection("services:web:http").Get<string[]>() is not { Length: > 0 } webAddresses)
{
    throw new ApplicationException("Web service configuration is missing!");
}
var webDestinations = webAddresses
    .Select((addr, index) => ($"web-{index}", CreateDestinationConfig(addr)))
    .ToDictionary();

var clusters = new ClusterConfig[]
{
    new()
    {
        ClusterId = apiClusterName,
        Destinations = apiDestinations,
    },
    new()
    {
        ClusterId = webClusterName,
        Destinations = webDestinations,
    }
};

builder.Services.AddReverseProxy()
    .LoadFromMemory(routes, clusters);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapReverseProxy();

app.Run();
