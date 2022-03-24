namespace WebDevStripboeken.Repository;

public class CollectieRepository : DBConnection

{
    public static List<myCollectie> GetAll()
    {
        using var connection = Connect();

        IEnumerable<myCollectie> all = connection.Query<myCollecite>(
            @"SELECT *
                FROM (website.collectie)
                WHERE Collectie_naam ='Wishlist');
        return all.ToList();
    }
}