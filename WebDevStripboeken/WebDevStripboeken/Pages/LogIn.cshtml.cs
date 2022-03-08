using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Pages;

public class LogIn : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
    }
    
    public void OnPost([FromForm] string User/*, [FromForm] string WW*/)
    {
        string json = Request.Cookies["user"];
        myUser coockieUser = JsonConvert.DeserializeObject<myUser>(json);

        coockieUser.userName = User;

        json = JsonConvert.SerializeObject(coockieUser);
        Response.Cookies.Append("user", json);

        currentUser = coockieUser;
    }
}