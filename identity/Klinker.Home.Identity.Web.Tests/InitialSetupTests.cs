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
        users[0].UserName.Should().Be(TestAdminUser.Default.Username);
        users[0].EmailConfirmed.Should().Be(true);
    }

    [Test]
    public async Task WhenAdminUserMissingThenHomePageRedirectsToSetupPage()
    {
        var response = await NavigateToAsync("/");

        response!.Url.Should().EndWithEquivalentOf("/setup");
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
        await Page.GetByLabel("Username").FillAsync(TestAdminUser.Default.Username);
        await Page.GetByLabel("Password").FillAsync(TestAdminUser.Default.Password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();
    }
}
