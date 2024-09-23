-----------------------------[ Gestion des utilisateurs ]------------------------------
/*Pour la gestion des utilisateurs, j'ai créé des rôles. Cela me permet de définir une seule fois 
les privilèges nécessaires. Ensuite, je peux créer les utilisateurs et leur attribuer ces rôles.
Dans ce cas précis, l'utilisation d'un rôle peut sembler superflue, mais dans une base de données plus 
grande avec davantage d'utilisateurs, cela réduit considérablement la charge de travail.
De cette manière, tous les privilèges nécessaires sont regroupés dans un seul mot : le rôle.*/

use db_space_invaders;
--Administrateurs
    --Role
    CREATE role "Administrateurs"@"localhost";
    --Droits pour le role Administrateur
    GRANT CREATE, SELECT, UPDATE, DELETE ON * to "Administrateurs"@"localhost" WITH GRANT OPTION;
    --Créer l'utilisateur administrateur.
    CREATE USER "Administrateur"@"localhost" IDENTIFIED BY "AdminPass";
    --Attribuer les droits 
    GRANT "Administrateurs"@"localhost" TO "Administrateur"@"localhost";

--Joueur
    --Role
    CREATE role "Joueurs"@"localhost";
    --Droits pour le role Joueur
    GRANT SELECT ON t_arme TO "Joueurs"@"localhost";
    GRANT CREATE, SELECT ON t_commande TO "Joueurs"@"localhost";
    --Créer l'utilisateur joueur
    CREATE USER "Joueur"@"localhost" IDENTIFIED BY "JoueurPass";
    --Attribuer les droits
    GRANT "Joueurs"@"localhost" TO "Joueur"@"localhost";

--Gestionnaire de boutique
    --Role
    CREATE role "GestionnairesDeBoutique"@"localhost";
    --Droits pour le role Gestionnaire de boutique
    GRANT SELECT ON t_joueur TO 'GestionnairesDeBoutique'@'localhost';
    GRANT UPDATE, SELECT, DELETE on t_arme TO "GestionnairesDeBoutique"@"localhost";
    GRANT SELECT on t_commande TO "GestionnairesDeBoutique"@"localhost";
    --Créer l'utlisateur Gestionnaire de boutique 
    CREATE USER "GestionnaireDeBoutique"@"localhost" IDENTIFIED BY "GestionnairePass";
    --Attribuer les droits
    GRANT "GestionnairesDeBoutique"@"localhost" TO "GestionnaireDeBoutique"@"localhost";

--Vérifier les privilèges de chaque rôle
    SHOW GRANTS FOR "Administrateurs"@"localhost";
    SHOW GRANTS FOR "Joueurs"@"localhost";
    SHOW GRANTS FOR "GestionnairesDeBoutique"@"localhost";