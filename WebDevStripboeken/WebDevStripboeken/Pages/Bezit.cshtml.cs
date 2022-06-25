using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Bezit : PageModel
{
    public myUser currentUser {set; get;}
    public myBezit toevoegenboek { set; get; }
    public void OnGet()
    {
        if (Request.Query
            .ContainsKey("delete")) //Wordt aangeroepen als de gebruiker op de knop "Uitloggen" klikt in de header
        {
            Console.WriteLine("yay");
            HttpContext.Response.Cookies.Delete("user");
            //myUser.delCookies();
            currentUser = new myUser();
        }
        else
        {
            if (Request.Cookies["user"] == null)
            {
                HttpContext.Response.Cookies.Append("user", myUser.setCookies());
            }
            else
            {
                currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
            }
        }
    }
}