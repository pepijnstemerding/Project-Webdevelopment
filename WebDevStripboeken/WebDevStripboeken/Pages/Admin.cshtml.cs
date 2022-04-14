using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection.Metadata;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Admin : PageModel
{
    public myUser currentUser { get; set; }
    public List<myUser> listUsers;
    public List<myStripboek> listStripboek;

    public IActionResult OnGet([FromRoute] string action)
    {
        if (Request.Cookies["user"] == null)                                        //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { 
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
            currentUser = ProfileRepository.GetOne(currentUser.Gebruikersnaam);     //Haalt data op van de gebruiker gedefineerd in de cookie uit de database. 
        }

        if (currentUser.Is_admin == 1)                                              //Checkt of de geselecteerde gebruiker een admin is
        {
            listUsers = AdminRepository.GetUsers();
            listStripboek = AdminRepository.GetBoeken();
            return Page();
        }
        return Redirect("../home");
    }
    
/// <summary>
/// Methode die aangeroepen wordt als een admin op de knop drukt die een gebruiker verwijdert.
/// </summary>
/// <param name="id">Het id van de desbetreffende gebruiker (Gebruiker_id).</param>
/// <returns></returns>
    public IActionResult OnPostDelUser([FromForm] int id)
    {
        AdminRepository.delUser(id);
        listStripboek = AdminRepository.GetBoeken();
        listUsers = AdminRepository.GetUsers();
        if (Request.Cookies["user"] != null)
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        return Redirect("../Admin");
    }

/// <summary>
/// Methode die aangeroepen wordt als een admin een niet geaccepteerd boek verwijderd.
/// </summary>
/// <param name="id">Het id van het desbetreffende stripboek (Boek_id).</param>
/// <returns></returns>
    public IActionResult OnPostDelBoek([FromForm] int id)
    {
        AdminRepository.delBoek(id);
        listStripboek = AdminRepository.GetBoeken();
        listUsers = AdminRepository.GetUsers();
        if (Request.Cookies["user"] != null)
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        return Redirect("../Admin");
    }

/// <summary>
/// Methode die aangeroepen wordt als een admin een gesuggereerd stripboek accepteert.
/// </summary>
/// <param name="id">Het id van het desbetreffende stripboek (Boek_id).</param>
/// <returns></returns>
    public IActionResult OnPostAccBoek([FromForm] int id)
    {
        AdminRepository.accBoek(id);
        listStripboek = AdminRepository.GetBoeken();
        listUsers = AdminRepository.GetUsers();
        if (Request.Cookies["user"] != null)
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        return Redirect("../Admin");
    }
}