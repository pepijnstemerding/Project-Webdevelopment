using System.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebDevStripboeken.Repository;

public class DBConnection
{
    public static MySqlConnection Connect()
    {
        return new MySqlConnection(
            @"Server=127.0.0.1;
            Database=website;
            Uid=root;
            Pwd=Test@1234!;
            Port=3306");
    }
    /*Marten : Database=website
     *         Uid=root;
     *         Pwd=Test@1234!; 
     */
}