using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Klinker.Home.Identity.Web.Pages;

public record LoginViewModel(string Username = "", string Password = "");

[AllowAnonymous]
[ValidateAntiForgeryToken]
public class Login : PageModel
{
    private readonly SignInManager<KlinkerUser> _signInManager;

    [BindProperty]
    public LoginViewModel ViewModel { get; set; } = new();

    public Login(SignInManager<KlinkerUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _signInManager.PasswordSignInAsync(ViewModel.Username, ViewModel.Password, true, true);

        if (result.Succeeded)
            return RedirectToPage("./Dashboard");

        return Page();
    }
}
