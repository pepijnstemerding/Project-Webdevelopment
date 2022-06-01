using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevStripboeken.Repository;
using WebDevStripboeken.Models;
using Newtonsoft.Json;

namespace WebDevStripboeken.Pages;

public class Stripboek : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public int Boekid { get; set; }
    public myStripboek boek { get; set; }
    public myBezit userspecific { get; set; }

    public void OnGet([FromRoute]int Boekid)
    {
        if (Request.Cookies["user"] == null) //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        {
            HttpContext.Response.Cookies.Append("user", myUser.setCookies());
        } else {
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        } 
        
        boek = StripboekRepository.GetOne(Boekid);  //Haalt data op van de database op basis van het Boek_id uit de route
        if (currentUser.Gebruikersnaam != "Guest")
        {
            userspecific = BezitRepository.UserSpecificStripboekData(Boekid, currentUser.Gebruikersnaam); // Haalt data op van de database die specifiek is aan de ingelogde gebruiker
            Console.WriteLine($"{userspecific.Boek_id}, {userspecific.Gebruiker_id}, {userspecific.Gekocht_voor}, {userspecific.Locatie}, {userspecific.Status_exemplaar}");   
        }
    }

    public void OnPostCollectionSelect([FromForm] string collection)
    {
        //adds boek to selected category
    }
}