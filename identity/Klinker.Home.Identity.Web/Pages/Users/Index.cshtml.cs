using System.Collections.Immutable;
using Klinker.Home.Identity.Web.Common;
using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Klinker.Home.Identity.Web.Pages.Users;

public record UsersIndexViewModel(ImmutableArray<KlinkerUser> Users);

public class Index : PageModel
{
    private readonly UserManager<KlinkerUser> _userManager;
    public UsersIndexViewModel ViewModel { get; set; } = new(ImmutableArray<KlinkerUser>.Empty);

    public Index(UserManager<KlinkerUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var users = await _userManager.Users.ToImmutableArrayAsync();
        ViewModel = new UsersIndexViewModel(users);
        return Page();
    }
}
