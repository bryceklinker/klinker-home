using Klinker.Home.Identity.Web.Tests.Support;
using Microsoft.Playwright;

namespace Klinker.Home.Identity.Web.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UsersListTest : IdentityWebApplicationFixture
{
    public override async Task BeforeEach()
    {
        await LoginAsAdminUser();
    }

    [Test]
    public async Task WhenUsersAreViewedThenAdminUserIsInList()
    {
        await NavigateToAsync("/users");

        var userItem = Page.GetByRole(AriaRole.Listitem, "user");
        await Expect(userItem).ToHaveCountAsync(1);
        await Expect(userItem).ToContainTextAsync(TestAdminUser.Default.Username);
        await Expect(userItem.GetByRole(AriaRole.Button, "edit")).ToBeVisibleAsync();
        await Expect(userItem.GetByRole(AriaRole.Button, "delete")).ToBeVisibleAsync();
    }
}
