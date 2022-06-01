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

        var sql = @"INSERT INTO stripboek 
            (Reeks, Titel, ISBN, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) 
            VALUES (@Reeks, @Titel, @ISBN, @Jaar, @Uitgever, @Afbeelding, @Waarde)";
        
        
        using var connection = Connect();
        {
            connection.Execute(sql, par);
        }
    }

    public static void ToevoegenTekenaars(List<string> z)
    {
        var sqlTekenaar = @"INSERT INTO tekenaar
                            (Naam_Tekenaar)
                            VALUE (@Tekenaar);";
        
        var sqlSelectTekenaarID = @"SELECT tekenaar.Tekenaar_id FROM tekenaar WHERE Naam_Tekenaar = @Tekenaar;";
            
        var sqlTekenaarStripID = @"INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES ((SELECT MAX(Boek_id) FROM Stripboek), @Tekenaarid);";

        using var connectionTekenaar = Connect();
        {
            if (z.Count > 0)
            {
                foreach (string tekenaar in z)
                {
                    var parTekenaar = new {Tekenaar = tekenaar};
                    connectionTekenaar.Execute(sqlTekenaar, parTekenaar);
                }
            }
        }

        using var connectionTekenaar2 = Connect();
        {
            if (z.Count > 0)
            {
                foreach (string tekenaar in z)
                {
                    var parTekenaar = new {Tekenaar = tekenaar};
                    var id_t = connectionTekenaar2.QuerySingle<int>(sqlSelectTekenaarID, parTekenaar);

                    var parids = new {Tekenaarid = id_t};
                    connectionTekenaar2.Execute(sqlTekenaarStripID, parids);
                }
            }
        }
    }

    public static void toevoegenAuteurs(List<string> y)
    {
        var sqlAuteur = @"INSERT INTO auteur
                        (Naam_Auteur)
                        VALUE (@Auteur);";
        
        var sqlSelectAuteurID = @"SELECT auteur.auteur_id FROM auteur WHERE Naam_Auteur = @Auteur;";
            
        var sqlAuteurStripID = @"INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES ((SELECT MAX(Boek_id) FROM Stripboek), @Auteurid);";
        
        using var connectionAuteur = Connect();
        {
            if (y.Count > 0)
            {
                foreach (string auteur in y)
                {
                    var parAuteur = new {Auteur = auteur};
                    connectionAuteur.Execute(sqlAuteur, parAuteur);
                }
            }
        }

        using var connectionAuteur2 = Connect();
        {
            if (y.Count > 0)
            {
                foreach (string auteur in y)
                {
                    var parAuteur = new {Auteur = auteur};
                    var id_a = connectionAuteur2.QuerySingle<int>(sqlSelectAuteurID, parAuteur);

                    var parids = new {Auteurid = id_a};
                    connectionAuteur2.Execute(sqlAuteurStripID, parids);
                }
            }

        }
    }
}