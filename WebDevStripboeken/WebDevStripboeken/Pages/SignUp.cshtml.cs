using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebDevStripboeken.Pages;

public class SignUp : PageModel
{
    [BindProperty(SupportsGet = true)]
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

    
}