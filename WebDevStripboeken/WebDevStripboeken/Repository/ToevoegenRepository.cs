using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class ToevoegenRepository : DBConnection
{
    public static void AddOne(myStripboek x)
    {
        var par = new
        {
            Reeks = x.Reeks, Titel = x.Titel, ISBN = x.ISBN, Jaar = x.Jaar_v_Uitgave, Uitgever = x.Uitgever,
            Afbeelding = x.Afbeelding_urls, Waarde = x.Waarde_schatting
        };
        using var connection = Connect();

        int exc = connection.Execute(
            @"INSERT INTO website.stripboek 
            (Reeks, Titel, ISBN, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) 
            VALUE (@Reeks, @Titel, @ISBN, @Jaar, @Uitgever, @Afbeelding, @Waarde);", par);
    }
}