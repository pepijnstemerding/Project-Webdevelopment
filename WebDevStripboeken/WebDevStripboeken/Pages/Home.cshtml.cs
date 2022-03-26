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
    public int myBase = 1;
    private const int defiation = 5;
    public List<myStripboek> lil;
    public void OnGet()
    {
        if (Request.Query.ContainsKey("delete"))
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
        lil = HomeRepository.GetAll(myBase);
    }

    public void OnPostMin([FromForm] int based)
    {
        myBase = based - defiation;
        lil = HomeRepository.GetAll(myBase);
    }
    public void OnPostAdd([FromForm] int based)
    {
        myBase = based + defiation;
        lil = HomeRepository.GetAll(myBase);
    }

    public void OnPostReset([FromForm] int based)
    {
        myBase = 1;
        lil = HomeRepository.GetAll(myBase);
    }
}