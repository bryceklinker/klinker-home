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
        var userManager = App.GetService<UserManager<KlinkerUser>>();
        var users = await userManager.Users.ToArrayAsync();
        foreach (var user in users)
            await userManager.DeleteAsync(user);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await App.DisposeAsync();
    }
}
