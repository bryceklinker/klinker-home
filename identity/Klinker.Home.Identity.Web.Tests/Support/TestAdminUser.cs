namespace Klinker.Home.Identity.Web.Tests.Support;

public record TestAdminUser(string Username, string Password)
{
    public static readonly TestAdminUser Default = new("admin", "tuEQ^kWnf8Pd%J6PkJorK65x");
}
