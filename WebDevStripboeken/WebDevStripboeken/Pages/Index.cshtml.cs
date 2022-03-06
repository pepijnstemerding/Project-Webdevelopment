using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Pages.Shared;
using Newtonsoft.Json;

namespace WebDevStripboeken.Pages;

public class myUser : IamUser
{
    public string userName { get; set; } = "Guest";
    public string passWord { get; set; }
    public string eMail { get; set; }
}
public class IndexModel : PageModel

{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]  //global get
    public myUser currentUser { get; set; }
    public void OnGet()
    {
        #region MySQL stuff
        
        using (var connection = new MySqlConnection(
                   "Server=127.0.0.1;Database=stripboekdatabase;Uid=root;Pwd=j9muMb2002;Port=3306"))
        {
            connection.Open();
            Console.WriteLine($"MySQL version: {connection.ServerVersion}");

            using (var createUserTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `User` (
                `user_id`           INT AUTO_INCREMENT,        -- Unieke gebruikers ID
                `naam`              VARCHAR(70) NOT NULL,      -- Gebruikersnaam
                `wachtwoord`        VARCHAR(30) NOT NULL,      -- Wachtwoord van de gebruiker
                `is_administrator`  BOOL NOT NULL,             -- Of de gebruiker administrator permissies heeft
                `profile_visiblity` TINYINT UNSIGNED NOT NULL, -- 0 als niemand je naam en geboortedatum kan zien, 1 als iedereen hem kan zien (uitbreid mogenlijkheid)
                `shelve_visibility` TINYINT UNSIGNED NOT NULL, -- 0 als niemand je shelves kan zien, 1 als iedereen hem kan zien
                `geboorte_datum`    DATE,                      -- Geboorte datum van gebruiker
                PRIMARY KEY (`user_id`)
            );", connection))
            {
                createUserTableQuery.ExecuteNonQuery();
            }
            
            using (var createShelfTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Shelf` (
                `shelf_id` INT AUTO_INCREMENT,   -- Unieke shelf ID
                `naam`     VARCHAR(15) NOT NULL, -- Naam van de shelf, standaard 'Mijn stripboeken'
                PRIMARY KEY (`shelf_id`)
            );", connection))
            {
                createShelfTableQuery.ExecuteNonQuery();
            }
            
            using (var createComicTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Comic` (
                `boek_id`          INT AUTO_INCREMENT, -- Unieke stripboek ID
                `reeks`            TINYTEXT NOT NULL,  -- Reeks van boek, bijvoorbeeld 'Luc Orient'
                `titel`            TINYTEXT NOT NULL,  -- Titel van boek, bijvoorbeeld '24 uur voor de planeet aarde'
                `isbn`             VARCHAR(17),        -- Internationaal Standaard Boeknummer van boek 
                `jaar_v_uitgave`   YEAR(4),            -- Jaar wanneer het stripboek werd uitgegeven
                `auteur`           TINYTEXT,           -- De auteur van het boek
                `tekenaar`         TINYTEXT,           -- De tekenaar van de illustraties in het boek
                `uitgever`         TINYTEXT,           -- De uitgever van het boek
                `locatie`          TINYTEXT,           -- De locatie van het boek, bijvoorbeeld kast 3A
                `afbeelding_urls`  TEXT,               -- Directe links naar afbeeldingen van het boek (met comma gesepareerd)
                `waarde_schatting` DECIMAL(10, 2),     -- Schatting van de waarde van het stripboek
                PRIMARY KEY (`boek_id`)
            );", connection))
            {
                createComicTableQuery.ExecuteNonQuery();
            }
            
            using (var createHasTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `has` (
                `user_id`  INT NOT NULL, -- Het ID van de gebruiker waarvan dit stripboek is
                `boek_id`  INT NOT NULL, -- Het ID van het boek waar het om gaat
                `shelf_id` INT NOT NULL, -- De shelf waarin het boek zich bevindt
                PRIMARY KEY (`boek_id`, `user_id`),
                FOREIGN KEY (`boek_id`) REFERENCES `Comic`(`boek_id`),
                FOREIGN KEY (`user_id`) REFERENCES `User`(`user_id`),
                FOREIGN KEY (`shelf_id`) REFERENCES `Shelf`(`shelf_id`)
            );", connection))
            {
                createHasTableQuery.ExecuteNonQuery();
            }
            
            #region Fill db with default data

            bool fillWithDummyData = true;
            if (fillWithDummyData)
            {
                int userID;
                int shelfID;
                int comicID;
                
                // Voeg nieuwe gebruiker toe onder naam Bas
                using (var insertUserQuery = new MySqlCommand(
                           @"
                INSERT INTO `User` (
                    naam,
                    wachtwoord,
                    is_administrator,
                    profile_visiblity,
                    shelve_visibility,
                    geboorte_datum
                ) VALUES (
                    'Bas',
                    'password123',
                    true,
                    1,
                    1,
                    1967
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    userID = insertUserQuery.ExecuteNonQuery();
                }
                
                // Voeg nieuwe shelf toe onder naam 'Nu lezen'
                using (var insertShelfQuery = new MySqlCommand(
                           @"
                INSERT INTO `Shelf` (
                    naam
                ) VALUES (
                    'Nu lezen'
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    shelfID = insertShelfQuery.ExecuteNonQuery();
                }
                
                // Voeg nieuwe Comic toe met naam 'Walli de walvis' en behorende data
                using (var insertComicQuery = new MySqlCommand(
                           @"
                INSERT INTO `Shelf` (
                    reeks,
                    titel,
                    isbn,
                    jaar_v_uitgave,
                    auteur,
                    tekenaar,
                    uitgever,
                    locatie,
                    afbeelding_urls,
                    waarde_schatting
                ) VALUES (
                    'Suske en Wiske',
                    'Walli de walvis',
                    9789002139611,
                    1978,
                    'Willy Vandersteen',
                    'Willy Vandersteen',
                    'Standaard Uitgeverij',
                    'Kast 3A',
                    'https://media.s-bol.com/YyKVnvnoQprW/550x550.jpg',
                    16.94
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    comicID = insertComicQuery.ExecuteNonQuery();
                }
                
                // Leg de connectie tussen de nieuwe gebruiker, shelf en comic met has table
                using (var insertHasQuery = new MySqlCommand(
                           @"
                INSERT INTO `Shelf` (
                    user_id,
                    boek_id,
                    shelf_id
                ) VALUES (
                    @user_id,
                    @boek_id,
                    @shelf_id
                );", connection))
                {
                    insertHasQuery.Parameters.AddWithValue("@user_id", userID);
                    insertHasQuery.Parameters.AddWithValue("@boek_id", comicID);
                    insertHasQuery.Parameters.AddWithValue("@shelf_id", shelfID);
                    insertHasQuery.ExecuteNonQuery();
                }
            }
            
            #endregion
        }
        
        #endregion
    }
}