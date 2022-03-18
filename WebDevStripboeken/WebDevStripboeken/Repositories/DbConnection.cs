using MySql.Data.MySqlClient;

namespace WebDevStripboeken.Repositories;

public class DbConnection
{
    public static MySqlConnection connect()
    {
        // database part of string correct???
        return new MySqlConnection
        ("Server=127.0.0.1;" +
         "Database=MySQL;" +
         "Uid=root;" +
         "Pwd=Test@1234!;" +
         "Port=3306");
    }
    
}