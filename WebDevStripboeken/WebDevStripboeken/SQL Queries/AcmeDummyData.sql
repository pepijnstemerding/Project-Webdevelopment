USE website;
-- Standaard tabellen
-- Dummy data auteur tabel
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (1, 'Becky Chambers');
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (2, 'Daniel Chambers');
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (3, 'James Chambers');
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (4, 'Ian Flemming');
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (5, 'Malorie Blackman');
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (6, 'Taylor Jenkins Reid');
INSERT INTO auteur(Auteur_id, Naam_Auteur) VALUES (7, 'Sophie Morgan');

-- dummy data tekenaar tabel 
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (1, 'Nick Sharrat');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (2, 'Vincent Van Gogh');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (3, 'Henri Matisse');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (4, 'Roy Lichenstein');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (5, 'Damien Hirst');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (6, 'Banksy');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (7, 'Rembrandt');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (8, 'Leonardo Da Vinci');
INSERT INTO tekenaar(Tekenaar_id, Naam_Tekenaar) VALUES (9, ' Piet Mondiran');

-- Dummy data collectie tabel toevoegen 
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('1','Wishlist');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('2','TBR');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('3','Already have');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('4','Bookshelf 1');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('5','Wishlist2');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('6','Wishlist');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('7','Wishlist');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('8','Wishlist');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('9','Wishlist');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('10','Wishlist');
INSERT INTO collectie (Collectie_id, Collectie_naam) VALUES ('11','Honors shelf');

-- Dummy Data Gebruiker tabel
INSERT INTO Gebruiker(Gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('honor','something@gmail.com','wachtwoord1','1996-08-24','nee');
INSERT INTO Gebruiker(Gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('jason','something@hotmail.com','wachtwoord9','1995-09-24','nee');
INSERT INTO Gebruiker(Gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('marten','something@outlook.com','wachtwoord8','1994-12-24','veel');
INSERT INTO Gebruiker(gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('pepijn','something@yahoo.com','wachtwoord7','1993-11-24','ik');
INSERT INTO Gebruiker(gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('nigel','something1@gmail.com','wachtwoord6','1997-09-24','weet');
INSERT INTO Gebruiker(gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('tsjabbe','something2@gmail.com','wachtwoord5','1993-07-24','wij');
INSERT INTO Gebruiker(gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('lutsche','something3@gmail.com','wachtwoord4','1994-05-24','zij');
INSERT INTO Gebruiker(gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('bas','something4@gmail.com','wachtwoord3','1995-06-24','hij');
INSERT INTO Gebruiker(gebruikersnaam, email, wachtwoord, geboorte_datum, beveiligingsvraag) VALUES('joris','something5@gmail.com','wachtwoord2','1997-07-24','ja');

-- Dummy Data Stripboek tabel 
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (1, 'Donald Duck', 'Donald Duck: The duckiest book1', 112030405060, '1',  '1996', 'comicbookstore', 'www', 1010);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (2, 'Donald Duck', 'Donald Duck: The duckiest book2', 122030405060, '0', '1997', 'comicbookstore', 'www', 11);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (3, 'Donald Duck', 'Donald Duck: The duckiest book3', 132030405060, '1', '1998', 'comicbookstore', 'www', 90);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (4, 'Donald Duck', 'Donald Duck: The duckiest book4', 142030405060, '0', '1999', 'comicbookstore', 'www', 5);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (5, 'Donald Duck', 'Donald Duck: The duckiest book5', 152030405060, '1', '1992', 'comicbookstore', 'www', 2);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (6, 'Donald Duck', 'Donald Duck: The duckiest book6', 162030405060, '0', '1993', 'comicbookstore', 'www',900);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (7, 'Donald Duck', 'Donald Duck: The duckiest book7', 172030405060, '1', '1994', 'comicbookstore', 'www', 750);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (8, 'Donald Duck', 'Donald Duck: The duckiest book8', 182030405060, '0', '1995', 'comicbookstore', 'www', 600);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (9, 'Donald Duck', 'Donald Duck: The duckiest book9', 192030405060, '1', '1991', 'comicbookstore', 'www', 400);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (10, 'Donald Duck', 'Donald Duck: The duckiest book10', 101030405060, '0', '1990', 'comicbookstore', 'www', 300);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (11, 'Donald Duck', 'Donald Duck: The duckiest book11', 103030405060, '1', '1989', 'comicbookstore', 'www',20);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (12, 'Donald Duck', 'Donald Duck: The duckiest book12', 104030405060, '0', '1986', 'comicbookstore', 'www', 10);
INSERT INTO Stripboek(Boek_id, Reeks, Titel, ISBN, Goedgekeurd, Jaar_v_Uitgave, Uitgever, Afbeelding_urls, Waarde_schatting) VALUES (1000, 'Donald Duck', 'Donald Duck: The duckiest book13', 105030405060, '1', '1987', 'comicbookstore', 'www', 12);

-- Koppel tabellen
-- Dummy Data bezit toevoegen
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (1, 1, 'Shelf1', 'goed', '10');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (2, 2, '2a', 'nieuw', '5');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (3, 3, '3c', 'matig', '100');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (4, 4, '4d', 'goed', '9');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (5, 5, '5f', 'zogoed als nieuw', '15');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (6, 6, '6g', 'goed', '25');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (7, 7, '7h', 'matig', '45');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (8, 8, '8j', 'nieuw', '58');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (9, 9, '9k', 'goed', '5');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (1, 10, '10l', 'goed', '2.5');
INSERT INTO bezit(Gebruiker_id, Boek_id, Locatie, Status_exemplaar, Gekocht_voor) VALUES (2, 11, '11m', 'goed', '1.5');

-- dummy data voor geschreven_door
INSERT INTO geschreven_door (Boek_id, Auteur_id ) VALUES (1, 1);
INSERT INTO geschreven_door (Boek_id, Auteur_id ) VALUES (1, 2);
INSERT INTO geschreven_door (Boek_id, Auteur_id ) VALUES (1, 3);
INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES (2, 2);
INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES (3, 3);
INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES (4, 4);
INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES (5, 5);
INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES (6, 6);
INSERT INTO geschreven_door (Boek_id, Auteur_id) VALUES (7, 7);

-- dummy data voor de getekend door tabel 
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (1,1);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (1,2);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (1,3);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (2,2);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (3,3);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (4,4);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (5,5);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (6,6);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (7,7);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (8,9);
INSERT INTO getekend_door (Boek_id, Tekenaar_id) VALUES (9,8);

-- zit in dummy data 
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (1,1,1);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (2,2,2);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (3,3,3);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (4,4,4);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (5,5,5);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (6,6,6);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (7,7,7);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (8,8,8);
INSERT INTO zit_in (Boek_id, Collectie_id, Gebruiker_id) VALUES (9,9,9);