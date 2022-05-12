using Microsoft.AspNetCore.Components.Web;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class ToevoegenTekenaars : PageModel
{
    public int aantalTekenaars { set; get; } = 1;
    public int tempValue { set; get; } = 1;
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        if (Request.Cookies["user"] == null)
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); } 
    }
    [BindProperty]
    public myStripboek SuggestStripboek { get; set; }

    public void OnPost()
    {

    }

    public void OnPostAddTekenaarField([FromForm] int tempValue)
    {
        aantalTekenaars = tempValue + 1;
        this.tempValue = aantalTekenaars;
    }

    public void OnPostRemoveTekenaarField([FromForm] int tempValue)
    {
        if (aantalTekenaars > 1)
        {
            aantalTekenaars = tempValue - 1;
            this.tempValue = aantalTekenaars;
        }
    }
}