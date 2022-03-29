using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class StripboekRepository : DBConnection
{
    public static myStripboek GetOne(int x)
    {
        //dunno of ik de joins netjes heb geimplementeerd
        var parameters = new {Boekid = x};
        /*var sql = @"SELECT (stripboek.Boek_id), (stripboek.Reeks), (stripboek.Titel), (stripboek.ISBN), (stripboek.Jaar_v_Uitgave), (auteur.Naam_Autheur), (tekenaar.Naam_Tekenaar), (stripboek.Uitgever), (stripboek.Afbeelding_urls), (stripboek.Waarde_schatting)
                FROM (website.stripboek)
                LEFT OUTER JOIN (website.geschreven_door) 
                ON (website.stripboek.Boek_id) = (website.geschreven_door.Boek_id)
                LEFT OUTER JOIN (website.getekend_door)
                ON (website.stripboek.Boek_id) = (website.getekend_door.Boek_id)
                JOIN (website.auteur)
                ON (website.geschreven_door.Auteur_id) = (website.auteur.Auteur_id)
                JOIN (website.tekenaar)
                ON (website.getekend_door.Tekenaar_id) = (website.tekenaar.Tekenaar_id)
                WHERE (website.stripboek.Boek_id) = @Boekid LIMIT 1;";*/

        var IBegSqlIsGood =
            @"SELECT (stripboek.Afbeelding_urls), (stripboek.Titel), (stripboek.Reeks), (auteur.Naam_Auteur), (tekenaar.Naam_Tekenaar), (stripboek.Jaar_v_Uitgave), (stripboek.ISBN), (stripboek.Waarde_schatting) 
            FROM (website.stripboek, website.auteur, website.tekenaar, website.geschreven_door, website.getekend_door)
            WHERE (geschreven_door.Boek_id = @Boekid && geschreven_door.Auteur_id = auteur.Auteur_id) && (getekend_door.Boek_id = @Boekid && getekend_door.Tekenaar_id = tekenaar.Tekenaar_id)";

        var sqlStripboek = @"SELECT *
                            FROM (website.stripboek)
                            WHERE Boek_id = @Boekid";

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
        myStripboek one = connection.QuerySingle<myStripboek>(IBegSqlIsGood, parameters);
        one.MyAuteurs = connection.Query<myAuteur>(sqlAuteur, parameters).ToList();
        one.MyTekenaars = connection.Query<myTekenaar>(sqlTekenaar, parameters).ToList();
        
        return one;
    }
    //dont work, QuerySingle geeft een enkele rij terug. Dit werkt dus niet met de lijsten myTekenaars en myAuteurs
    //mogelijke oplossing zou zijn om lijsten appart op te halen en dan in one te zetten.
}