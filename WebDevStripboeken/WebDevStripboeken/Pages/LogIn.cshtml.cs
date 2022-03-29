using System.Net;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class LogIn : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        if (Request.Cookies["user"] == null)
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); } 
    }
    
    public IActionResult OnPost([FromForm] string User, [FromForm] string WW)
    {
        if (LogInRepository.checkLogIn(User, WW) == true)
        {
            string json = Request.Cookies["user"];
            myUser coockieUser = JsonConvert.DeserializeObject<myUser>(json);

            coockieUser.Gebruikersnaam = User;

            json = JsonConvert.SerializeObject(coockieUser);
            Response.Cookies.Append("user", json);

            currentUser = coockieUser;
            return Page();
        }
        else
        {
            return Page();
        }
    }
}