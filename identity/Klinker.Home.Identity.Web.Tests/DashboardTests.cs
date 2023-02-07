using Klinker.Home.Identity.Web.Tests.Support;

namespace Klinker.Home.Identity.Web.Tests;

[TestFixture]
public class DashboardTests : IdentityWebApplicationFixture
{
    [Test]
    public async Task WhenUnauthenticatedUserNavigatesToDashboardThenRedirectsToLogin()
    {
        var response = await NavigateToAsync("/dashboard");

        new Uri(response!.Url).LocalPath.Should().EndWith("/login");
    }
}
