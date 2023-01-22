using Klinker.Home.Identity.Web.Tests.Support;
using Microsoft.Playwright;

namespace Klinker.Home.Identity.Web.Tests;

[TestFixture]
public class InitialUserSetup : IdentityWebApplicationFixture
{
    [Test]
    public async Task WhenVisitingServerThenPromptsForAdminUserCredentials()
    {
        await NavigateToAsync("/setup");

        await Expect(Page.GetByRole(AriaRole.Textbox, "Username")).ToHaveCountAsync(1);
        await Expect(Page.GetByRole(AriaRole.Textbox, "Password")).ToHaveCountAsync(1);
    }
}
