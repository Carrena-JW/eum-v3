using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Eum.Extensions.Logging
{
    public static class LoggingExtensions
    {
        public static ConfigureHostBuilder UseEumLogging(this ConfigureHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile("appsettings.Development.json")
#endif
                .Build();

            builder.UseSerilog((builder, config) =>
            {
                config.ReadFrom.Configuration(configuration);
            });

            return builder;
        }
    }
}