using WebDevStripboeken.Models;
using Dapper;

namespace WebDevStripboeken.Repository;

public class AdminRepository : DBConnection
{
    public static List<myStripboek> GetBoeken()
    {
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Goedgekeurd = 0;");
        return all.ToList();
    }

    public static List<myUser> GetUsers()
    {
        using var connection = Connect();
        
        IEnumerable<myUser> all = connection.Query<myUser>(
            @"SELECT *
                FROM (website.gebruiker);");
        return all.ToList();
    }
}