using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebDevStripboeken.Models;
namespace WebDevStripboeken.Repository;

public class SignUpRepository : DBConnection
{
    public static void SignUp(Account account)
    {
        var par = new {gebruikersnaam = account.gebruikersnaam, email = account.email, wachtwoord = account.wachtwoord, geboortedatum = account.geboortedatum, beveiligingsvraag = account.beveiligingsvraag};
        var sql = @"INSERT INTO website.gebruiker 
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