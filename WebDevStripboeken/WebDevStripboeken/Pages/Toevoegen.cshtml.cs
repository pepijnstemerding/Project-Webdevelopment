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
    public IActionResult OnPostToevoegen([FromForm] string AuteursString, string TekenaarsString)
    {
        if (!ModelState.IsValid)
        {
            var errors = 
                from value in ModelState.Values
                where value.ValidationState == ModelValidationState.Invalid
                select value;
            return Page();
        }
        else
        {

            List<string> AuteurList = new List<string>(AuteursString.Split(", "));
            List<string> TekenaarList = new List<string>(TekenaarsString.Split(", "));
            ToevoegenRepository.AddOne(SuggestStripboek, AuteurList, TekenaarList);
        }
        return Page();
    }
}