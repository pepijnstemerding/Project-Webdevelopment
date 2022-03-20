using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class LogInRepository : DBConnection
{
    public static myUser chicken;
    public static bool checkLogIn(string a, string b)
    {
        using var connection = Connect();

        IEnumerable<myUser> check = connection.Query<myUser>(
            @"SELECT Gebruikersnaam, Wachtwoord
                FROM (website.gebruiker)
                WHERE Gebruikersnaam = " + a + ";");
        check.ToList();
        foreach (myUser pig in check)
        { chicken = pig; }
        //MySqlException: Unknown column 'Bas' in 'where clause'
        if (chicken.passWord == b)
        {
            return true;
        }
        return false;
    }
}