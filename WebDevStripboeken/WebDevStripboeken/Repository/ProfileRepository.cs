using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class ProfileRepository : DBConnection
{
    public static myUser GetOne(string x)
    {
        var parameters = new {Gebruikersnaam = x};
        using var connnection = Connect();

        myUser one = connnection.QuerySingle<myUser>(
            @"SELECT * 
                FROM (website.gebruiker)
               WHERE Gebruikersnaam = @Gebruikersnaam LIMIT 1;", parameters);
        return one;
    }
}