using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebDevStripboeken.Repository;

namespace WebDevStripboeken.Pages;

public class SignUp : PageModel
{
    // [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        if (Request.Cookies["user"] == null)            //Haalt Cookie op als deze bestaat, als deze niet besstaat dan wordt er een nieuwe aangemaakt.
        { HttpContext.Response.Cookies.Append("user", myUser.setCookies()); }
        else
        { currentUser = JsonConvert.DeserializeObject<myUser>(Request.Cookies["user"]); }
    }
    
    [BindProperty]
    public Account Account { get; set; }
    public IActionResult OnPostSignUp()
    {
        // ValidationContext vc = new ValidationContext(Account); 
        // ICollection<ValidationResult> results = new List<ValidationResult>(); // Will contain the results of the validation
        // bool isValid = Validator.TryValidateObject(Account, vc, results, true);
        
        if (!ModelState.IsValid)
        {
            var errors = 
                from value in ModelState.Values
                where value.ValidationState == ModelValidationState.Invalid
                select value;  
            return Page();
        }
        else
        {
            SignUpRepository.SignUp(Account);
        }
        return RedirectToPage("index");
    }
    

}
