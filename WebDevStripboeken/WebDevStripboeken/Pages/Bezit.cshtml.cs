using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Bezit : PageModel
{
    public myUser currentUser {set; get;}
    public myStripboek currentBoek { get; set; }
    public myBezit toevoegenboek { set; get; }
    public void OnGet([FromRoute] int Boekid)
    {
        if (Request.Cookies["user"] == null)
        {
            HttpContext.Response.Cookies.Append("user", myUser.setCookies());
        }
        else
        {
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        }

        currentBoek = StripboekRepository.GetOne(Boekid);
    }

    public void OnPostBoekToevoegen([FromForm] myBezit bezit)
    {
        Console.WriteLine(bezit.Boek_id);
        currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        currentBoek = StripboekRepository.GetOne(bezit.Boek_id);
        
        GebruikerBezitRepository.BoekToevoegen(bezit);
        
        Console.WriteLine("called");
    }
}