using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Web;
namespace WebDevStripboeken.Models;

[BindProperties]
public class myUser
{
    
    public string Gebruikersnaam { get; set; } = "Guest";
    public string Email { get; set; }
    public string Wachtwoord { get; set; }
    public int Is_admin { get; set; }
    public int Profiel_zichtbaarheid { get; set; }
    public int Collectie_zichtbaarheid { get; set; }
    public DateTime Geboorte_datum { get; set; }
    public string Beveiligingsvraag { get; set; }
    
    
    public static string setCookies()
    {
        myUser currentUser = new myUser();
        string jsonUser = JsonConvert.SerializeObject(currentUser);
        Cookie user = new Cookie("user", jsonUser);
        {
            user.Expires = DateTime.Now.AddDays(30);
        };
        return JsonConvert.SerializeObject(user);
    }

    public static void delCookies()
    {
        Cookie del = new Cookie();
        del.Discard = true;
    }
}