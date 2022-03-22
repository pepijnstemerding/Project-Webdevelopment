using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class LogInRepository : DBConnection
{
    //public static myUser chicken;
    //public static string crow;
    //public myUser check;
    public static bool checkLogIn(string a, string b)
    {
        using var connection = Connect();
        try
        {
            var check = connection.QuerySingle<myUser>(
                @"SELECT Gebruikersnaam, Wachtwoord
                FROM (website.gebruiker)
                WHERE Gebruikersnaam = '" + a + "' LIMIT 1;");
            if (check.Wachtwoord == b)
            { return true; }
            return false;
        }
        catch (InvalidOperationException)
        { return false; }
    }
}