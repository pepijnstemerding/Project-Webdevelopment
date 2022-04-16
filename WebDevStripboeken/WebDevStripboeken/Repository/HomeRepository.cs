using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class HomeRepository : DBConnection
{
    /// <summary>
    /// Haalt op alle stripboeken binnen de megegeven min en max
    /// </summary>
    /// <param name="min">megegeven min</param>
    /// <returns>Lijst van stripboeken</returns>
    public static List<myStripboek> GetAll(params object[] args)
    {
        // Ik weet niet hoe ik het anders moet doen
        // C# optionele variabelen zijn echt slecht te implementeren
        var min = (int) args[0];
        myUser? user = null;
        if (args.Length == 2)
            user = (myUser) args[1];
        var parameters = new {Min = min};
        using var connection = Connect();

        if (user != null)
        {
            IEnumerable<myStripboek> from_user = connection.Query<myStripboek>(
                @"SELECT stripboek.*
                 FROM (bezit)
                  INNER JOIN stripboek ON bezit.Boek_id = stripboek.Boek_id
                 WHERE stripboek.Goedgekeurd = 1
                  AND bezit.Gebruiker_id = 4
                 LIMIT @Min, 5;", parameters);
            return from_user.ToList(); 
        }
        else
        {
            IEnumerable<myStripboek> all = connection.Query<myStripboek>(
                @"SELECT *
                FROM (stripboek)
                LIMIT @Min, 5;", parameters);
            return all.ToList();
        }
    }
}