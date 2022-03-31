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

    public List<myStripboek> getResults;
    public List<myStripboek> showResults1 = new List<myStripboek>();
    public List<myStripboek> showResults2 = new List<myStripboek>();
    public List<myStripboek> showResults3 = new List<myStripboek>();
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
        
        getResults = (SearchRepository.SearchSubmit(catagorie, query)); //Stuurt parameters naar repository, verwacht een List<myStripboeken> terug.
        PostMethod = true;
        
        if (getResults.Count != 0)                                          //Splits de verkregen lijst naar kleinere lijsten.
        {
            foreach (myStripboek x in getResults)
            {
                if (getResults.Count != 0)
                {
                    if (Count < 5)
                    {
                        showResults1.Add(x);
                        Count++;
                    }
                    else if (Count >= 5 && Count < 10)
                    {
                        showResults2.Add(x);
                        Count++;
                    }
                    else if (Count >= 10 && Count < 15)
                    {
                        showResults3.Add(x);
                        Count++;
                    }
                }
            }
        }
        else        //Message die wordt weergeven als er niks gevonden word in de database met de meegegeven parameters.
        {
            message = "Niks gevonden..";
        }
    }
}