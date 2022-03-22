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
            Database=stripboek;
            Uid=website;
            Pwd=Test12345;
            Port=3306");
    }
    //db jason uid: website, pwd Test12345
}