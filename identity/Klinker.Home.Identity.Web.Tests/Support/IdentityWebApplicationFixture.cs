using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Klinker.Home.Identity.Web.Tests.Support;

public class IdentityWebApplicationFixture : PageTest
{
    public IdentityWebApplication App { get; private set; }
    public HttpClient Client { get; private set; }

    public string BaseAddress => App.BaseAddress.Substring(0, App.BaseAddress.Length - 1);

    public async Task<IResponse?> NavigateToAsync(string path)
    {
        return await Page.GotoAsync($"{BaseAddress}{path}");
    }

    [OneTimeSetUp]
    public Task OneTimeSetup()
    {
        App = new IdentityWebApplication();
        Client = App.CreateClient();
        return Task.CompletedTask;
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await App.DisposeAsync();
    }
}
