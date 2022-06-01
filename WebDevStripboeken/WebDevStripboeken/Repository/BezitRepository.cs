using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class BezitRepository : DBConnection
{
    public static myBezit UserSpecificStripboekData(int Boek_id, string gebruiker_naam)
    {
        using var connection = Connect();
        Console.WriteLine($"Boek ID {Boek_id} Gebruiker: {gebruiker_naam}");
        int gebruiker_id = connection.QueryFirst<int>(
            @"SELECT Gebruiker_id
                FROM gebruiker
                WHERE Gebruikersnaam = @gebruiker_naam",
            new {gebruiker_naam = gebruiker_naam}
        );
        return connection.QueryFirstOrDefault<myBezit>(
            @"SELECT *
                 FROM bezit
                 WHERE gebruiker_id = @gebruiker_id AND Boek_id = @Boek_id",
            new { Boek_id = Boek_id, gebruiker_id = gebruiker_id }
        );
    }
}