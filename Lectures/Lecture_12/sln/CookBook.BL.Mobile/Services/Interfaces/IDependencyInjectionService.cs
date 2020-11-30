using CookBook.BL.Mobile.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CookBook.BL.Mobile.Services
{
    public interface IDependencyInjectionService
    {
        void Build(IServiceCollection serviceCollection);
        TService Resolve<TService>();
        TService Resolve<TService>(params TypedParameter[] parameters);
        object Resolve(Type type);
        object Resolve(Type type, params TypedParameter[] parameters);
        IEnumerable<TService> ResolveAll<TService>();
    }
}