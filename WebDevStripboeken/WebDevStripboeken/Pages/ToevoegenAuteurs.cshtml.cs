using Microsoft.AspNetCore.Components.Web;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class ToevoegenAuteurs : PageModel
{
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
    
    public IActionResult OnPostToevoegenAuteurs([FromForm] string Naam_Auteur1, [FromForm] string Naam_Auteur2,
        [FromForm] string Naam_Auteur3)
    {
        List<string> alleAuteurs = new List<string>();

        if (Naam_Auteur1 != null)
        {
            alleAuteurs.Add(Naam_Auteur1);
        }

        if (Naam_Auteur2 != null)
        {
            alleAuteurs.Add(Naam_Auteur2);
        }

        if (Naam_Auteur3 != null)
        {
            alleAuteurs.Add(Naam_Auteur3);
        }

        if (alleAuteurs != null)
        {
            ToevoegenRepository.toevoegenAuteurs(alleAuteurs);
            
            return Redirect("/Home");
        }
        return Page();
    }
    
}