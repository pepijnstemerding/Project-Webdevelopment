using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class CollectieRepository : DBConnection

{
    public static List<myCollectie> GetAll()
    {
        using var connection = Connect();

        IEnumerable<myCollectie> all1 = connection.Query<myCollectie>(
            @"SELECT *
                FROM (website.collectie)
                WHERE Collectie_naam = 'Wishlist';");
        return all1.ToList();
    }
}