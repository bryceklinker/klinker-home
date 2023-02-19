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
        var totalUserCount = _userManager.Users.CountAsync();
        var verifiedUserCount = _userManager.Users.CountAsync(u => u.EmailConfirmed);
        var unverifiedUserCount = _userManager.Users.CountAsync(u => !u.EmailConfirmed);

        var results = await Task.WhenAll(totalUserCount, verifiedUserCount, unverifiedUserCount).ConfigureAwait(false);

        return new UsersWidgetViewModel(results[0], results[1], results[2]);
    }
}
