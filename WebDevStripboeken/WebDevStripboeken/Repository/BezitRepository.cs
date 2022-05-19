using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class BezitRepository : DBConnection
{
    public static myBezit UserSpecificStripboekData(int Boek_id, int Gebruiker_id)
    {
        var parameters = new { Boek_id = Boek_id, Gebruiker_id = Gebruiker_id };
        using var connection = Connect();
        return connection.QueryFirstOrDefault<myBezit>(
            @"SELECT *
                 FROM bezit
                 WHERE Gebruiker_id = @Gebruiker_id AND Boek_id = @Boek_id",
            parameters
        );
    }
}