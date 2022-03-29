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
            Uid=website;
            Pwd=Test12345;
            Port=3306");
    }
    //db jason uid: website, pwd Test12345
    //db honor uid: website1, pwd Password123
    //db marten uid: root, pwd Test@1234!
    
    //please god volgende keer neemt iedereen dezelfde uid en pwd XD
}