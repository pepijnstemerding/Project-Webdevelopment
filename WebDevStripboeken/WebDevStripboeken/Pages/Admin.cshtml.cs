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
        if (Request.Cookies["user"] == null)
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { 
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
            currentUser = ProfileRepository.GetOne(currentUser.Gebruikersnaam);
        }

        if (currentUser.Is_admin == 1)
        {
            listUsers = AdminRepository.GetUsers();
            listStripboek = AdminRepository.GetBoeken();
            return Page();
        }

        return Redirect("../home");
    }

    public void OnPostDelUser([FromForm] int id)
    {
        AdminRepository.delUser(id);
        listUsers = AdminRepository.GetUsers();
    }

    public void OnPostDelBoek([FromForm] int id)
    {
        AdminRepository.delBoek(id);
        listStripboek = AdminRepository.GetBoeken();
    }

    public void OnPostAccBoek([FromForm] int id)
    {
        AdminRepository.accBoek(id);
        listStripboek = AdminRepository.GetBoeken();
    }
}