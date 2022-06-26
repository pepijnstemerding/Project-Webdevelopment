using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Profile : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }

    public string routeUser;
    public void OnGet([FromRoute] string bla)           //why tf u null, moet wss routeUser zijn ipv bla
    {
        if (Request.Cookies["user"] == null)            //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
        
        //get user uit database van route, kijk stripboek voor voorbeeld
        routeUser = bla;
        currentUser = ProfileRepository.GetOne(currentUser.Gebruikersnaam);     //Haalt data op van de huidige Gebruiker uit de database.
        //cheap but works
    }
}
