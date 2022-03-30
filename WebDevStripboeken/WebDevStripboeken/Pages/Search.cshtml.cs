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
        if (Request.Cookies["user"] == null)
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }

        PostMethod = false;
    }

    public void OnPost([FromForm] string catagorie, string query)
    {
        getResults = (SearchRepository.SearchSubmit(catagorie, query));
        PostMethod = true;

        if (getResults.Count != 0)
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
        else
        {
            message = "Niks gevonden..";
        }
    }
}