using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevStripboeken.Pages;

public class Home : PageModel
{
    [BindProperty]
    public string Gebruiker { get; set; }
    public void OnGet([FromQuery] string User)
    {
        
    }
}