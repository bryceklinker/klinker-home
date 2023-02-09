using Klinker.Home.Identity.Web.Common.Storage;
using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Klinker.Home.Identity.Web.Tests.Support;

public class IdentityWebApplicationFixture : PageTest
{
    public IdentityWebApplication App { get; private set; }
    public HttpClient Client { get; private set; }

    public string BaseAddress => App.BaseAddress.Substring(0, App.BaseAddress.Length - 1);

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions { BaseURL = BaseAddress, BypassCSP = true };
    }

    public async Task<IResponse?> NavigateToAsync(string path)
    {
        return await Page.GotoAsync($"{BaseAddress}{path}");
    }

    public KlinkerIdentityDbContext GetIdentityContext()
    {
        return App.GetService<KlinkerIdentityDbContext>();
    }

    [OneTimeSetUp]
    public Task OneTimeSetup()
    {
        App = new IdentityWebApplication();
        Client = App.CreateClient();
        return Task.CompletedTask;
    }

    [SetUp]
    public async Task Setup()
    {
        await Cleanup();
        await BeforeEach();
    }

    [TearDown]
    public async Task Teardown()
    {
        await Cleanup();
        await AfterEach();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await App.DisposeAsync();
    }

    public virtual Task BeforeEach()
    {
        return Task.CompletedTask;
    }

    public virtual Task AfterEach()
    {
        return Task.CompletedTask;
    }

    private async Task Cleanup()
    {
        var userManager = App.GetService<UserManager<KlinkerUser>>();
        var users = await userManager.Users.ToArrayAsync();
        foreach (var user in users)
            await userManager.DeleteAsync(user);
    }

    protected async Task AddAdminUser()
    {
        await NavigateToAsync("/setup");
        await Page.GetByLabel("Username").FillAsync(TestAdminUser.Default.Username);
        await Page.GetByLabel("Password").FillAsync(TestAdminUser.Default.Password);
        await Page.GetByRole(AriaRole.Button).ClickAsync();
    }
}
