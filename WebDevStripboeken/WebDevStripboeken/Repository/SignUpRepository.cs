using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebDevStripboeken.Models;
namespace WebDevStripboeken.Repository;

public class SignUpRepository : DBConnection
{
    public static async Task SignUp(Account account)
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
    //maakt sql string aan maar wordt nergens uitgevoerd
    //geen gebruik van dapper
    //hz Task?
    //onduidelijk voor comp welke gebruikersnaam
}