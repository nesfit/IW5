using ConfigurationSample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.Configure<ServerNameConfiguration>(builder.Configuration.GetSection("ServerName"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/configuration", ([FromServices] IOptions<ServerNameConfiguration> options) =>
{
    return options.Value.Name;
});

app.MapGet("/configuration/root", ([FromServices] IConfigurationRoot configurationRoot) =>
{
    return configurationRoot.GetChildren();
});

app.Run();