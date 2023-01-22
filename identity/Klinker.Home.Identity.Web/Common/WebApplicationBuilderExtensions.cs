namespace Klinker.Home.Identity.Web.Common;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddKlinkerIdentityWeb(
        this WebApplicationBuilder builder,
        IConfiguration config)
    {
        builder.Services.AddKlinkerHomeIdentityWeb(config);
        return builder;
    }
}