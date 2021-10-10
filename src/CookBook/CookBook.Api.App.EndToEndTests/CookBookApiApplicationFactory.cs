using System.Reflection;
using CookBook.Api.App.Controllers.v3;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CookBook.Api.App.EndToEndTests;

public class CookBookApiApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(collection =>
        {
            var controllerAssemblyName = typeof(RecipeController).Assembly.FullName;
            collection.AddMvc().AddApplicationPart(Assembly.Load(controllerAssemblyName));
        });
        return base.CreateHost(builder);
    }
}
