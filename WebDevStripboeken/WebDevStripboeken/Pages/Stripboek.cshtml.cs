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
    public myStripboek boek;
    public void OnGet([FromRoute]int boekid)
    {
        if (Request.Cookies["user"] == null)
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
        
        Boekid = boekid;
        //boek = StripboekRepository.GetOne(boekid);
        foreach (myStripboek x in StripboekRepository.GetOne(boekid))
        {
            boek = x;
        }
    }
}