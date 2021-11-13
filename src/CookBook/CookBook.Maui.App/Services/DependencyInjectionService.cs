using System;
using CookBook.Maui.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace CookBook.Maui.App.Services;

public class DependencyInjectionService : IDependencyInjectionService
{
    private IServiceProvider serviceProvider;

    public MauiApp Build(MauiAppBuilder builder)
    {
        var app = builder.Build();
        serviceProvider = app.Services;

        return app;
    }

    public TService GetRequiredService<TService>()
    {
        return serviceProvider!.GetRequiredService<TService>();
    }

    public object GetRequiredService(Type type)
    {
        return serviceProvider!.GetRequiredService(type);
    }
}
