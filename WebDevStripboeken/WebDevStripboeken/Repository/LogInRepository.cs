using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class LogInRepository : DBConnection
{
    /// <summary>
    /// Haalt op Gebruikersdata op basis van megegeven Gebruikersnaam, vergelijkt daarna opgehaalde WW en megegeven WW
    /// </summary>
    /// <param name="a">Megegeven Gebruikersnaam</param>
    /// <param name="b">Megegeven Wachtwoord</param>
    /// <returns>True/False (inloggen gelukt/inloggen niet gelukt)</returns>
    public static bool checkLogIn(string a, string b)
    {
        var parameters = new {Gebruikersnaam = a};
        using var connection = Connect();
        try
        {
            var check = connection.QuerySingle<myUser>(
                @"SELECT Gebruikersnaam, Wachtwoord
                FROM (gebruiker)
                WHERE Gebruikersnaam = @Gebruikersnaam LIMIT 1;", parameters);
            if (check.Wachtwoord == b)
            { return true; }
            return false;
        }
        catch (InvalidOperationException)
        { return false; }
    }
}