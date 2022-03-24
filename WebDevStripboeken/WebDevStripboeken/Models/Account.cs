using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebDevStripboeken.Models;

public class Account
{
    [Required]
    [MinLength(2)]
    [MaxLength(32)]
    public string gebruikersnaam { set; get; }
    
    [Required]
    [EmailAddress]
    public string email { set; get; }
    
    [Required]
    [MinLength(6)]
    [MaxLength(30)]
    public string wachtwoord { set; get; }
    
    [Required]
    [MaxLength(30)]
    [Compare(nameof(wachtwoord), ErrorMessage = "De wachtwoorden komen niet overeen")]
    public string wachtwoordbevestiging { set; get; }
    
    [Required]
    public DateTime geboortedatum { set; get; }
    
    [Required]
    public string beveiligingsvraag { get; set; }
}