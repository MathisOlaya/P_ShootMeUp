-----------------------------[ Gestion des utilisateurs ]------------------------------
/*Pour la gestion des utilisateurs, j'ai créé des rôles. Cela me permet de définir une seule fois 
les privilèges nécessaires. Ensuite, je peux créer les utilisateurs et leur attribuer ces rôles.
Dans ce cas précis, l'utilisation d'un rôle peut sembler superflue, mais dans une base de données plus 
grande avec davantage d'utilisateurs, cela réduit considérablement la charge de travail.
De cette manière, tous les privilèges nécessaires sont regroupés dans un seul mot : le rôle.*/

/*Pour la création de rôles et d'utilisateurs, on ne précise pas @localhost car nous travaillons dans 
un environnement docker ce qui nous forcerait à utiliser l'adresse IP du docker. Nous utilisons donc 
aucun paramètre, ce qui le place dans le nom d'hôte % qui sélectionne tous les hôtes par défaut.*/

use db_space_invaders;
--Administrateurs
    --Role
    CREATE role "Administrateurs";
    --Droits pour le role Administrateur
    GRANT CREATE, SELECT, UPDATE, DELETE ON * to "Administrateurs" WITH GRANT OPTION;
    --Créer l'utilisateur administrateur.
    CREATE USER "Administrateur" IDENTIFIED BY "AdminPass";
    --Attribuer les droits 
    GRANT "Administrateurs" TO "Administrateur" ;
    --Activer le rôle
    SET DEFAULT ROLE "Administrateurs" TO "Administrateur" ;


--Joueur
    --Role
    CREATE role "Joueurs";
    --Droits pour le role Joueur
    GRANT SELECT ON t_arme TO "Joueurs";
    GRANT CREATE, SELECT ON t_commande TO "Joueurs";
    --Créer l'utilisateur joueur
    CREATE USER "Joueur" IDENTIFIED BY "JoueurPass";
    --Attribuer les droits
    GRANT "Joueurs" TO "Joueur" ;
    --Activer le rôle
    SET DEFAULT ROLE "Joueurs" TO "Joueur" ;


--Gestionnaire de boutique
    --Role
    CREATE role "GestionnairesDeBoutique";
    --Droits pour le role Gestionnaire de boutique
    GRANT SELECT ON t_joueur TO 'GestionnairesDeBoutique';
    GRANT UPDATE, SELECT, DELETE on t_arme TO "GestionnairesDeBoutique";
    GRANT SELECT on t_commande TO "GestionnairesDeBoutique";
    --Créer l'utlisateur Gestionnaire de boutique 
    CREATE USER "GestionnaireDeBoutique" IDENTIFIED BY "GestionnairePass";
    --Attribuer les droits
    GRANT "GestionnairesDeBoutique" TO "GestionnaireDeBoutique";
    --Activer le rôle
    SET DEFAULT ROLE "GestionnairesDeBoutique" TO "GestionnaireDeBoutique";


--Vérifier les privilèges de chaque rôle
    SHOW GRANTS FOR "Administrateurs" ;
    SHOW GRANTS FOR "Joueurs" ;
    SHOW GRANTS FOR "GestionnairesDeBoutique" ;