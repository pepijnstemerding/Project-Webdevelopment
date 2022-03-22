using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class ProfileRepository : DBConnection
{
    public static myUser GetOne(string x)
    {
        using var connnection = Connect();

        myUser one = connnection.QuerySingle<myUser>(
            @"SELECT * 
                FROM (website.gebruiker)
               ;");
        return one;
        //WHERE Gebruikersnaam = '" + x + "' LIMIT 1
    }
}