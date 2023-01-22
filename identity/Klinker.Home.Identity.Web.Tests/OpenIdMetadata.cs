using System.Net;
using Klinker.Home.Identity.Web.Tests.Support;

namespace Klinker.Home.Identity.Web.Tests;

public class OpenIdMetadata : IClassFixture<IdentityWebApplicationFixture>
{
    private readonly IdentityWebApplicationFixture _fixture;

    public OpenIdMetadata(IdentityWebApplicationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task WhenGettingOpenIdMetadataThenReturnsOpenIdMetadata()
    {
        var client = _fixture.CreateClient();

        var response = await client.GetAsync(".well-known/openid-configuration");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}