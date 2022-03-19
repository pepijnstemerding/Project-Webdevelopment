namespace WebDevStripboeken.Models;

public class myStripboek
{
    public int Boek_id;
    public string Reeks;
    public string Titel;
    public string ISBN;
    public int Jaar_v_Uitgave;
    //public List<myAuteur> MyAuteurs;
    //public List<myTekenaar> MyTekenaars;
    public string Uitgever;
    //public string Locatie;
    public string Afbeelding_urls; //meervoud so maybe List<string> ?
    public double Waarde_schatting;
    
}