namespace WebDevStripboeken.Models;

public class myCollectie
{
    public int Collectie_id { get; set; }
    public string Collectie_naam { get; set; }
    public object? Gebruikersnaam { get; set; }

    /*public myCollectie(int x, string y, string z)
    {
        Collectie_id = x;
        Collectie_naam = y;
        Gebruikersnaam = z;
    }*/
}
