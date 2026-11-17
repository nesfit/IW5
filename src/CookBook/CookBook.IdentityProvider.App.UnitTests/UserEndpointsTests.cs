using CookBook.IdentityProvider.App.Endpoints;
using CookBook.IdentityProvider.BL.Facades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class UserEndpointsTests
{
    [Fact]
    public async Task UserEndpointsExposeOnlySearchAtBothPrefixes()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(Mock.Of<IAppUserFacade>());
        await using var app = builder.Build();
        app.MapGroup("api").AllowAnonymous().UseUserEndpoints();
        app.UseUserEndpoints();

        var endpoints = ((IEndpointRouteBuilder)app).DataSources
            .SelectMany(source => source.Endpoints)
            .OfType<RouteEndpoint>()
            .ToArray();

        Assert.Equal(2, endpoints.Length);
        Assert.Contains(endpoints, endpoint => endpoint.RoutePattern.RawText == "api/user/search");
        Assert.Contains(endpoints, endpoint => endpoint.RoutePattern.RawText == "user/search");
        Assert.All(endpoints, endpoint =>
            Assert.Equal(new[] { HttpMethods.Get }, endpoint.Metadata.GetRequiredMetadata<HttpMethodMetadata>().HttpMethods));
    }
}
