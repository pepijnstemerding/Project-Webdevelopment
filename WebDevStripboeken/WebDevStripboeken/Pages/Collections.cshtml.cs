using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Collections : PageModel
{

    [BindProperty(SupportsGet = true)] //global get
    public myUser currentUserCookie { get; set; }
    public myUser currentUser { get; set; }
    public int myBase = 1;
    private const int defiation = 5;
    public List<myStripboek> lil1;
    public List<myCollectie> lil2;

    public void OnGet()
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
        lil1 = HomeRepository.GetAll(myBase);
        //lil2 = CollectieRepository.giveCollecties(currentUser.Gebruiker_id);
    }
    public void OnPostMin([FromForm] int based)
    {
        myBase = based - defiation;
        lil1 = HomeRepository.GetAll(myBase);
    }
    public void OnPostAdd([FromForm] int based)
    {
        myBase = based + defiation;
        lil1 = HomeRepository.GetAll(myBase);
    }

    public void OnPostReset([FromForm] int based)
    {
        myBase = 1;
        lil1 = HomeRepository.GetAll(myBase);
    }
    public void OnPostCollectieAanMaken (string CollectieNaam)
    {
        CollectieRepository.CollectieAanMaken(CollectieNaam);
    }
}