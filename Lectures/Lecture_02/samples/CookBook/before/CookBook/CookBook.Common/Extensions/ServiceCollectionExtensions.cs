using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection)
            where TInstaller : IInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(serviceCollection);
        }
    }
}
