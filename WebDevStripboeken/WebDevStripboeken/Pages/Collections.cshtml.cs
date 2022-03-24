using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Collections : PageModel
{
    
    [BindProperty(SupportsGet = true)]  //global get
    public myCollectie currentUser { get; set; }
    public List<myCollectie> lil2 = CollectieRepository.GetAll();
    public void OnGet()
    {
        if (Request.Query.ContainsKey("delete"))
        {
            Console.WriteLine("yay");
            HttpContext.Response.Cookies.Delete("user");
            //myUser.delCookies();
            currentUser = new myCollectie();
        }
        else
        {
            string jsonUser = Request.Cookies["user"];
            if (jsonUser == null) //sets first cookie
            {
                jsonUser = myUser.setCookies();
            }

            currentUser = JsonConvert.DeserializeObject<myCollectie>(jsonUser);
        }
    }
}