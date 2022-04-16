using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class CollectieRepository : DBConnection

{
    public static List<myCollectie> giveCollecties (string b)
    {
        var parameters = new {Gebruikersnaam = b};
        using var connection = Connect();

        var sql = @"SELECT ";

        IEnumerable<myCollectie> all1 = connection.Query<myCollectie>(
            @"SELECT Collectie_naam
                FROM (collectie), (gebruiker)
                WHERE Gebruikersnaam = @Gebruikersnaam LIMIT 1;", parameters);
        return all1.ToList();
    }
}