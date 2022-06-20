using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Operators;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class StripboekRepository : DBConnection
{
    public static myStripboek GetOne(int x)
    {
        myStripboek one = new myStripboek();
        //one.MyAuteurs = new List<myAuteur>();
        //one.MyTekenaars = new List<myTekenaar>();
        
        var parameters = new {Boekid = x};

        /*var IBegSqlIsGood =
            @"SELECT (stripboek.Afbeelding_urls), (stripboek.Titel), (stripboek.Reeks), (auteur.Naam_Auteur), (tekenaar.Naam_Tekenaar), (stripboek.Jaar_v_Uitgave), (stripboek.ISBN), (stripboek.Waarde_schatting) 
            FROM (stripboek, auteur, tekenaar, geschreven_door, getekend_door)
            WHERE (geschreven_door.Boek_id = @Boekid AND geschreven_door.Auteur_id = auteur.Auteur_id) AND (getekend_door.Boek_id = @Boekid AND getekend_door.Tekenaar_id = tekenaar.Tekenaar_id)";*/

        var secondtry = @"SELECT *
                        FROM stripboek
                        WHERE Boek_id = @Boekid";

        var sqlAuteur = @"SELECT Naam_Auteur
                        FROM geschreven_door
                        JOIN auteur
                        ON geschreven_door.Auteur_id = auteur.Auteur_id
                        WHERE geschreven_door.Boek_id = @Boekid;";

        var sqlTekenaar = @"SELECT Naam_Tekenaar
                            FROM getekend_door
                            JOIN tekenaar
                            ON getekend_door.Tekenaar_id = tekenaar.Tekenaar_id
                            WHERE getekend_door.Boek_id = @Boekid;";

        using var connection = Connect();
        {
            one = connection.QuerySingleOrDefault<myStripboek>(secondtry, parameters);          //System.InvalidOperationException: Sequence contains no elements
            one.MyAuteurs = connection.Query<myAuteur>(sqlAuteur, parameters).ToList();         //System.NullReferenceException: Object reference not set to an instance of an object.
            one.MyTekenaars = connection.Query<myTekenaar>(sqlTekenaar, parameters).ToList();
        }
        return one;
    }

    public static bool addToCollection(int collId, int BoekId, string Gebruikersnaam)
    {
        try
        {
            var par = new { Boekid = BoekId, Collid = collId, Gebruiker = Gebruikersnaam };
            var sql = @"INSERT INTO zit_in (boek_id, collectie_id, gebruiker_id)
                        VALUES (@Boekid, @Collid, (
                        SELECT Gebruiker_id FROM gebruiker WHERE Gebruikersnaam = @Gebruiker));";

            using var connection = Connect();
            {
                connection.Execute(sql, par);
            }
            return true;
        }
        catch (MySqlException)
        {
            return false;
        }
        
    }
}