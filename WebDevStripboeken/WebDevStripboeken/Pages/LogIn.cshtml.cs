using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
            currentUser = new myUser();
            //string json = JsonConverter.SerializeObject(userName);
            jsonUser = JsonConvert.SerializeObject(currentUser);
            Response.Cookies.Append("user", jsonUser, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(30)
            });
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