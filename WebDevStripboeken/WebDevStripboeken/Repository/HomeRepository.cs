using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class HomeRepository : DBConnection
{
    /// <summary>
    /// Haalt op alle stripboeken binnen de meegegeven min en max
    /// </summary>
    /// <param name="min">megegeven min</param>
    /// <returns>Lijst van stripboeken</returns>
    public static List<myStripboek> GetAll(int min)
    {
        //var Min = min;
        //var Max = min + 5;
        var parameters = new {Min = min, Max = min + 5};
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (stripboek)
                WHERE Boek_id >= @Min AND Boek_id < @Max AND Goedgekeurd = 1;", parameters);
        return all.ToList();
    }
}