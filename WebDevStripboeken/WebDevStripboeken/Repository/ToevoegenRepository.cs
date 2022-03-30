using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class ToevoegenRepository : DBConnection
{
    public static void AddOne(myStripboek x, List<string> y, List<string> z)
    {
        var par = new
        {
            Reeks = x.Reeks, Titel = x.Titel, ISBN = x.ISBN, Jaar = x.Jaar_v_Uitgave, Uitgever = x.Uitgever,
            Afbeelding = x.Afbeelding_urls, Waarde = x.Waarde_schatting
        };
        
        var sql = @"INSERT INTO website.stripboek 
            (Reeks, Titel, ISBN, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) 
            VALUES (@Reeks, @Titel, @ISBN, @Jaar, @Uitgever, @Afbeelding, @Waarde);";

        
        var sqlAuteur = @"INSERT INTO website.auteur
                        (Naam_Auteur)
                        VALUE (@Auteur);";

        var sqlTekenaar = @"INSERT INTO website.tekenaar
                            (Naam_Tekenaar)
                            VALUE (@Tekenaar);";

        // var parTekenaar = new {Tekenaar = x.MyTekenaars[0]};
        // var sqlTekenaar = @"INSERT INTO website.tekenaar
        //     (Naam_Tekenaar)
        //     VALUE (@Tekenaar)";
        
        using var connection = Connect();
        {
            connection.Execute(sql, par);
            if (y.Count > 0)
            {
                foreach (string auteur in y)
                {
                    var parAuteur = new {Auteur = auteur};
                    connection.Execute(sqlAuteur, parAuteur);
                }
            }

            if (z.Count > 0)
            {
                foreach (string tekenaar in z)
                {
                    var parTekenaar = new {Tekenaar = tekenaar};
                    connection.Execute(sqlTekenaar, parTekenaar);
                }
            }
            //connection.Execute(sqlTekenaar, parTekenaar);
        }
        
    }
}