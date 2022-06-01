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
    public List<myStripboek> lil1 = new List<myStripboek>();
    public List<myCollectie> lil2;
    public List<myCollectie> lil3 = new List<myCollectie>();
    public List<myStripboek> giveBooksResult = new List<myStripboek>();

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
        }
        Rest();
        
    }
    public void OnPostCollectieAanMaken ([FromForm] string CollectieNaam)
    {
        Console.WriteLine(CollectieNaam);
        if (CollectieNaam != null)
        {
            //CollectieRepository.CollectieAanMaken(CollectieNaam);
        }

        Rest();
    }

    public void Rest()
    {
        lil1 = HomeRepository.GetAll(myBase);
        lil2 = CollectieRepository.giveCollecties(currentUser.Gebruikersnaam);
        //lil3 = lil2.Distinct<myCollectie>().ToList();
        foreach (myCollectie collectie in lil2)
        {
            if (lil3.Count == 0)
            {
                lil3.Add(collectie);
            }
            else
            {
                if (lil3.Last().Collectie_id != collectie.Collectie_id)
                {
                    lil3.Add(collectie);
                }
            }
        }
    }
}