using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevStripboeken.Pages;

public class LogIn : PageModel
{
    [BindProperty]
    public string User { get; set; }
    public string WW { get; set; }
    public void OnGet()
    {
        
    }

    public void OnPostSignup([FromForm] string newUser, [FromForm] string newWW)
    {
        User = newUser;
        WW = newWW;
    }
    public void OnPostLogin([FromForm] string User, [FromForm] string WW)
    {
        this.User = User;
        this.WW = WW;
    }
}