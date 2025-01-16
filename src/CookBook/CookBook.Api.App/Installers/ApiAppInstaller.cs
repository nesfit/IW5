using CookBook.Common.Installers;

namespace CookBook.Api.App.Installers;

public class ApiAppInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();
    }
}
