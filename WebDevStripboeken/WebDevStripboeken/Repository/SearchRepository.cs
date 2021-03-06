using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class SearchRepository : DBConnection
{
    public static List<myStripboek> SearchSubmit(string a, string b)
    {
        var parameters = new {Query = b};
        using var connection = Connect();
        //controle a
        if (a.Contains("--") || a.Contains('"') || a.Contains(' ') || a.Contains("'"))
        {
            return new List<myStripboek>();
        }
        if (a == "Reeks" || a == "Titel" || a == "Uitgever" || a == "Jaar van Uitgave")
        {
            IEnumerable<myStripboek> results = connection.Query<myStripboek>(
                $@"SELECT *
                FROM (stripboek)
                WHERE {a} LIKE CONCAT('%', @Query, '%');", parameters);
            // ----> param voor category pruimt ie niet?? <----
            return results.ToList();
        }
        if (a == "Auteur")
        {
            IEnumerable<myStripboek> results = connection.Query<myStripboek>(
                @"SELECT *
                    FROM (stripboek)
                    JOIN (geschreven_door)
                    ON (geschreven_door.Boek_id) = (stripboek.Boek_id)
                    JOIN (auteur)
                    ON (auteur.Auteur_id) = (geschreven_door.Auteur_id)
                    WHERE Naam_Auteur LIKE CONCAT('%', @Query, '%')", parameters);
            return results.ToList();
        }
        if (a == "Tekenaar")
        {
            IEnumerable<myStripboek> results = connection.Query<myStripboek>(
                @"SELECT *
                    FROM (stripboek)
                    JOIN (getekend_door)
                    ON (getekend_door.Boek_id) = (stripboek.Boek_id)
                    JOIN (tekenaar)
                    ON (tekenaar.Tekenaar_id) = (getekend_door.Tekenaar_id)
                    WHERE Naam_Tekenaar LIKE CONCAT('%', @Query, '%');", parameters);
            return results.ToList();
        }
        return new List<myStripboek>();
    }
        //string c = string.Format("{0}" + b  +"{0}", '%');
        //Console.WriteLine(c);
       
        //return results.ToList();
}
