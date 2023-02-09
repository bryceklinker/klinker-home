using System.Net;
using Klinker.Home.Identity.Web.Tests.Support;

namespace Klinker.Home.Identity.Web.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class OpenIdMetadata : IdentityWebApplicationFixture
{
    [Test]
    public async Task WhenGettingOpenIdMetadataThenReturnsOpenIdMetadata()
    {
        var response = await Client.GetAsync(".well-known/openid-configuration");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
