using Klinker.Home.Identity.Web.Tests.Support;

namespace Klinker.Home.Identity.Web.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HomeTests : IdentityWebApplicationFixture
{
    public override async Task BeforeEach()
    {
        await AddAdminUser();
    }

    [Test]
    public async Task WhenUsersExistAndHomePageIsViewedThenRedirectsToLogin()
    {
        var response = await NavigateToAsync("/");

        response!.Url.Should().Contain("/login");
    }
}
