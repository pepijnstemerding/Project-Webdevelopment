using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Pages;

public class Profile : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
    }
}