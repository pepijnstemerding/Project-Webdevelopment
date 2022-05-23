using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class CollectieRepository : DBConnection

{
    public static List<myCollectie> giveCollecties (string b)
    {
        var parameters = new {Gebruikersnaam = b};
        
        /*List<myCollectie> all1 = new List<myCollectie>();

        var sqlGebrId = @"SELECT gebruiker_id
                    FROM gebruiker
                    WHERE Gebruikersnaam = @gebruikersnaam";

        dynamic gebrId;
        using var connection = Connect();
        {
            gebrId = connection.QuerySingle<int>(sqlGebrId, parameters);
        }
        
        var parameters1 = new {gebruikersid = gebrId};
        var sqlCollectieId = @"SELECT DISTINCT collectie_id
                    FROM zit_in
                    WHERE gebruiker_id = @gebruikersid";

        IEnumerable<int> collId;
        using var connection1 = Connect();
        {
            collId = connection1.QueryFirstOrDefault(sqlCollectieId, parameters1);

            foreach (var collectionid in collId)
            {
                var parameters2 = new {collectieId = collectionid};
                var sqlCollectieNamen = @"SELECT collectie_naam
                                        FROM collectie
                                        WHERE collectie_id = @collectieId";
                all1.Add(connection1.QuerySingle(sqlCollectieNamen, parameters2));  
            }
        }
        return all1;*/

        using var connection = Connect();
        IEnumerable<myCollectie> all = connection.Query<myCollectie>(
            @"SELECT c.Collectie_id, c.Collectie_naam
                FROM collectie c
                JOIN zit_in z 
                ON c.Collectie_id = z.Collectie_id
                JOIN gebruiker g on z.Gebruiker_id = g.Gebruiker_id
                WHERE g.Gebruikersnaam = @Gebruikersnaam", parameters);

        return all.ToList();
    }

    public static void maakCollectieAan (myCollectie x)
    {
        var sql = @"INSERT INTO collectie 
            (Collectie_id, Collectie_naam) 
            VALUES (@Collectie_id, @Collectie_naam)";
        
    }
}