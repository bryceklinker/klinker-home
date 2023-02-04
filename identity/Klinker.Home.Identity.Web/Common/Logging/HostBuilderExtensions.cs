using Serilog;

namespace Klinker.Home.Identity.Web.Common.Logging;

public static class HostBuilderExtensions
{
    public static ConfigureHostBuilder UseKlinkerLogging(this ConfigureHostBuilder builder, LoggerSettings settings)
    {
        builder.UseSerilog(KlinkerLoggerFactory.CreateLogger(settings));
        return builder;
    }
}
