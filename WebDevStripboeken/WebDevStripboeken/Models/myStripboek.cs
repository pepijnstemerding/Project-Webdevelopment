using System.ComponentModel.DataAnnotations;

namespace WebDevStripboeken.Models;

public class myStripboek
{
    public int Boek_id { get; set; }

    [Required]
    public string Reeks { get; set; }

    [Required] 
    public string Titel { get; set; }

    [Required]
    public string ISBN { get; set; }

    [Required]
    [Range(1000,9999)]
    public int Jaar_v_Uitgave { get; set; }

    //[Required]
    //public List<myAuteur> MyAuteurs { get; set; }
    
    //[Required]
    //public List<myTekenaar> MyTekenaars { get; set; }

    [Required]
    public string Uitgever { get; set; }

    [Required]
    [Url]
    public string Afbeelding_urls { get; set; }        //meervoud so maybe List<string>?    Eet db dat gewoon?
    
    public double Waarde_schatting { get; set; }

}