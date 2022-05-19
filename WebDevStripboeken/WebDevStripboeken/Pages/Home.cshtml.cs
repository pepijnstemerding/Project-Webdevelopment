using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Home : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public int myBase = 0;
    private const int defiation = 5;
    public List<myStripboek> lil;
    public string message;
    
    public void OnGet()
    {
        if (Request.Query.ContainsKey("delete"))        //Wordt aangeroepen als de Gebruiker op de knop "Logout" drukt in de header
        {                                               //Verwijdert het huidige Cookie en zet de "currentUser" als een nieuwe default "myUser".
            Console.WriteLine("yay");
            HttpContext.Response.Cookies.Delete("user");
            //myUser.delCookies();
            message = "bye bye";
            currentUser = new myUser();
        }
        else
        {
            if (Request.Cookies["user"] == null)        //Haalt Cookie op als deze bestaat, als deze niet bestaat dan wordt er een nieuwe aangemaakt.
            { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
            else
            { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
        }
        lil = HomeRepository.GetAll(myBase, currentUser);
        userSpecific = 
    }

    /// <summary>
    /// Methode die wordt aangeroepen als de Gebruiker het vorige set stripboeken wilt zien. 
    /// </summary>
    /// <param name="based"></param>
    public void OnPostMin([FromForm] int based)
    {
        if (based > 1)
            myBase = based - defiation;
        else
            myBase = based;

        currentUser = null;
        if (Request.Cookies["user"] != null)
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        lil = HomeRepository.GetAll(myBase, currentUser);
    }
    
    /// <summary>
    /// Methode die wordt aangeroepen als de Gebruiker het volgende set stripboeken wilt zien.
    /// </summary>
    /// <param name="based"></param>
    public void OnPostAdd([FromForm] int based)
    {
        if (HomeRepository.GetAll(based + 5).Count == 0)
            myBase = based;
        else
            myBase = based + defiation;
        currentUser = null;
        if (Request.Cookies["user"] != null)
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        lil = HomeRepository.GetAll(myBase, currentUser);
    }

    /// <summary>
    /// Methode die wordt aangeroepen als de Gebruiker terug wilt naar het begin van de lijst.
    /// </summary>
    /// <param name="based"></param>
    public void OnPostReset([FromForm] int based)
    {
        myBase = 1;
        currentUser = null;
        if (Request.Cookies["user"] != null)
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        lil = HomeRepository.GetAll(myBase, currentUser);
    }
}