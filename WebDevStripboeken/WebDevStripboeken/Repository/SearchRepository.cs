using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class SearchRepository : DBConnection
{
    public static List<myStripboek> SearchSubmit(string a, string b)
    {
        using var connection = Connect();

        IEnumerable<myStripboek> results = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE " + a + " LIKE '%" + b + "%';");
        return results.ToList();
    }
}