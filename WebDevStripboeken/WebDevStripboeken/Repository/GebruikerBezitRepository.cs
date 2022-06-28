using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Operators;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;
namespace WebDevStripboeken.Repository;

public class GebruikerBezitRepository : DBConnection
{
    public static bool  BoekToevoegen(myBezit x, string y)
    {
        var par = new { Gebruiker = y, Boekid = x.Boek_id, Locatie = x.Locatie, Status = x.Status_exemplaar, Gekocht = x.Gekocht_voor};
        var sql = @"INSERT INTO bezit (Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor)
                    VALUES ((SELECT Gebruiker_id FROM gebruiker WHERE Gebruikersnaam = @Gebruiker), 
                    @Boekid, @Locatie, @Status, @Gekocht);";

        try
        {
            using var connection = Connect();
            {
                connection.Execute(sql, par);
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public static bool BoekUpdate(myBezit x, string y)
    {
        var par = new { Locatie = x.Locatie, Status = x.Status_exemplaar, Gekocht = x.Gekocht_voor, Boek = x.Boek_id, Gebruiker = y };
        var sql = @"UPDATE bezit
                    SET Locatie = @Locatie, Status_exemplaar = @Status, Gekocht_voor = @Gekocht
                    WHERE Boek_id = @Boek AND Gebruiker_id = 
                                              (SELECT Gebruiker_id FROM gebruiker 
                                              WHERE Gebruikersnaam = @Gebruiker)";
        try
        {
            using var connection = Connect();
            {
                connection.Execute(sql, par);
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}