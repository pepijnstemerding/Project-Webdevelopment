using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
namespace WebDevStripboeken.Pages;

public class Collections : PageModel
{
    
    [BindProperty(SupportsGet = true)]  //global get
    public myCollectie currentUser { get; set; }
    public List<myCollectie> lil2 = CollectieRepository.GetAll();
    public void OnGet()
    {
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            jsonUser = myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myCollectie>(jsonUser);
    }
}