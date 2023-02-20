using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Pages.Dashboard;

[Authorize]
public class Dashboard : PageModel
{
    private readonly UserManager<KlinkerUser> _userManager;

    [BindProperty]
    public UsersWidgetViewModel UsersWidgetViewModel { get; set; } = new(0, 0, 0);

    public Dashboard(UserManager<KlinkerUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGet()
    {
        UsersWidgetViewModel = await CreateUsersWidgetViewModel();
        return Page();
    }

    private async Task<UsersWidgetViewModel> CreateUsersWidgetViewModel()
    {
        var totalUserCount = await _userManager.Users.CountAsync().ConfigureAwait(false);
        var verifiedUserCount = await _userManager.Users.CountAsync(u => u.EmailConfirmed).ConfigureAwait(false);
        var unverifiedUserCount = await _userManager.Users.CountAsync(u => !u.EmailConfirmed).ConfigureAwait(false);

        return new UsersWidgetViewModel(totalUserCount, verifiedUserCount, unverifiedUserCount);
    }
}
