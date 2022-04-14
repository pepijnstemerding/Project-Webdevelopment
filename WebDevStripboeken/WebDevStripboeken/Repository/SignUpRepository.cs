using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebDevStripboeken.Models;
namespace WebDevStripboeken.Repository;

public class SignUpRepository : DBConnection
{
    /// <summary>
    /// Voegt Gebruiker toe op basis van megegeven account
    /// </summary>
    /// <param name="account">Megegeven Account</param>
    public static void SignUp(Account account)
    {
        var par = new {gebruikersnaam = account.gebruikersnaam, email = account.email, wachtwoord = account.wachtwoord, geboortedatum = account.geboortedatum, beveiligingsvraag = account.beveiligingsvraag};
        var sql = @"INSERT INTO gebruiker 
                        (Gebruikersnaam, 
                         Email, 
                         Wachtwoord, 
                         Geboorte_datum, 
                         Beveiligingsvraag)
                         values
                         (
                         @gebruikersnaam,
                         @email,
                         @wachtwoord,
                         @geboortedatum,
                         @beveiligingsvraag
                         )";
        using var connection = Connect();
        {
            connection.Execute(sql, par);
        }
    }
}