using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using System.ComponentModel.DataAnnotations;

namespace WebDevStripboeken.Pages;

public class SignUp : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            jsonUser = myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
    }
    [BindProperty]
    public Account Account { get; set; }
    public IActionResult OnPostSignUp()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return Page();
    }
    

}
