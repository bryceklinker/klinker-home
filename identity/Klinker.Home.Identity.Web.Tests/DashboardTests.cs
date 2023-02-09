using Klinker.Home.Identity.Web.Tests.Support;

namespace Klinker.Home.Identity.Web.Tests;

[Parallelizable(ParallelScope.Self)]
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
