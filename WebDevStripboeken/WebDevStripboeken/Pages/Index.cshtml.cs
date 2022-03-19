using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using WebDevStripboeken.Models;
using WebDevStripboeken.Repository;
using Newtonsoft.Json;

namespace WebDevStripboeken.Pages;

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
        
        //using MySqlConnection connection = DbConnection.connect();
        {
            //connection.Open();
            //Console.WriteLine($"MySQL version: {connection.ServerVersion}");

            /*using (MySqlCommand createGebruikerTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Gebruiker` (
                `Gebruikers_id`               INT AUTO_INCREMENT NOT NULL,  -- Unieke gebruikers ID
                `Gebruikersnaam`              VARCHAR(32) NOT NULL,         -- Gebruikersnaam
                `Email`                       VARCHAR(100) NOT NULL,        -- Email van de gebruiker
                `Wachtwoord`                  VARCHAR(30) NOT NULL,         -- Wachtwoord van de gebruiker
                `Is_admin`                    BOOL NOT NULL,                -- Of de gebruiker administrator permissies heeft
                `Profiel_zichtbaarheid`       TINYINT UNSIGNED NOT NULL,    -- 0 als niemand je naam en geboortedatum kan zien, 1 als iedereen hem kan zien (uitbreid mogenlijkheid)
                `Collectie_zichtbaarheid`     TINYINT UNSIGNED NOT NULL,    -- 0 als niemand je collecties kan zien, 1 als iedereen ze kan zien
                `Geboorte_datum`              DATE NOT NULL,                -- Geboorte datum van gebruiker
                `Beveiligingsvraag`           VARCHAR(20) NOT NULL,         -- Antwoord op de beveiligingsvraag voor ww reset 
                PRIMARY KEY (`Gebruikers_id`)
            );", connection))
            {
                createGebruikerTableQuery.ExecuteNonQuery();
            }
            
            using (var createCollectieTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Collectie` (
                `Collectie_id`     INT AUTO_INCREMENT NOT NULL,   -- Uniek collectie ID
                `Collectie_naam`   VARCHAR(30) NOT NULL,          -- Naam van de collectie, standaard 'Mijn stripboeken'
                `Gebruikers_id`    int(255) NOT NULL,             -- ID van de gebruiker
                PRIMARY KEY (`Collectie_id`)
            );", connection))
            {
                createCollectieTableQuery.ExecuteNonQuery();
            }
            
            using (var createStripboekTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Stripboek` (
                `Boek_id`          INT AUTO_INCREMENT NOT NULL, -- Unieke stripboek ID
                `Reeks`            TINYTEXT NOT NULL,           -- Reeks van boek, bijvoorbeeld 'Luc Orient'
                `Titel`            TINYTEXT NOT NULL,           -- Titel van boek, bijvoorbeeld '24 uur voor de planeet aarde'
                `ISBN`             VARCHAR(17),                 -- Internationaal Standaard Boeknummer van boek 
                `Jaar_v_Uitgave`   YEAR(4),                     -- Jaar wanneer het stripboek werd uitgegeven
                `Auteur`           TINYTEXT,                    -- De auteur van het boek
                `Tekenaar`         TINYTEXT,                    -- De tekenaar van de illustraties in het boek
                `Uitgever`         TINYTEXT,                    -- De uitgever van het boek
                `Locatie`          TINYTEXT,                    -- De locatie van het boek, bijvoorbeeld kast 3A
                `Afbeelding_urls`  TEXT,                        -- Directe links naar afbeeldingen van het boek (met comma gesepareerd)
                `Waarde_schatting` DECIMAL(10, 2),              -- Schatting van de waarde van het stripboek
                PRIMARY KEY (`Boek_id`)
            );", connection))
            {
                createStripboekTableQuery.ExecuteNonQuery();
            }
            
            using (var createBezitTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Bezit` (
                `Gebruikers_id`    INT NOT NULL, -- Het ID van de gebruiker waarvan dit stripboek is
                `Boek_id`          INT NOT NULL, -- Het ID van het boek waar het om gaat
                `Collectie_id`     INT NOT NULL, -- locatie waar het boek zich bevindt
                `Status_exemplaar` TINYTEXT,     -- De status/qualiteit van het fysieke exemplaar
                `Gekocht_voor`     DOUBLE,       -- De prijs waarvoor de gebruiker het boek heeft gekocht
                PRIMARY KEY (`Boek_id`, `Gebruikers_id`),
                FOREIGN KEY (`Boek_id`) REFERENCES `Stripboek`(`Boek_id`),
                FOREIGN KEY (`Gebruikers_id`) REFERENCES `Gebruiker`(`Gebruikers_id`),
                FOREIGN KEY (`Collectie_id`) REFERENCES `Collectie`(`Collectie_id`)
            );", connection))
            {
                createBezitTableQuery.ExecuteNonQuery();
            }
            
            using (var createZitInTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Zit_in` (
                `Boek_id`       INT NOT NULL,
                `Collectie_id`  INT NOT NULL,
                PRIMARY KEY (`Boek_id`, `Collectie_id`)
            );", connection))
            {
                createZitInTableQuery.ExecuteNonQuery();
            }
            
            using (var createAuteurTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Auteur` (
                `Naam_Autheur`          VARCHAR(50) NOT NULL,  -- Naam van de auteur
                PRIMARY KEY (`Naam_Autheur`)
            );", connection))
            {
                createAuteurTableQuery.ExecuteNonQuery();
            }
            
            using (var createTekenaarTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Tekenaar` (
                `Naam_Tekenaar`          VARCHAR(50) NOT NULL,  -- Naam van de tekenaar
                PRIMARY KEY (`Naam_Tekenaar`)
            );", connection))
            {
                createTekenaarTableQuery.ExecuteNonQuery();
            }
            
            using (var createGetekendDoorTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Getekend_door` (
                `Boek_id`       INT NOT NULL,       -- ID van het boek getekend door de tekenaar
                `Naam_Tekenaar`          VARCHAR(50) NOT NULL,  -- Naam van de tekenaar
                PRIMARY KEY (`Naam_Tekenaar`, `Boek_id`)
            );", connection))
            {
                createGetekendDoorTableQuery.ExecuteNonQuery();
            }
            
            using (var createGeschrevenDoorTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Geschreven_door` (
                `Boek_id`       INT NOT NULL,       -- ID van het boek geschreven door de auteur
                `Naam_Autheur`          VARCHAR(50) NOT NULL,  -- Naam van de auteur
                PRIMARY KEY (`Naam_Autheur`, `Boek_id`)
            );", connection))
            {
                createGeschrevenDoorTableQuery.ExecuteNonQuery();
            }
            
            #region Fill db with default data

            bool fillWithDummyData = false;
            if (fillWithDummyData)
            {
                int GebruikersID;
                int CollectieID;
                int BoekID;
                
                // Voeg nieuwe gebruiker toe onder naam Bas
                using (var insertGebruikerQuery = new MySqlCommand(
                           @"
                INSERT INTO `Gebruiker` (
                    Gebruikersnaam,
                    Wachtwoord,
                    Email,
                    Is_admin,
                    Profiel_zichtbaarheid,
                    Collectie_zichtbaarheid,
                    Geboorte_datum,
                    Beveiligingsvraag
                ) VALUES (
                    'Bas',
                    'password123',
                    'bas.van.hensbergen@nhlstenden.com'
                    true,
                    1,
                    1,
                    1967
                    'Pietje'
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    GebruikersID = insertGebruikerQuery.ExecuteNonQuery();
                }
                
                // Voeg nieuwe shelf toe onder naam 'Nu lezen'
                using (var insertCollectieQuery = new MySqlCommand(
                           @"
                INSERT INTO `Collectie` (
                    Collectie_naam
                ) VALUES (
                    'Nu lezen'
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    CollectieID = insertCollectieQuery.ExecuteNonQuery();
                }
                
                // Voeg nieuw stripboek toe met naam 'Walli de walvis' en behorende data
                using (var insertStripboekQuery = new MySqlCommand(
                           @"
                INSERT INTO `Stripboek` (
                    Reeks,
                    Titel,
                    ISBN,
                    Jaar_v_Uitgave,
                    Auteur,
                    Tekenaar,
                    Uitgever,
                    Locatie,
                    Afbeelding_urls,
                    Waarde_schatting
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
                    BoekID = insertStripboekQuery.ExecuteNonQuery();
                }
                
                // Leg de connectie tussen de nieuwe gebruiker, shelf en comic met has table
                using (var insertBezitQuery = new MySqlCommand(
                           @"
                INSERT INTO `Shelf` (
                    Gebruikers_id,
                    Boek_id,
                    Collectie_id
                ) VALUES (
                    @Gebruikers_id,
                    @Boek_id,
                    @Collectie_id
                );", connection))
                {
                    insertBezitQuery.Parameters.AddWithValue("@Gebruikers_id", GebruikersID);
                    insertBezitQuery.Parameters.AddWithValue("@Boek_id", BoekID);
                    insertBezitQuery.Parameters.AddWithValue("@Collectie_id", CollectieID);
                    insertBezitQuery.ExecuteNonQuery();
                }
            }
            
            #endregion
            */
        }
        
        #endregion
        
    }
}