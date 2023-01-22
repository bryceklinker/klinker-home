namespace Klinker.Home.Identity.Web.Common;

public static class WebApplicationExtensions
{
    public static WebApplication UseKlinkerHomeIdentityWeb(this WebApplication app)
    {
        app.UseIdentityServer();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();
        return app;
    }
}
