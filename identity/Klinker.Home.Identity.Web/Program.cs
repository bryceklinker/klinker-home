using Klinker.Home.Identity.Web.Common;

var builder = WebApplication.CreateBuilder(args);
builder.AddKlinkerIdentityWeb(builder.Configuration);

var app = builder.Build().UseKlinkerHomeIdentityWeb();

await app.RunMigrationsAsync();
await app.RunAsync();

public partial class Program { }
