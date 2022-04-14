using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;
using System;

namespace WebDevStripboeken.Pages;

public class Collections : PageModel
{

    [BindProperty(SupportsGet = true)] //global get
    public myUser currentUserCookie { get; set; }
    public myUser currentUser { get; set; }
    public int myBase = 1;
    private const int defiation = 5;
    public List<myStripboek> lil1;
    public string name { get; set; }
    

    public void OnGet()
    {
        if (Request.Query.ContainsKey("delete"))                //Wordt aangeroepen als de gebruiker op de knop "Uitloggen" klikt in de header
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
        lil1 = HomeRepository.GetAll(myBase);
        name = currentUser.Gebruikersnaam.ToString();
    }
    
    public void OnPostMin([FromForm] int based)
    {
        if (based > 1)
        {
            myBase = based - defiation;
            lil1 = HomeRepository.GetAll(myBase);
        }
        else
        {
            myBase = based;
            lil1 = HomeRepository.GetAll(myBase);
        }
        if (Request.Cookies["user"] != null)
            currentUserCookie = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
    }
    public void OnPostAdd([FromForm] int based)
    {
        if (HomeRepository.GetAll(based + defiation).Count == 0)
        {
            myBase = based;
            lil1 = HomeRepository.GetAll(myBase);
        }
        else
        {
            myBase = based + defiation;
            lil1 = HomeRepository.GetAll(myBase);
        }
        if (Request.Cookies["user"] != null)
            currentUserCookie = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
    }

    public void OnPostReset([FromForm] int based)
    {
        myBase = 1;
        lil1 = HomeRepository.GetAll(myBase);
        if (Request.Cookies["user"] != null)
            currentUserCookie = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
    }

}