using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace IotAdminAPI.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               var connectionString = context.Configuration.GetValue<string>("ConnectionStrings:SerilogDB");


               configuration.Enrich.FromLogContext()
               .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }
               , null, null, LogEventLevel.Information, null, null, null, null);
                

           };
    }
}
