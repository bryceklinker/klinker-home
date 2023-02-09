using System.Text.RegularExpressions;
using Klinker.Home.Identity.Web.Tests.Support;
using Microsoft.Playwright;

namespace Klinker.Home.Identity.Web.Tests;

[TestFixture]
public class LoginTests : IdentityWebApplicationFixture
{
    public override async Task BeforeEach()
    {
        await AddAdminUser();
    }

    [Test]
    public async Task WhenAdminLogsInThenDashboardIsDisplayed()
    {
        await NavigateToAsync("/login");

        await Page.GetByLabel("Username").FillAsync(TestAdminUser.Default.Username);
        await Page.GetByLabel("Password").FillAsync(TestAdminUser.Default.Password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();
    }

    [Test]
    public async Task WhenAdminFailsToLoginThenLoginPageIsDisplayed()
    {
        await NavigateToAsync("/login");

        await Page.GetByLabel("Username").FillAsync("bad");
        await Page.GetByLabel("Password").FillAsync("bad");
        await Page.GetByRole(AriaRole.Button).ClickAsync();

        await Expect(Page.GetByLabel("Username")).ToBeVisibleAsync();
    }
}
