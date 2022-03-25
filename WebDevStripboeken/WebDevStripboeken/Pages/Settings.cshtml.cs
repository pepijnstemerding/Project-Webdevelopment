using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class Settings : PageModel
{
    
        [BindProperty(SupportsGet = true)]  //global get
        public myUser currentUser { get; set; }
        public void OnGet()
        {
            string jsonUser = Request.Cookies["user"];
            if (jsonUser == null) //sets first cookie
            {
                jsonUser = myUser.setCookies();
            }
            currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
        }
        [BindProperty]
        public Account Account { get; set; }
        public IActionResult OnPostSignUp()
        {
            if (!ModelState.IsValid)
            {
                var errors = 
                    from value in ModelState.Values
                    where value.ValidationState == ModelValidationState.Invalid
                    select value;  
                return Page();  // <-- I set a breakpoint here, and examine "errors"
            }
            else
            {
                SignUpRepository.SignUp(Account);
            }
            return Page();
        }
    

    }
