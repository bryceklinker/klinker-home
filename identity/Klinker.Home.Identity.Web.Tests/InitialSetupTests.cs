using Klinker.Home.Identity.Web.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;

namespace Klinker.Home.Identity.Web.Tests;

[TestFixture]
public class InitialUserSetup : IdentityWebApplicationFixture
{
    [Test]
    public async Task WhenAdminCredentialsAreEnteredThenAddsUserToDatabase()
    {
        await NavigateToAsync("/setup");

        await CreateAdminUser();

        var users = await GetIdentityContext().Users.ToArrayAsync();
        users.Should().HaveCount(1);
    }

    [Test]
    public async Task WhenAdminAlreadyExistsThenSetupRedirectsToLoginPage()
    {
        await NavigateToAsync("/setup");

        await CreateAdminUser();

        var response = await NavigateToAsync("/setup");
        response!.Url.Should().EndWithEquivalentOf("/login");
    }

    private async Task CreateAdminUser()
    {
        await Page.GetByRole(AriaRole.Textbox, "Username").TypeAsync(TestAdminUser.Default.Username);
        await Page.GetByRole(AriaRole.Textbox, "Password").TypeAsync(TestAdminUser.Default.Password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();
    }
}
