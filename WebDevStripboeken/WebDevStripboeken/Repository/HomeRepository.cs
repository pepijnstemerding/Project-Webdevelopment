using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class HomeRepository : DBConnection
{
    public static List<myStripboek> GetAll()
    {
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id > 0 AND Boek_id <= 5;");
        return all.ToList();
    }
}