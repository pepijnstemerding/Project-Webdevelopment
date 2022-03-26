using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class SearchRepository : DBConnection
{
    public static List<myStripboek> SearchSubmit(string a, string b)
    {
        var parameters = new {Catagory = a, Query = "%"};
        using var connection = Connect();

        IEnumerable<myStripboek> results = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE @Catagory LIKE @Query ;", parameters);
        return results.ToList();
    }
}