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

        await Page.GetByRole(AriaRole.Textbox, "Username").TypeAsync(TestAdminUser.Default.Username);
        await Page.GetByRole(AriaRole.Textbox, "Password").TypeAsync(TestAdminUser.Default.Password);
        var response = await Page.RunAndWaitForNavigationAsync(async () =>
        {
            await Page.GetByRole(AriaRole.Button).ClickAsync();
        });

        response!.Url.Should().EndWith("/Dashboard");
    }
}
