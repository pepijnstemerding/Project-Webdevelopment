using System.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebDevStripboeken.Repository;

public class DBConnection
{
    public static MySqlConnection Connect()
    {
        return new MySqlConnection(
            @"Server=localhost;
            Database=website;
            Uid=root;
            Pwd=admin;
            Port=3306"
        );
    }
    //db jason uid: website, pwd Test12345
    //db honor uid: website1, pwd Password123
    //db marten uid: root, pwd Test@1234!
    //db pepijn uid: root, pwd admin
    
    //please god volgende keer neemt iedereen dezelfde uid en pwd XD
}