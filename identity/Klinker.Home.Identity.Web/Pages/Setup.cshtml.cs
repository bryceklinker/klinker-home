using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Pages;

public record SetupViewModel(string Username = "", string Password = "")
{
    public KlinkerUser ToUser()
    {
        return new KlinkerUser { UserName = Username, };
    }
}

public class Setup : PageModel
{
    private readonly UserManager<KlinkerUser> _userManager;

    [BindProperty]
    public SetupViewModel ViewModel { get; set; } = new();

    public Setup(UserManager<KlinkerUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var hasUser = await _userManager.Users.AnyAsync();
        return hasUser ? RedirectToPage("./Login") : Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = ViewModel.ToUser();
        var result = await _userManager.CreateAsync(user, ViewModel.Password);
        return result.Succeeded ? RedirectToPage("./Login") : Page();
    }
}
