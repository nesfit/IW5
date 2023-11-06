using CookBook.Common.Extensions;
using CookBook.IdentityProvider.App;
using CookBook.IdentityProvider.App.Installers;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Installers;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Installers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddInstaller<IdentityProviderDALInstaller>();
    builder.Services.AddInstaller<IdentityProviderBLInstaller>();
    builder.Services.AddInstaller<IdentityProviderAppInstaller>();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.MapPost("/user", (IAppUserFacade appUserFacade, [FromBody] AppUserCreateModel appUser) => appUserFacade.CreateAppUserAsync(appUser));

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
