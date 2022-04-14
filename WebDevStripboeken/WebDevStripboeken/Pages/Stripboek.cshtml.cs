using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevStripboeken.Repository;
using WebDevStripboeken.Models;
using Newtonsoft.Json;

namespace WebDevStripboeken.Pages;

public class Stripboek : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public int Boekid { get; set; }
    public myStripboek boek { get; set; }

    public void OnGet([FromRoute]int boekid)
    {
        if (Request.Cookies["user"] == null)        //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
        
        Boekid = boekid;
        boek = StripboekRepository.GetOne(boekid);  //Haalt data op van de database op basis van het Boek_id uit de route
    }
}