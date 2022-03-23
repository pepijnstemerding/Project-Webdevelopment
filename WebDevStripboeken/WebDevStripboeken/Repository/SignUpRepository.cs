using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebDevStripboeken.Models;
namespace WebDevStripboeken.Repository;

public class SignUpRepository : DBConnection
{
    public async Task SignUp([FromBody] Account account)
    {
        using var connection = Connect();
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
    }
}