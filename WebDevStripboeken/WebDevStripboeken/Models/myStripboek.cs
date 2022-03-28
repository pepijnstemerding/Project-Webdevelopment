using System.ComponentModel.DataAnnotations;

namespace WebDevStripboeken.Models;

public class myStripboek
{
    public int Boek_id;
    
    [Required]
    public string Reeks;
    
    [Required]
    public string Titel;
    
    [Required]
    public string ISBN;
    
    [Required]
    [MaxLength(4)]
    [MinLength(4)]
    public int Jaar_v_Uitgave;
    
    //[Required]
    //public List<myAuteur> MyAuteurs;
    
    //[Required]
    //public List<myTekenaar> MyTekenaars;
    
    [Required]
    public string Uitgever;

    [Required]
    [Url]
    public string Afbeelding_urls; //meervoud so maybe List<string>?    Eet db dat gewoon?
    
    public double Waarde_schatting;
    
}