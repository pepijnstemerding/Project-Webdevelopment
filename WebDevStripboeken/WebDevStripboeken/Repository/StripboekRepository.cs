using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class StripboekRepository : DBConnection
{
    public static myStripboek GetOne(int x)
    {
        using var connection = Connect();
        myStripboek one = connection.QuerySingle<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id = " + x + " LIMIT 1;");
        return one;
    }
}