using Dapper;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Pages;

namespace WebDevStripboeken.Repository;

public class StripboekRepository : DBConnection
{
    //public static myStripboek GetOne(int x)
    //{
        /*using MySqlConnection connection = Connect();
        connection.Open();

        using (MySqlCommand command = new MySqlCommand(
                   @"SELECT * 
                            FROM (website.stripboek)
                            WHERE Boek_id = @x;", connection))
        { command.ExecuteReader();};
        connection.Close();
        
        
        using var connection = Connect();
        IEnumerable<myStripboek> one = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id = @x;");
        return one;*/
    //}
}