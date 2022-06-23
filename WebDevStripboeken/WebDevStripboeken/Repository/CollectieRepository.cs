using System.Collections;
using Dapper;
using Org.BouncyCastle.Math.EC;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class CollectieRepository : DBConnection

{
    public static List<myCollectie> giveCollecties (string b)
    {
        List<myCollectie> methodresult = new List<myCollectie>();

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
        List<int> collId = new List<int>();
        using var connection1 = Connect();
        {
            collId = connection1.QueryFirstOrDefault<IEnumerable<int>>(sqlCollectieId, parameters1);

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
        IEnumerable<myCollectie> dbresult = connection.Query<myCollectie>(
            @"SELECT c.Collectie_id, c.Collectie_naam
                FROM collectie c
                JOIN zit_in z 
                ON c.Collectie_id = z.Collectie_id
                JOIN gebruiker g on z.Gebruiker_id = g.Gebruiker_id
                WHERE g.Gebruikersnaam = @Gebruikersnaam", parameters);
        dbresult.ToList();
        
        //Filtering of duplicates out of list given by db
        foreach (myCollectie collectie in dbresult)
        {
            if (methodresult.Count == 0)
            {
                methodresult.Add(collectie);
            }
            else
            {
                if (methodresult.Last().Collectie_id != collectie.Collectie_id)
                {
                    methodresult.Add(collectie);
                }
            }
        }
        return methodresult;
    }

    public static List<myStripboek> giveBooks(int CollectionID)
    {
        var par = new {id = CollectionID};
        string sql = @"SELECT *
                FROM stripboek s
                JOIN zit_in z
                ON s.Boek_id = z.Boek_id
                WHERE z.Collectie_id = @id AND s.Goedgekeurd = 1;";
        using var connection = Connect();
        
        IEnumerable<myStripboek> all = connection.Query<myStripboek>(sql, par);
        return all.ToList();
    }

    public static string giveName(int collID)
    {
        string name;
        
        var par = new {id = collID};
        string sql = @"SELECT Collectie_naam 
                FROM collectie
                WHERE Collectie_id = @id;";
        using var connection = Connect();
        {
            name = connection.QuerySingle<string>(sql, par);
        }
        return name;
    }

    public static void CollectieAanMaken(string x, string gebrnaam)
    {
    
        var parameters = new {Collectie_naam = x};
        var naam = new {gebrnaam = gebrnaam};
        var sql = @"INSERT INTO Collectie (Collectie_naam) VALUES (@Collectie_naam)";
            
        using var connection = Connect();
        {
            connection.Execute(sql, parameters);
        }
        var gebridsql = @"SELECT gebruiker_id FROM gebruiker WHERE Gebruikersnaam = @gebrnaam";
        var collectiesql =
            @"INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (1, LAST_INSERT_ID(), @id)"; // een default boek (id = 1) toevoegen zodat de collectie weergeven wordt
        using var connection2 = Connect();
        {
            int userid = connection2.Execute(gebridsql, naam);
            var id = new {id = userid};
            connection2.Execute(collectiesql, id);
        }
    }
}