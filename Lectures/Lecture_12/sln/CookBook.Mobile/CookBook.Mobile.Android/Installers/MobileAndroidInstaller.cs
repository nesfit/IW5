using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Xamarin.Android.Net;

namespace CookBook.Mobile.Droid.Installers
{
    public class MobileAndroidInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            new AndroidClientHandler();
            serviceCollection.AddSingleton(factory => new HttpClient(new AndroidClientHandler()));
        }
    }
}