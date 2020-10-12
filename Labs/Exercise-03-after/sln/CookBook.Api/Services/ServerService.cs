using CookBook.Api.Options;
using Microsoft.Extensions.Options;

namespace CookBook.Api.Services
{
    public class ServerService : IServerService
    {
        private readonly IOptions<ServerNameOptions> _serverNameOptions;

        public ServerService(IOptions<ServerNameOptions> serverNameOptions)
        {
            _serverNameOptions = serverNameOptions;
        }

        public string GetServerName()
        {
            return _serverNameOptions.Value.Name;
        }
    }
}
