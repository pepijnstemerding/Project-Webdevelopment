using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class CollectieRepository : DBConnection

{
    public static List<myCollectie> giveCollecties(string b)
    {
        var parameters = new {Gebruikersnaam = b};

        var sqlGebrId = @"SELECT gebruiker_id
                    FROM gebruiker
                    WHERE Gebruikersnaam = @gebruikersnaam";

        dynamic gebrId;
        using var connection = Connect();
        {
            gebrId = connection.QuerySingle(sqlGebrId);
        }

        var parameters1 = new {gebruikersid = gebrId};
        //return all1.ToList();   
        return null;
    }

    public static void  CollectieAanMaken(string x)
    {
        var parameters = new {Collectie_naam = x};


        var sql = @"INSERT INTO collectie (Collectie_naam) VALUES (@Colectie_naam)";
            
        using var connection = Connect();
        {
            connection.Execute(sql, parameters);
        }
    }
    
    
    
    
    
}