namespace Klinker.Home.Identity.Web.Tests.Support;

public record TestAdminUser(string Username, string Password)
{
    public static readonly TestAdminUser Default = new("admin", "admin");
}
