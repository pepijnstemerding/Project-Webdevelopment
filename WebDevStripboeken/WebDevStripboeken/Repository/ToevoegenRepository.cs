using Dapper;

namespace WebDevStripboeken.Repository;

public class ToevoegenRepository : DBConnection
{
    public static void AddOne()
    {
        using var connection = Connect();

        int exc = connection.Execute(
            @"INSERT INTO website.stripboek 
            (Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) 
            VALUE ();");
    }
}