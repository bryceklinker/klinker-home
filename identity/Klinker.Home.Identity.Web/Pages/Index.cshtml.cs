using Klinker.Home.Identity.Web.Common;
using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Klinker.Home.Identity.Web.Pages;

public class Index : PageModel
{
    private readonly UserManager<KlinkerUser> _userManager;

    public Index(UserManager<KlinkerUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGet()
    {
        return await _userManager.DoAnyUsersExistAsync() ? Page() : RedirectToPage("./Setup");
    }
}
