using System.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebDevStripboeken.Repository;

public class DBConnection
{
    protected IDbConnection Connect()
    {
        return new MySqlConnection(
            @"Server=127.0.0.1;
            Database=stripboekdatabase;
            Uid=root;
            Pwd=j9muMb2002;
            Port=3306");
    }
}