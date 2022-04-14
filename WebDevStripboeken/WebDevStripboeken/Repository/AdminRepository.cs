using WebDevStripboeken.Models;
using Dapper;

namespace WebDevStripboeken.Repository;

public class AdminRepository : DBConnection
{
    /// <summary>
    /// Haalt alle goedgekeurde stripboeken op uit de database
    /// </summary>
    /// <returns>Lijst van alle goed gekeurde stripboeken</returns>
    public static List<myStripboek> GetBoeken()
    {
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Goedgekeurd = 0;");
        return all.ToList();
    }

    /// <summary>
    /// Haalt alle myUsers op uit de database
    /// </summary>
    /// <returns>Lijst van alle Gebruikers</returns>
    public static List<myUser> GetUsers()
    {
        using var connection = Connect();
        
        IEnumerable<myUser> all = connection.Query<myUser>(
            @"SELECT *
                FROM (website.gebruiker);");
        return all.ToList();
    }

    /// <summary>
    /// Verwijdert een Gebruiker op basis van het megegeven Gebruikerid
    /// </summary>
    /// <param name="Gebruikerid">Gebruikerid van de gebruiker die verwijdert zal worden</param>
    public static void delUser(int Gebruikerid)
    {
        using var connection = Connect();
        var param = new {Gebruikerid = Gebruikerid};

        int x = connection.Execute(
            @"DELETE 
                FROM website.gebruiker
                WHERE Gebruiker_id =@Gebruikerid;", param);
    }

    /// <summary>
    /// Verwijdert boek op basis van het megegeven Boek id
    /// </summary>
    /// <param name="Boekid">Boek_id van het boek dat verwijdert gaat worden</param>
    public static void delBoek(int Boekid)
    {
        using var connection = Connect();
        var param = new {Boekid = Boekid};

        int x = connection.Execute(
            @"DELETE
                FROM website.stripboek
                WHERE Boek_id = @Boekid", param);
    }

    /// <summary>
    /// Accepteert boek op basis van megegeven Boek id
    /// </summary>
    /// <param name="Boekid">Boek_id van het boek dat geaccepteert gaat worden</param>
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