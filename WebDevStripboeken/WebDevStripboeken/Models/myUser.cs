using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Web;
namespace WebDevStripboeken.Models;

public class myUser
{
    [BindProperty] 
    public string userName { get; set; } = "Guest";
    public string passWord { get; set; }
    public string eMail { get; set; }
    
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