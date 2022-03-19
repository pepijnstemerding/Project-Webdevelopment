using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevStripboeken.Repository;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Pages;

public class Stripboek : PageModel
{
    public int Boekid { get; set; }
    public myStripboek boek;
    public void OnGet([FromRoute]int boekid)
    {
        Boekid = boekid;
        //boek = StripboekRepository.GetOne(boekid);
    }
}