using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class SearchRepository : DBConnection
{
    public static List<myStripboek> SearchSubmit(string a, string b)
    {
        //controle a
        
        //string c = string.Format("{0}" + b  +"{0}", '%');
        //Console.WriteLine(c);
        var parameters = new {Query = b};
        using var connection = Connect();

        IEnumerable<myStripboek> results = connection.Query<myStripboek>(
            $@"SELECT *
                FROM (website.stripboek)
                WHERE {a} LIKE CONCAT('%', @Query, '%');", parameters);
        // ----> param voor category pruimt ie niet?? <----
        return results.ToList();
    }
}