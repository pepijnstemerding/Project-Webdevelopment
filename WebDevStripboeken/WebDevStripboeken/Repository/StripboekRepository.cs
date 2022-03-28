using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class StripboekRepository : DBConnection
{
    public static myStripboek GetOne(int x)
    {
        var parameters = new {Boekid = x};
        using var connection = Connect();
        myStripboek one = connection.QuerySingle<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id = @Boekid LIMIT 1;", parameters);
        return one;
    }
}