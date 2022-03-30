using WebDevStripboeken.Models;
using Dapper;

namespace WebDevStripboeken.Repository;

public class AdminRepository : DBConnection
{
    public static List<myStripboek> GetBoeken()
    {
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Goedgekeurd = 0;");
        return all.ToList();
    }

    public static List<myUser> GetUsers()
    {
        using var connection = Connect();
        
        IEnumerable<myUser> all = connection.Query<myUser>(
            @"SELECT *
                FROM (website.gebruiker);");
        return all.ToList();
    }

    public static void delUser(int Gebruikerid)
    {
        using var connection = Connect();
        var param = new {Gebruikerid = Gebruikerid};

        int x = connection.Execute(
            @"DELETE 
                FROM website.gebruiker
                WHERE Gebruiker_id =@Gebruikerid;", param);
    }

    public static void delBoek(int Boekid)
    {
        using var connection = Connect();
        var param = new {Boekid = Boekid};

        int x = connection.Execute(
            @"DELETE
                FROM website.stripboek
                WHERE Boek_id = @Boekid", param);
    }

    public static void accBoek(int Boekid)
    {
        using var connection = Connect();
        var param = new {Boekid = Boekid};

        int x = connection.Execute(
            @"UPDATE website.stripboek
                SET Goedgekeurd = 1
                WHERE Boek_id = @Boekid", param);
    }
}