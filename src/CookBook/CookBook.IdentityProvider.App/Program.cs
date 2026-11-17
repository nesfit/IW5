using CookBook.Common.Extensions;
using CookBook.IdentityProvider.App;
using CookBook.IdentityProvider.App.Endpoints;
using CookBook.IdentityProvider.App.Installers;
using CookBook.IdentityProvider.App.Services;
using CookBook.IdentityProvider.BL.Installers;
using CookBook.IdentityProvider.DAL.Installers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var provisionUser = args.Length > 0 && args[0] == "provision-user";
    var builder = WebApplication.CreateBuilder(provisionUser ? args[1..] : args);

    if (provisionUser)
    {
        var role = builder.Configuration["ProvisionUser:Role"]
            ?? throw new InvalidOperationException("ProvisionUser:Role is required.");
        await LocalUserProvisioning.ProvisionAsync(
            builder.Configuration, [("ProvisionUser", role)], skipExisting: false);
        Log.Information("Local account provisioned.");
        return;
    }

    if (builder.Environment.IsDevelopment()
        && builder.Configuration.GetValue<bool>("DevelopmentSeed:Enabled"))
    {
        var result = await LocalUserProvisioning.ProvisionAsync(
            builder.Configuration,
            [("DevelopmentSeed:Admin", CookBook.Common.UserRoles.Admin),
             ("DevelopmentSeed:User", CookBook.Common.UserRoles.User)],
            skipExisting: true);
        Log.Information("Development provisioning: {Created} created, {Skipped} existing accounts left unchanged.",
            result.Created, result.Skipped);
    }

    builder.Services.AddInstaller<IdentityProviderDALInstaller>();
    builder.Services.AddInstaller<IdentityProviderBLInstaller>();
    builder.Services.AddInstaller<IdentityProviderAppInstaller>();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder.ConfigureServices();

    app.ConfigurePipeline();

    app.MapGroup("api")
        .AllowAnonymous()
        .UseUserEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
    Environment.ExitCode = 1;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
