using Klinker.Home.Identity.Web.Common;

var builder = WebApplication.CreateBuilder(args);
builder.AddKlinkerIdentityWeb(builder.Configuration);

var app = builder.Build()
    .UseKlinkerHomeIdentityWeb();

app.Run();

public partial class Program
{
}