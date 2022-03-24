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

    public List<myStripboek> showResults;
    public bool PostMethod;
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
        showResults = (SearchRepository.SearchSubmit(catagorie, query));
        PostMethod = true;
        foreach (myStripboek y in showResults)
        {
            //Console.WriteLine("bla");
        }
    }
}