using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Settings : PageModel
{ 
    //[BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            jsonUser = myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
    }

    // public void OnPostUpdateGebruikersnaam([FromForm] string gebruikersnaam)
    // {
    //     SettingsRepository.UpdateGebruikersnaam(gebruikersnaam);
    //     if (Request.Cookies["user"] != null)
    //         currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
    // }
    [BindProperty]
    public Account Account { get; set; }

    public IActionResult OnPostUpdateAccount()
    {
        if (ModelState.IsValid)
        {
            SettingsRepository.UpdateGebruikersnaam();
        }
        return Page();
    }
}
