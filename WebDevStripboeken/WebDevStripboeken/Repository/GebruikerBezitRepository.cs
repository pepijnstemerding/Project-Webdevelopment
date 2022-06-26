using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Operators;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;
namespace WebDevStripboeken.Repository;

public class GebruikerBezitRepository : DBConnection
{
    public static void  BoekToevoegen(myBezit x)
    {
        var par = new { Gebruiker = x.Gebruiker, Boekid = x.Boek_id, Locatie = x.Locatie, Status = x.Status_exemplaar, Gekocht = x.Gekocht_voor};
        var sql = @"INSERT INTO bezit (Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor)
                    VALUES ((SELECT Gebruiker_id FROM gebruiker WHERE Gebruikersnaam = @Gebruiker), 
                    @Boekid, @Locatie, @Status, @Gekocht);";

        using var connection = Connect();
        {
            connection.Execute(sql, par);
        }
    }
}