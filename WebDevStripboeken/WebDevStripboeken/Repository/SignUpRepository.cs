using Dapper;
namespace WebDevStripboeken.Repository;

public class SignUpRepository : DBConnection
{
    public void ietsvoorlater()
    {
        using var connection = Connect();
        
    }
}