using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace Klinker.Home.Identity.Web.Tests.Support;

public static class LocatorExtensions
{
    public static ILocator GetByRole(this ILocator locator, AriaRole role, string name)
    {
        return locator.GetByRole(
            role,
            new LocatorGetByRoleOptions { NameRegex = new Regex(name, RegexOptions.IgnoreCase) }
        );
    }
}
