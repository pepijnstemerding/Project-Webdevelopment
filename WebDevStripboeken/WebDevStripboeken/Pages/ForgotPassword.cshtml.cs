using System.Net;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;


namespace WebDevStripboeken.Pages;

public class ForgotPassword : PageModel
{
    [BindProperty(SupportsGet = true)] //global get
    public myUser currentUser { get; set; }
    
    public void OnGet()
    {
        if (Request.Cookies["user"] == null)
        {
            HttpContext.Response.Cookies.Append("user", myUser.setCookies());
        }
        else
        {
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        }
    }

    public void OnPost([FromForm] string User, [FromForm] string WW, [FromForm] string beveiligingsVraag)
    {
        Console.WriteLine($"{User} {WW} {beveiligingsVraag}");
    }
}