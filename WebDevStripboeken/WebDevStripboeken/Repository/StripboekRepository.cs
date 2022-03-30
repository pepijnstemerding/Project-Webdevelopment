using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class StripboekRepository : DBConnection
{
    public static myStripboek GetOne(int x)
    {
        var parameters = new {Boekid = x};

        var IBegSqlIsGood =
            @"SELECT (stripboek.Afbeelding_urls), (stripboek.Titel), (stripboek.Reeks), (auteur.Naam_Auteur), (tekenaar.Naam_Tekenaar), (stripboek.Jaar_v_Uitgave), (stripboek.ISBN), (stripboek.Waarde_schatting) 
            FROM (website.stripboek, website.auteur, website.tekenaar, website.geschreven_door, website.getekend_door)
            WHERE (geschreven_door.Boek_id = @Boekid && geschreven_door.Auteur_id = auteur.Auteur_id) && (getekend_door.Boek_id = @Boekid && getekend_door.Tekenaar_id = tekenaar.Tekenaar_id)";

        var sqlAuteur = @"SELECT Naam_Auteur
                        FROM (website.geschreven_door)
                        JOIN (website.auteur)
                        ON (geschreven_door.Auteur_id) = (auteur.Auteur_id)
                        WHERE geschreven_door.Boek_id = @Boekid;";

        var sqlTekenaar = @"SELECT Naam_Tekenaar
                            FROM (website.getekend_door)
                            JOIN (website.tekenaar)
                            ON getekend_door.Tekenaar_id = tekenaar.Tekenaar_id
                            WHERE getekend_door.Boek_id = @Boekid;";
        
        using var connection = Connect();
        myStripboek one = connection.QueryFirst<myStripboek>(IBegSqlIsGood, parameters);
        one.MyAuteurs = connection.Query<myAuteur>(sqlAuteur, parameters).ToList(); 
        one.MyTekenaars = connection.Query<myTekenaar>(sqlTekenaar, parameters).ToList();
        
        return one;
    }
}