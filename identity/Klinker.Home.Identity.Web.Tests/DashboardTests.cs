using Klinker.Home.Identity.Web.Tests.Support;
using Microsoft.Playwright;

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

    [Test]
    public async Task WhenUserLogsInThenUsersWidgetIsVisible()
    {
        await LoginAsAdminUser();

        await Expect(Page.GetByRole(AriaRole.Heading, "users widget")).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, "total users")).ToContainTextAsync("1");
        await Expect(Page.GetByRole(AriaRole.Heading, "confirmed users")).ToContainTextAsync("1");
        await Expect(Page.GetByRole(AriaRole.Heading, "unverified users")).ToContainTextAsync("0");
    }
}
