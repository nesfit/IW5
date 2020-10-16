using CookBook.Api.Services;

namespace CookBook.Api.Tests.Fixtures
{
    public class ServerServiceStub : IServerService
    {
        public string GetServerName()
        {
            return "Jozin";
        }
    }
}