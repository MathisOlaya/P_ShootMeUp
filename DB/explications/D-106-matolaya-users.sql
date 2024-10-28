-----------------------------[ Gestion des utilisateurs ]------------------------------
/*Pour la gestion des utilisateurs, j'ai cr�� des r�les. Cela me permet de d�finir une seule fois 
les privil�ges n�cessaires. Ensuite, je peux cr�er les utilisateurs et leur attribuer ces r�les.
Dans ce cas pr�cis, l'utilisation d'un r�le peut sembler superflue, mais dans une base de donn�es plus 
grande avec davantage d'utilisateurs, cela r�duit consid�rablement la charge de travail.
De cette mani�re, tous les privil�ges n�cessaires sont regroup�s dans un seul mot : le r�le.*/

/*Pour la cr�ation de r�les et d'utilisateurs, on ne pr�cise pas @localhost car nous travaillons dans 
un environnement docker ce qui nous forcerait � utiliser l'adresse IP du docker. Nous utilisons donc 
aucun param�tre, ce qui le place dans le nom d'h�te % qui s�lectionne tous les h�tes par d�faut.*/

USE db_space_invaders;
-- Administrateurs
    -- Role
    CREATE role IF NOT EXISTS "Administrateurs";
    -- Droits pour le role Administrateur
    GRANT CREATE, SELECT, UPDATE, DELETE ON * to "Administrateurs" WITH GRANT OPTION;
    -- Cr�er l'utilisateur administrateur.
    CREATE USER IF NOT EXISTS "Administrateur" IDENTIFIED BY "AdminPass";
    -- Attribuer les droits 
    GRANT "Administrateurs" TO "Administrateur" ;
    -- Activer le r�le
    SET DEFAULT ROLE "Administrateurs" TO "Administrateur" ;


-- Joueur
    -- Role
    CREATE role IF NOT EXISTS "Joueurs";
    -- Droits pour le role Joueur
    GRANT SELECT ON t_arme TO "Joueurs";
    GRANT CREATE, SELECT ON t_commande TO "Joueurs";
    -- Cr�er l'utilisateur joueur 
    CREATE USER IF NOT EXISTS "Player01" IDENTIFIED BY "JoueurPass";
    -- Attribuer les droits
    GRANT "Joueurs" TO "Player01" ;
    -- Activer le r�le
    SET DEFAULT ROLE "Joueurs" TO "Player01" ;


-- Gestionnaire de boutique
    -- Role
    CREATE role IF NOT EXISTS "GestionnairesDeBoutique";
    -- Droits pour le role Gestionnaire de boutique
    GRANT SELECT ON t_joueur TO 'GestionnairesDeBoutique';
    GRANT UPDATE, SELECT, DELETE on t_arme TO "GestionnairesDeBoutique";
    GRANT SELECT on t_commande TO "GestionnairesDeBoutique";
    -- Cr�er l'utlisateur Gestionnaire de boutique 
    CREATE USER IF NOT EXISTS "GestionnaireDeBoutique" IDENTIFIED BY "GestionnairePass";
    -- Attribuer les droits
    GRANT "GestionnairesDeBoutique" TO "GestionnaireDeBoutique";
    -- Activer le r�le
    SET DEFAULT ROLE "GestionnairesDeBoutique" TO "GestionnaireDeBoutique";


-- V�rifier les privil�ges de chaque r�le
    SHOW GRANTS FOR "Administrateurs" ;
    SHOW GRANTS FOR "Joueurs" ;
    SHOW GRANTS FOR "GestionnairesDeBoutique" ;