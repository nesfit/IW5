using System.Reflection;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CookBook.Api.App.EndToEndTests;

public class CookBookApiApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(collection =>
        {
            var controllerAssemblyName = typeof(Program).Assembly.FullName;
            if (controllerAssemblyName is not null)
            {
                collection.AddMvc().AddApplicationPart(Assembly.Load(controllerAssemblyName));
            }
        });
        return base.CreateHost(builder);
    }
}
