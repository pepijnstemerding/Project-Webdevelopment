using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class HomeRepository : DBConnection
{
    public static List<myStripboek> GetAll(params object[] args)
    {
        // Ik weet niet hoe ik het anders moet doen
        // C# optionele variabelen zijn echt slecht te implementeren
        var min = (int) args[0];
        myUser? user;
        if (args.Length == 1)
            user = null;
        else
            user = (myUser) args[1];
        var parameters = new {Min = min, Max = min + 5};
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id >= @Min AND Boek_id < @Max AND Goedgekeurd = 1;", parameters);
        return all.ToList();
    }
}