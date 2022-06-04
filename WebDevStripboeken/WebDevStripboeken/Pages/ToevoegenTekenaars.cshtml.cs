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
    public int tempValue { set; get; } = 1;
    public myUser currentUser { get; set; }

    public void OnGet()
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

    [BindProperty] public myTekenaar SuggestTekenaar { get; set; }

    public void OnPost()
    {

    }

    public IActionResult OnPostToevoegenTekenaars([FromForm] string Naam_Tekenaar1, [FromForm] string Naam_Tekenaar2,
        [FromForm] string Naam_Tekenaar3)
    {
        List<string> alleTekenaars = new List<string>();

        if (Naam_Tekenaar1 != null)
        {
            alleTekenaars.Add(Naam_Tekenaar1);
        }

        if (Naam_Tekenaar2 != null)
        {
            alleTekenaars.Add(Naam_Tekenaar2);
        }

        if (Naam_Tekenaar3 != null)
        {
            alleTekenaars.Add(Naam_Tekenaar3);
        }

        if (alleTekenaars != null)
        {
            ToevoegenRepository.ToevoegenTekenaars(alleTekenaars);
            return Redirect("/ToevoegenAuteurs");
        }
        return Page();
    }
}