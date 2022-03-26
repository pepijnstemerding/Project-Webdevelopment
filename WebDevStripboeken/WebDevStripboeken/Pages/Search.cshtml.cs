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
    public List<myStripboek> showResults1;
    public List<myStripboek> showResults2;
    public List<myStripboek> showResults3;
    public bool PostMethod;
    private int Count;
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

        foreach (myStripboek x in getResults)
        {
            if (Count < 5)
            {
                showResults1.Add(x);
                Count++;
            }
            else if (Count > 5 && Count < 10)
            {
                showResults2.Add(x);
                Count++;
            }
            else if (Count > 10 &&Count < 15)
            {
                showResults3.Add(x);
                Count++;
            }
        }
        //for (int i = 0; i < 5; i++) use foreach dumkoff
        {
            //myStripboek add = getResults.ElementAt(1);
            //showResults1.Add(getResults.ElementAt(1));
            //Console.WriteLine(add.Titel);
        }
    }
}