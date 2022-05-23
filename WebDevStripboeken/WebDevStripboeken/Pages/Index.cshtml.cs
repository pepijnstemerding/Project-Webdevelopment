using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;
using Newtonsoft.Json;

namespace WebDevStripboeken.Pages;

public class IndexModel : PageModel

{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    //[BindProperty(SupportsGet = true)]  //global get
    //public myUser currentUser { get; set; }
    public IActionResult OnGet()
    {
        /*if (Request.Cookies["user"] == null)                                        //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { 
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
            currentUser = ProfileRepository.GetOne(currentUser.Gebruikersnaam);     //Haalt data op van de gebruiker gedefineerd in de cookie uit de database. 
        }*/
        return Redirect("/home"); //hoef je zelf niet meer te navigeren naar home page
    }
}