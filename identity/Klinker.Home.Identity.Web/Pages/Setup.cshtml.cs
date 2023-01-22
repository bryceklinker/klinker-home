using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Klinker.Home.Identity.Web.Pages;

public record SetupViewModel(string Username = "", string Password = "");

public class Setup : PageModel
{
    [BindProperty]
    public SetupViewModel ViewModel { get; set; } = new();

    public void OnGet() { }
}
