using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevStripboeken.Pages;

public class LogIn : PageModel
{
    [BindProperty]
    public string gebruiker { get; set; } ="Gast";
    public string ww { get; set; }
    public void OnGet()
    {
        
    }
    
    public void OnPostLogIn([FromForm] string User, [FromForm] string WW)
    {
        this.gebruiker = User;
        this.ww = WW;
    }
}