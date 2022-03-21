using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using System.ComponentModel.DataAnnotations;

namespace WebDevStripboeken.Pages;

public class SignUp : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
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
    
    [BindProperty]
    [Required]
    [MinLength(2)]
    [MaxLength(32)]
    public string gebruikersnaam { set; get; }
    
    [BindProperty]
    [Required]
    [EmailAddress]
    public string email { set; get; }
    
    [BindProperty]
    [Required]
    [MinLength(6)]
    [MaxLength(30)]
    public string wachtwoord { set; get; }

    [BindProperty]
    [Required]
    [MaxLength(30)]
    public string wachtwoordbevestiging { set; get; }

}
