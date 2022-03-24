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
    public List<myStripboek> lil = HomeRepository.GetAll();
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
            string jsonUser = Request.Cookies["user"];
            if (jsonUser == null) //sets first cookie
            {
                jsonUser = myUser.setCookies();
            }

            currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
        }
    }
}