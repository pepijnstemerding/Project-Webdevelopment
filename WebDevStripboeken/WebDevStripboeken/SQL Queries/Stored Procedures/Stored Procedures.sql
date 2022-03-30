#ik denk dat tekenaar/autuer makkelijker kan, mbv slimmer zijn
DROP PROCEDURE IF EXISTS StripboekAanpassen;

DELIMITER //
CREATE PROCEDURE StripboekAanpassen(
    #stripboek
    reeksVAR tinytext,
    titelVAR tinytext,
    isbnVAR varchar(17),
    goedgekeurdVAR  tinyint(1),
    jaar_v_UitgaveVAR year,
    uitgeverVAR tinytext,
    boek_idVAR int,

    #tekenaar
    naam_tekenaarARRAY varchar(255),

    #auteur
    naam_auteurARRAY varchar(255)
)
BEGIN
    #stripboek
    UPDATE stripboek
    SET Reeks          = reeksVAR,
        Titel          = titelVAR,
        isbn           = isbnVAR,
        Goedgekeurd    = goedgekeurdVAR,
        Jaar_v_Uitgave = jaar_v_UitgaveVAR,
        Uitgever       = uitgeverVAR
    WHERE Boek_id = boek_idVAR;


#tekenaar
    BEGIN

        #declare
        DECLARE tekenaar_index int;
        DECLARE counter INT DEFAULT 1;

        #while loop (kan misschien nog in een declare?)
        WHILE counter <= (SELECT count(d.tekenaar_id)
                          from tekenaar
                                   inner join getekend_door d on tekenaar.tekenaar_id = d.tekenaar_id)
            DO
            #kan misschien beter (alleen kan hij niet CAST(unisgned) als je het in de where zet)
            #haalt individueel id op
                SELECT nth_value(coalesce(g.tekenaar_id + ',', ''), counter) over ()
                FROM tekenaar
                         inner join getekend_door g on tekenaar.tekenaar_id = g.tekenaar_id
                LIMIT 1
                INTO @tekenaar;
                SET tekenaar_index = @tekenaar;


                #update
                UPDATE tekenaar
                    JOIN getekend_door gd on tekenaar.tekenaar_id = gd.tekenaar_id

                    #set naam
                SET Naam_Tekenaar =
                        #haalt array uit elkaar    
                        substring_index(
                                substring_index(tekenaar_naamARRAY, ',', counter)
                            , ',', -1)

                    #zorgt voor correct tekenaar per boek
                WHERE gd.Boek_id = boek_idVAR
                  #tekenaar per id
                  and tekenaar.tekenaar_id = tekenaar_index;

                #counter++
                SET counter = counter + 1;
            END WHILE;

    end;

#auteur
    BEGIN

        #declare
        DECLARE auteur_index int;
        DECLARE counter INT DEFAULT 1;

        #while loop (kan misschien nog in een declare?)
        WHILE counter <= (SELECT count(d.auteur_id)
                          from auteur
                                   inner join geschreven_door d on auteur.auteur_id = d.auteur_id)
            DO
            #kan misschien beter (alleen kan hij niet CAST(unisgned) als je het in de where zet)
            #haalt individueel id op
                SELECT nth_value(coalesce(g.auteur_id + ',', ''), counter) over ()
                FROM auteur
                         inner join geschreven_door g on auteur.auteur_id = g.auteur_id
                LIMIT 1
                INTO @auteur;
                SET auteur_index = @auteur;


                #update
                UPDATE auteur
                    JOIN geschreven_door gd on auteur.auteur_id = gd.auteur_id

                    #set naam
                SET Naam_Auteur =
                        #haalt array uit elkaar    
                        substring_index(
                                substring_index(auteur_naamARRAY, ',', counter)
                            , ',', -1)

                    #zorgt voor correct tekenaar per boek
                WHERE gd.Boek_id = boek_idVAR
                  #tekenaar per id
                  and auteur.auteur_id = auteur_index;

                #counter++
                SET counter = counter + 1;
            END WHILE;

    end;

END
//;
DELIMITER ;