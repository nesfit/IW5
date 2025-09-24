using LoggingSample.Api.DAL;
using LoggingSample.Api.Facades;
using LoggingSample.Api.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSeq();

builder.Services.AddOpenApi();
builder.Services.AddSingleton<OrderDatabase>();
builder.Services.AddTransient<OrderRepository>();
builder.Services.AddTransient<OrderFacade>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

app.UseHttpsRedirection();

app.MapPost("/api/order", (
    [FromBody] CreateOrderModel createModel,
    [FromServices] OrderFacade orderFacade) =>
        orderFacade.CreateOrder(createModel));

app.MapGet("/api/order/{userId:guid}", (
    [FromRoute] Guid userId,
    [FromServices] OrderFacade orderFacade) =>
        orderFacade.GetOrderCreatedByUser(userId));

app.Run();