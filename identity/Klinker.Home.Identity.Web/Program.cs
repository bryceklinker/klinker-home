using Klinker.Home.Identity.Web.Common;
using Klinker.Home.Identity.Web.Common.Logging;
using Serilog;

var logSettings = new LoggerSettings("identity");
Log.Logger = KlinkerLoggerFactory.CreateLogger(logSettings);
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddKlinkerIdentityWeb(builder.Configuration);
    builder.Host.UseKlinkerLogging(logSettings);

    var app = builder.Build().UseKlinkerHomeIdentityWeb();

    await app.RunMigrationsAsync();
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Failed to start application");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
