using System;

namespace IW5_Swagger.ConsoleApp.API
{
    public partial class APIClient
    {
        public APIClient(string uri)
            : this(new Uri(uri))
        {
        }
    }
}