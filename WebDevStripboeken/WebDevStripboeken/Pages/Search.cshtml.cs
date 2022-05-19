using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Search : PageModel
{
    [BindProperty(SupportsGet = true)]
    public myUser currentUser { get; set; }

    public List<myStripboek> getResults = new List<myStripboek>();
    public bool PostMethod;
    private int Count;
    public string message;
    public void OnGet()
    {
        if (Request.Cookies["user"] == null)    //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }

        PostMethod = false;                     //Boolean die vermeldt welke On methode is gebruikt, voorkomt het afdrukken van lege lijsten.
    }

    /// <summary>
    /// Methode die word aangeroepen als de Gebruiker iets zoekt
    /// </summary>
    /// <param name="catagorie">De tabelnaam waarin vervolgens zal worden gezocht.</param>
    /// <param name="query">De zoekterm waarop gezocht zal worden</param>
    public void OnPost([FromForm] string catagorie, string query)
    {
        Console.WriteLine(catagorie);
        getResults = (SearchRepository.SearchSubmit(catagorie, query)); //Stuurt parameters naar repository, verwacht een List<myStripboeken> terug.
        PostMethod = true;
    }
}