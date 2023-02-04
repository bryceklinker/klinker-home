using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Klinker.Home.Identity.Web.Pages;

[Authorize]
public class Dashboard : PageModel
{
    public void OnGet() { }
}
