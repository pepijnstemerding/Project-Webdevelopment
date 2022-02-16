using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevStripboeken.Pages;

public class LogIn : PageModel
{
    [BindProperty]
    public string Gebruiker { get; set; } ="Gast";
    public string WW { get; set; }
    public void OnGet()
    {
        
    }

    public void OnPostSignup([FromForm] string newUser, [FromForm] string newWW)
    {
        Gebruiker = newUser;
        WW = newWW;
    }
    public void OnPostLogin([FromForm] string User, [FromForm] string WW)
    {
        this.Gebruiker = User;
        this.WW = WW;
    }
}