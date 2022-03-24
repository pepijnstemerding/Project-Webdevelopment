using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class HomeRepository : DBConnection
{
    public static List<myStripboek> GetAll(int min)
    {
        //var Min = min;
        //var Max = min + 5;
        var parameters = new {Min = min, Max = min + 5};
        using var connection = Connect();

        IEnumerable<myStripboek> all = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id >= @Min AND Boek_id < @Max;", parameters);
        return all.ToList();
    }
}