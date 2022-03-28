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
        using var connection = Connect();
        myStripboek one = connection.QuerySingle<myStripboek>(
            @"SELECT (stripboek.Boek_id), (stripboek.Reeks), (stripboek.Titel), (stripboek.ISBN), (stripboek.Jaar_v_Uitgave), (auteur.Naam_Autheur), (tekenaar.Naam_Tekenaar), (stripboek.Uitgever), (stripboek.Afbeelding_urls), (stripboek.Waarde_schatting)
                FROM (website.stripboek)
                LEFT OUTER JOIN (website.geschreven_door) 
                ON (website.stripboek.Boek_id) = (website.geschreven_door.Boek_id)
                LEFT OUTER JOIN (website.getekend_door)
                ON (website.stripboek.Boek_id) = (website.getekend_door.Boek_id)
                JOIN (website.auteur)
                ON (website.geschreven_door.Auteur_id) = (website.auteur.Auteur_id)
                JOIN (website.tekenaar)
                ON (website.getekend_door.Tekenaar_id) = (website.tekenaar.Tekenaar_id)
                WHERE (website.stripboek.Boek_id) = @Boekid LIMIT 1;", parameters);
        return one;
    }
    //dont work, QuerySingle geeft een enkele rij terug. Dit werkt dus niet met de lijsten myTekenaars en myAuteurs
    //mogelijke oplossing zou zijn om lijsten appart op te halen en dan in one te zetten.
}