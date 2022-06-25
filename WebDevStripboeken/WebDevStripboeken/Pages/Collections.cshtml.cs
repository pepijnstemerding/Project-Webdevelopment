using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Collections : PageModel
{

    [BindProperty(SupportsGet = true)] //global get
    //public myUser currentUserCookie { get; set; }
    public myUser currentUser { get; set; }
    public int myBase = 1;
    private const int defiation = 5;

    public string collName;
    public List<myStripboek> lil1 = new List<myStripboek>();
    //public List<myCollectie> lil2;
    public List<myCollectie> lil3 = new List<myCollectie>();
    public List<myStripboek> giveBooksResult { get; set; } = new List<myStripboek>();

    public void OnGet([FromRoute] int Collid)
    {
        if (Request.Query.ContainsKey("delete"))   //Wordt aangeroepen als de gebruiker op de knop "Uitloggen" klikt in de header
        {
            Console.WriteLine("yay");
            HttpContext.Response.Cookies.Delete("user");
            //myUser.delCookies();
            currentUser = new myUser();
        }
        else
        {
            if (Request.Cookies["user"] == null)
            { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
            else
            { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
        }
        if (Collid != 0)
        {
            giveBooksResult = CollectieRepository.giveBooks(Collid);
            collName = CollectieRepository.giveName(Collid);
        }
        lil1 = HomeRepository.GetAll(myBase);
        lil3 = CollectieRepository.giveCollecties(currentUser.Gebruikersnaam);
        
    }
    public void OnPostCollectieAanMaken ([FromForm] string CollectieNaam)
    {
        
        if (Request.Cookies["user"] == null)
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
        
        Console.WriteLine(CollectieNaam);
        if (CollectieNaam != null)
        {
            CollectieRepository.CollectieAanMaken(CollectieNaam, currentUser.Gebruikersnaam);
        }
        
        lil1 = HomeRepository.GetAll(myBase);
        lil3 = CollectieRepository.giveCollecties(currentUser.Gebruikersnaam);
    }
}