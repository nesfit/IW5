using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Common.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection serviceCollection);
    }
}