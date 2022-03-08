using System.Web;
using Microsoft.AspNetCore.Mvc;
using Ubiety.Dns.Core;
using Newtonsoft.Json;

namespace WebDevStripboeken.Models;

public class myUser
{
    [BindProperty]
    public string userName { get; set; }
    public string passWord { get; set; }
    public string eMail { get; set; }
    

    public static void setCookies()
    {
        string jsonUser;
        myUser currentUser = new myUser();
        //string json = JsonConverter.SerializeObject(userName);
        jsonUser = JsonConvert.SerializeObject(currentUser);
        HttpResponse.Cookies.Append("user", jsonUser, new CookieOptions()
        //Response.Cookies.Append("user", jsonUser, new CookieOptions()
        //https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0117
        {
            Expires = DateTimeOffset.Now.AddDays(30)
        });
    }
}