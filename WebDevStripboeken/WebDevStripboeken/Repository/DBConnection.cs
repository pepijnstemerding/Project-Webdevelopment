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
            Uid=website1;
            Pwd=Password123;
            Port=3306");
    }
    //db jason uid: website, pwd Test12345
    //db honor uid: website1, pwd Password123
}