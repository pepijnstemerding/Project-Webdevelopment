USE stripboek;
INSERT INTO `Gebruiker` (
    Gebruikersnaam,
    Wachtwoord,
    Email,
    Is_admin,
    Profiel_zichtbaarheid,
    Collectie_zichtbaarheid,
    Geboorte_datum,
    Beveiligingsvraag)
VALUES (
    'Bas',
    'password123',
    'bas.van.hensbergen@nhlstenden.com',
    true,
    1,
    1,
    '1967-01-02',
    'Pietje');

INSERT INTO `Collectie` (
    Collectie_naam,
    Gebruikers_id)
VALUES (
    'Nu lezen',
    1);

INSERT INTO `Stripboek` (
    Reeks,
    Titel,
    ISBN,
    Jaar_v_Uitgave,
    Uitgever,
    Afbeelding_urls,
    Waarde_schatting)
VALUES (
    'Suske en Wiske',
    'Walli de walvis',
    9789002139611,
    1978,
    'Standaard Uitgeverij',
    'https://media.s-bol.com/YyKVnvnoQprW/550x550.jpg',
    16.94);