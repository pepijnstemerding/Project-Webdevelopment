namespace WebDevStripboeken.Models;

public class myBezit
{
    public int Gebruiker_id { get; set; }
    public int Boek_id { get; set; }
    public string Locatie { get; set; }
    public int Status_exemplaar { get; set; }
    public double Gekocht_voor { get; set; }
}