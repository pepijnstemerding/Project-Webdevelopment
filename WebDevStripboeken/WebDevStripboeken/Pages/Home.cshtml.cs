﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Pages;

public class Home : PageModel
{
    [BindProperty(SupportsGet = true)]  //global get
    public Models.myUser currentUser { get; set; }
    public void OnGet()
    {
        string jsonUser = Request.Cookies["user"];
        if (jsonUser == null) //sets first cookie
        {
            jsonUser = myUser.setCookies();
        }
        currentUser = JsonConvert.DeserializeObject<myUser>(jsonUser);
    }
}