using Microsoft.AspNetCore.Components.Web;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Toevoegen : PageModel
{
    //[BindProperty(SupportsGet = true)]  //global get
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
    public IActionResult OnPostToevoegen()
    {
        if (SuggestStripboek.Reeks != null && SuggestStripboek.Titel != null && SuggestStripboek.ISBN != null
            && SuggestStripboek.Jaar_v_Uitgave != null && SuggestStripboek.Uitgever != null
            && SuggestStripboek.Afbeelding_urls != null && SuggestStripboek.Waarde_schatting != null)
        {
            ToevoegenRepository.AddOne(SuggestStripboek);
            return Redirect("/ToevoegenTekenaars");
        }
        else return Page();
    }
}