using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevStripboeken.Pages;

public class SignUp : PageModel
{
    [BindProperty]
    public string gebruiker { get; set; } ="Gast";
    public string ww { get; set; }
    public string email { get; set; }
    public void OnGet()
    {
        
    }

    public void OnPostSignup([FromForm] string user, string email, string password)
    {
        gebruiker = user;
        ww = password;
        this.email = email;
    }
}