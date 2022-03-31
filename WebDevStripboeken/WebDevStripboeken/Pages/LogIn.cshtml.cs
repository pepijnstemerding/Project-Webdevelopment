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

    public string message = "mislukt";
    public void OnGet()
    {
        if (Request.Cookies["user"] == null)                //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
    }
    
    public IActionResult OnPost([FromForm] string User, [FromForm] string WW)
    {
        if (LogInRepository.checkLogIn(User, WW))      //Verstuurt data van het inloggen naar de repository, verwacht een bool terug. 
        {                                                   //Als het inloggen lukt, haalt het huidige cookie op om deze te veranderen naar de waardes van de nieuw ingelogde Gebruiker.
            string json = Request.Cookies["user"];          
            myUser coockieUser = JsonConvert.DeserializeObject<myUser>(json);

            coockieUser.Gebruikersnaam = User;

            json = JsonConvert.SerializeObject(coockieUser);
            Response.Cookies.Append("user", json);

            currentUser = coockieUser;
            message = "Welkom, " + currentUser.Gebruikersnaam;
            return RedirectToPage("index");
        }
        else                                                //Als het inloggen niet lukt, verschijnt er een message box bovenaan de pagina en wordt de cookie niet verandert.
        {
            message = "Sorry, niet gelukt";
            return Page();
        }
    }
}