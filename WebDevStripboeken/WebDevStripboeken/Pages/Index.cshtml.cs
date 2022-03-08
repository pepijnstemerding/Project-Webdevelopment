using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebDevStripboeken.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        #region MySQL stuff
        
        using (var connection = new MySqlConnection(
                   "Server=127.0.0.1;Database=stripboekdatabase;Uid=root;Pwd=j9muMb2002;Port=3306"))
        {
            connection.Open();
            Console.WriteLine($"MySQL version: {connection.ServerVersion}");

            using (var createGebruikerTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Gebruiker` (
                `gebruiker_id`                INT AUTO_INCREMENT,        -- Unieke gebruikers ID (PK)
                `gebruiker_naam`              VARCHAR(70) NOT NULL,      -- Gebruikers naam (lengte is max 70)
                `email_adres`                 VARCHAR(255),              -- Gebruikers email adres (lengte is max 255)
                `wachtwoord`                  VARCHAR(30) NOT NULL,      -- Wachtwoord (lengte is max 30)
                `is_admin`                    BOOL NOT NULL,             -- Of de gebruiker administrator permissies heeft
                `profiel_zichtbaarheid`       TINYINT UNSIGNED NOT NULL, -- 0 als niemand je naam en geboortedatum kan zien, 1 als iedereen hem kan zien (uitbreid mogenlijkheid)
                `collectie_zichtbaarheid`     TINYINT UNSIGNED NOT NULL, -- 0 als niemand je shelves kan zien, 1 als iedereen hem kan zien (uitbreid mogenlijkheid)
                `geboorte_datum`              DATE,                      -- Geboorte datum van gebruiker
                `beveiligings_vraag_antwoord` VARCHAR(255),              -- Antwoord op beveilingsvraag voor gebruiker om wachtwoord te resetten (lengte is max 255)
                PRIMARY KEY (`gebruiker_id`)
            );", connection))
            {
                createGebruikerTableQuery.ExecuteNonQuery();
            }
            
            using (var createCollectieTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Collectie` (
                `collectie_id`   INT AUTO_INCREMENT,   -- Unieke collectie ID (PK)
                `collectie_naam` VARCHAR(40) NOT NULL, -- Naam van de shelf, standaard 'Mijn stripboeken' (lengte is max 40)
                `gebruiker_id`   INT NOT NULL,         -- Het ID van de gebruiker waarvan deze collectie is
                PRIMARY KEY (`collectie_id`)
            );", connection))
            {
                createCollectieTableQuery.ExecuteNonQuery();
            }
            
            using (var createStripboekTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Stripboek` (
                `boek_id`          INT AUTO_INCREMENT, -- Unieke stripboek ID (PK)
                `reeks`            TINYTEXT NOT NULL,  -- Reeks van boek, bijvoorbeeld 'Luc Orient'
                `titel`            TINYTEXT NOT NULL,  -- Titel van boek, bijvoorbeeld '24 uur voor de planeet aarde'
                `goedgekeurd`      BOOL NOT NULL,      -- Of het boek is goedgekeurd en door anderen gebruikt kan worden
                `isbn`             VARCHAR(17),        -- Internationaal Standaard Boeknummer van boek (lengte is max 17)
                `jaar_v_uitgave`   YEAR(4),            -- Jaar wanneer het stripboek werd uitgegeven
                `auteur`           TINYTEXT,           -- De auteur(s) van het boek
                `tekenaar`         TINYTEXT,           -- De tekenaar(s) van de illustraties in het boek
                `uitgever`         TINYTEXT,           -- De uitgever van het boek
                `afbeelding_urls`  TEXT,               -- Directe links naar afbeeldingen van het boek (met comma gesepareerd)
                `waarde_schatting` DECIMAL(10, 2),     -- Schatting van de waarde van het stripboek (2 decimaal, euro)
                PRIMARY KEY (`boek_id`),
                FOREIGN KEY (`auteur`) REFERENCES `Auteur`(`auteur_naam`),
                FOREIGN KEY (`tekenaar`) REFERENCES `Tekenaar`(`tekenaar_naam`)
            );", connection))
            {
                createStripboekTableQuery.ExecuteNonQuery();
            }
            
            using (var createBezitTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Bezit` (
                `gebruiker_id`     INT NOT NULL,   -- Het ID van de gebruiker waarvan dit stripboek is (PK)
                `boek_id`          INT NOT NULL,   -- Het ID van het boek waar het om gaat (PK)
                `locatie`          TINYTEXT,       -- De locatie van het boek, bijvoorbeeld kast 3A
                `status_exemplaar` VARCHAR(50),    -- De visuele status van het exemplaar, bijvoorbeeld 'goed' of 'slecht'
                `gekocht_voor`     DECIMAL(10, 2), -- Voor hoeveel het stripboek gekocht is (2 decimaal, euro)
                PRIMARY KEY (`gebruiker_id`, `boek_id`),
                FOREIGN KEY (`boek_id`) REFERENCES `Stripboek`(`boek_id`),
                FOREIGN KEY (`gebruiker_id`) REFERENCES `Gebruiker`(`gebruiker_id`)
            );", connection))
            {
                createBezitTableQuery.ExecuteNonQuery();
            }
            
            using (var createAuteurTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Auteur` (
                `auteur_naam` TINYTEXT NOT NULL, -- De naam van de auteur van het boek (PK)
                `boek_id`     INT NOT NULL,      -- Het stripboek die deze auteur heeft geschreven
                PRIMARY KEY (`auteur_naam`)
            );", connection))
            {
                createAuteurTableQuery.ExecuteNonQuery();
            }
            
            using (var createTekenaarTableQuery = new MySqlCommand(
                       @"
            CREATE TABLE IF NOT EXISTS `Tekenaar` (
                `tekenaar_naam` TINYTEXT NOT NULL, -- De naam van de tekenaar van het boek (PK)
                `boek_id`       INT NOT NULL,      -- Het stripboek waar deze tekenaar de illustraties voor heeft gemaakt
                PRIMARY KEY (`tekenaar_naam`)
            );", connection))
            {
                createTekenaarTableQuery.ExecuteNonQuery();
            }
            
            #region Fill db with dummy data

            bool fillWithDummyData = true;
            if (fillWithDummyData)
            {
                int gebruikerId;
                int collectieId;
                int stripboekId;
                
                // Voeg nieuwe Gebruiker toe
                using (var insertGebruikerQuery = new MySqlCommand(
                           @"
                INSERT INTO `Gebruiker` (
                    gebruiker_naam,
                    email_adres,
                    wachtwoord,
                    is_admin,
                    profiel_zichtbaarheid,
                    collectie_zichtbaarheid,
                    geboorte_datum,
                    beveilings_vraag_antwoord
                ) VALUES (
                    'Bas',
                    'Bas.Hensbergen@maildomain.com',
                    'password123',
                    true,
                    1,
                    1,
                    1967,
                    'Geen idee'
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    gebruikerId = insertGebruikerQuery.ExecuteNonQuery();
                }
                
                // Voeg nieuwe Collectie toe
                using (var insertCollectieQuery = new MySqlCommand(
                           @"
                INSERT INTO `Collectie` (
                    collectie_naam
                ) VALUES (
                    'Nu aan het lezen'
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    collectieId = insertCollectieQuery.ExecuteNonQuery();
                }
                
                // Voeg nieuwe Stripboek toe
                using (var insertStripboekQuery = new MySqlCommand(
                           @"
                INSERT INTO `Stripboek` (
                    reeks,
                    titel,
                    goedgekeurd,
                    isbn,
                    jaar_v_uitgave,
                    auteurs,
                    tekenaars,
                    uitgevers,
                    afbeelding_urls,
                    waarde_schatting
                ) VALUES (
                    'Suske en Wiske',
                    'Walli de walvis',
                    true,
                    9789002139611,
                    1978,
                    'Willy Vandersteen',
                    'Willy Vandersteen',
                    'Standaard Uitgeverij',
                    'https://media.s-bol.com/YyKVnvnoQprW/550x550.jpg',
                    16.94
                ) SELECT LAST_INSERT_ID();", connection))
                {
                    stripboekId = insertStripboekQuery.ExecuteNonQuery();
                }
                
                // Leg de connectie tussen de nieuwe Gebruiker, Collectie en Stripboek met Bezit table
                using (var insertBezitQuery = new MySqlCommand(
                           @"
                INSERT INTO `Bezit` (
                    gebruiker_id,
                    boek_id,
                    locatie,
                    status_exemplaar,
                    gekocht_voor
                ) VALUES (
                    @gebruiker_id,
                    @boek_id,
                    'Kast 3A',
                    'goed',
                    5.50
                );", connection))
                {
                    insertBezitQuery.Parameters.AddWithValue("@gebruiker_id", gebruikerId);
                    insertBezitQuery.Parameters.AddWithValue("@boek_id", stripboekId);
                    insertBezitQuery.ExecuteNonQuery();
                }
            }
            
            #endregion
        }
        
        #endregion
    }
}