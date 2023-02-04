using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Klinker.Home.Identity.Web.Pages;

public record LoginViewModel(string Username = "", string Password = "", bool StaySignedIn = false);

public class Login : PageModel
{
    private readonly SignInManager<KlinkerUser> _signInManager;

    [BindProperty]
    public LoginViewModel ViewModel { get; set; } = new();

    public Login(SignInManager<KlinkerUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPost()
    {
        var result = await _signInManager.PasswordSignInAsync(
            ViewModel.Username,
            ViewModel.Password,
            ViewModel.StaySignedIn,
            false
        );
        if (result.Succeeded)
        {
            return RedirectToPage("./Dashboard");
        }

        return Page();
    }
}
