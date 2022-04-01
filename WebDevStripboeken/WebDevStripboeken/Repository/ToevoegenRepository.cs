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
            VALUES (@Reeks, @Titel, @ISBN, @Jaar, @Uitgever, @Afbeelding, @Waarde)";

        
        var sqlAuteur = @"INSERT INTO website.auteur
                        (Naam_Auteur)
                        VALUE (@Auteur);";

        var sqlTekenaar = @"INSERT INTO website.tekenaar
                            (Naam_Tekenaar)
                            VALUE (@Tekenaar);";
        
        var sqlSelectAuteurID = @"SELECT auteur.auteur_id FROM website.auteur WHERE Naam_Auteur = @Auteur;";
        
        var sqlSelectTekenaarID = @"SELECT tekenaar.Tekenaar_id FROM website.tekenaar WHERE Naam_Tekenaar = @Tekenaar;";
            
        var sqlTekenaarStripID = @"INSERT INTO website.getekend_door (Boek_id, Tekenaar_id) VALUES (@Stripid, @Tekenaarid);";
        
        var sqlAuteurStripID = @"INSERT INTO website.geschreven_door (Boek_id, Auteur_id) VALUES (@Stripid, @Auteurid);";

        
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
        
        using var connection2 = Connect();
        {
            var id_s = connection2.QuerySingle<int>(
                @"SELECT stripboek.Boek_id FROM website.stripboek WHERE ISBN = @ISBN AND Titel = @Titel", par);
            
            if (y.Count > 0)
            {
                foreach (string auteur in y)
                {
                    var parAuteur = new {Auteur = auteur};
                    var id_a = connection2.QuerySingle<int>(sqlSelectAuteurID, parAuteur);
                    
                    var parids = new {Auteurid = id_a, Stripid = id_s};
                    connection2.Execute(sqlAuteurStripID, parids);
                }
            }
            
            if (z.Count > 0)
            {
                foreach (string tekenaar in z)
                {
                    var parTekenaar = new {Tekenaar = tekenaar};
                    var id_t = connection2.QuerySingle<int>(sqlSelectTekenaarID, parTekenaar);
                    
                    var parids = new {Tekenaarid = id_t, Stripid = id_s};
                    connection2.Execute(sqlTekenaarStripID, parids);
                }
            }
        }
        
    }
}