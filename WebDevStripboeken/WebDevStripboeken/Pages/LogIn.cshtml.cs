using System.Net;
using System.Text.Json.Serialization;
using System.Web;
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
        if (Request.Cookies["user"] == null)
        {
            HttpContext.Response.Cookies.Append("user", myUser.setCookies());
        }
        else
        {
            currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        }
        //currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]);
        /*
        Cookie User = HttpContext.Request.Cookies;
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            //jsonUser = myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);*/
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