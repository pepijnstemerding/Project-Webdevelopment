using Dapper;
using System;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class IndexRepository : DBConnection
{
    public static List<myStripboek> GetAll(int min)
    {
        var parameters = new {Min = min, Max = min + 5};
        using var connection = Connect();

        IEnumerable<myStripboek> all3 = connection.Query<myStripboek>(
            @"SELECT *
                FROM (website.stripboek)
                WHERE Boek_id >= @Min AND Boek_id < @Max;", parameters);
        return all3.ToList();
    }
}
