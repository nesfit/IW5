using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLogger
{
    public static class Logger
    {
        static Logger()
        {
            const string eventFormat = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: eventFormat)
                .CreateLogger();
        }



        public static void MethodEnter(string fullname)
        {
            Log.Information("Method {fullname} entered.", fullname);
        }

        public static void MethodLeave(string fullname)
        {
            Log.Information("Method {fullname} about to return.", fullname);
        }
    }
}
