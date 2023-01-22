using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace Klinker.Home.Identity.Web.Tests.Support;

public static class PageExtensions
{
    public static ILocator GetByRole(this IPage page, AriaRole role, string name)
    {
        return page.GetByRole(role, new PageGetByRoleOptions { NameRegex = new Regex(name) });
    }
}
