CREATE DATABASE IF NOT EXISTS website;
USE website;
-- Standaard tabellen
CREATE TABLE IF NOT EXISTS `Gebruiker` (
`Gebruiker_id`                INT AUTO_INCREMENT NOT NULL,            -- Gebruiker_id
`Gebruikersnaam`              VARCHAR(32) NOT NULL UNIQUE,            -- Gebruikersnaam
`Email`                       VARCHAR(100) NOT NULL,                  -- Email van de gebruiker
`Wachtwoord`                  VARCHAR(30) NOT NULL,                   -- Wachtwoord van de gebruiker
`Is_admin`                    BOOL NOT NULL DEFAULT 0,                -- Of de gebruiker administrator permissies heeft
`Profiel_zichtbaarheid`       TINYINT UNSIGNED NOT NULL DEFAULT 1,    -- 0 als niemand je naam en geboortedatum kan zien, 1 als iedereen hem kan zien (uitbreid mogenlijkheid)
`Collectie_zichtbaarheid`     TINYINT UNSIGNED NOT NULL DEFAULT 1,    -- 0 als niemand je collecties kan zien, 1 als iedereen ze kan zien
`Geboorte_datum`              DATE NOT NULL,                          -- Geboorte datum van gebruiker
`Beveiligingsvraag`           VARCHAR(20) NOT NULL,                   -- Antwoord op de beveiligingsvraag voor ww reset 
PRIMARY KEY (`Gebruiker_id`));

CREATE TABLE IF NOT EXISTS `Collectie` (
`Collectie_id`     INT AUTO_INCREMENT NOT NULL,   -- Uniek collectie ID
`Collectie_naam`   VARCHAR(30) NOT NULL,          -- Naam van de collectie, standaard 'Mijn stripboeken'
PRIMARY KEY (`Collectie_id`));

CREATE TABLE IF NOT EXISTS `Stripboek` (
`Boek_id`          INT AUTO_INCREMENT NOT NULL,        -- Unieke stripboek ID
`Reeks`            TINYTEXT NOT NULL,                  -- Reeks van boek, bijvoorbeeld 'Luc Orient'
`Titel`            TINYTEXT NOT NULL,                  -- Titel van boek, bijvoorbeeld '24 uur voor de planeet aarde'
`ISBN`             VARCHAR(17),                        -- Internationaal Standaard Boeknummer van boek 
`Goedgekeurd`      BOOLEAN NOT NULL DEFAULT FALSE,     -- Goedgekeurd door een admin 
`Jaar_v_Uitgave`   YEAR(4),                            -- Jaar wanneer het stripboek werd uitgegeven
`Uitgever`         TINYTEXT,                           -- De uitgever van het boek
-- Combineren door middel van Json?
`Afbeelding_urls`  TEXT,                        -- Directe links naar afbeeldingen van het boek (met comma gesepareerd)
`Waarde_schatting` DECIMAL(10, 2),              -- Schatting van de waarde van het stripboek
PRIMARY KEY (`Boek_id`));

CREATE TABLE IF NOT EXISTS `Auteur` (
`Naam_Autheur`          VARCHAR(50) NOT NULL,  -- Naam van de auteur
PRIMARY KEY (`Naam_Autheur`));

CREATE TABLE IF NOT EXISTS `Tekenaar` (
`Naam_Tekenaar`          VARCHAR(50) NOT NULL,  -- Naam van de tekenaar
PRIMARY KEY (`Naam_Tekenaar`));

-- Koppel tabellen
CREATE TABLE IF NOT EXISTS `Bezit` (
`Gebruiker_id`   INT NOT NULL, -- Het ID van de gebruiker waarvan dit stripboek is
`Boek_id`        INT NOT NULL, -- Het ID van het boek waar het om gaat
`Locatie`        VARCHAR(10) NOT NULL, -- Locatie waar het boek zich bevindt    
`Status_exemplaar` TINYTEXT,     -- De status/qualiteit van het fysieke exemplaar
`Gekocht_voor`     DOUBLE,       -- De prijs waarvoor de gebruiker het boek heeft gekocht
PRIMARY KEY (`Boek_id`, `Gebruiker_id`),
FOREIGN KEY (`Boek_id`) REFERENCES `Stripboek`(`Boek_id`),
FOREIGN KEY (`Gebruiker_id`) REFERENCES `Gebruiker`(`Gebruiker_id`));

CREATE TABLE IF NOT EXISTS `Zit_in` (
`Boek_id`       INT NOT NULL,
`Collectie_id`  INT NOT NULL,
`Gebruiker_id`  INT NOT NULL, 
PRIMARY KEY (`Boek_id`, `Collectie_id`, `Gebruiker_id`),
FOREIGN KEY (`Boek_id`) REFERENCES `Stripboek`(`Boek_id`),
FOREIGN KEY (`Collectie_id`) REFERENCES `Collectie`(`Collectie_id`),
FOREIGN KEY (`Gebruiker_id`) REFERENCES `Gebruiker`(`Gebruiker_id`));

CREATE TABLE IF NOT EXISTS `Getekend_door` (
`Boek_id`       INT NOT NULL,       -- ID van het boek getekend door de tekenaar
`Naam_Tekenaar`          VARCHAR(50) NOT NULL,  -- Naam van de tekenaar
PRIMARY KEY (`Naam_Tekenaar`, `Boek_id`),
FOREIGN KEY (`Boek_id`) REFERENCES `Stripboek`(`Boek_id`),
FOREIGN KEY (`Naam_Tekenaar`) REFERENCES `Tekenaar`(`Naam_Tekenaar`));

CREATE TABLE IF NOT EXISTS `Geschreven_door` (
`Boek_id`       INT NOT NULL,       -- ID van het boek geschreven door de auteur
`Naam_Autheur`  VARCHAR(50) NOT NULL,  -- Naam van de auteur
PRIMARY KEY (`Naam_Autheur`, `Boek_id`),
FOREIGN KEY (`Boek_id`) REFERENCES `Stripboek`(`Boek_id`),
FOREIGN KEY (`Naam_Autheur`) REFERENCES  `Auteur`(`Naam_Autheur`));