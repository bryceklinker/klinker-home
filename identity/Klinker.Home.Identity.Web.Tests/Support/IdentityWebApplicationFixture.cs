namespace Klinker.Home.Identity.Web.Tests.Support;

public class IdentityWebApplicationFixture
{
    public IdentityWebApplication App { get; private set; }
    public HttpClient Client { get; private set; }

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
