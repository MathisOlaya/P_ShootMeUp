-----------------------------[ Gestion des utilisateurs ]------------------------------
/*Pour la gestion des utilisateurs, j'ai cr�� des r�les. Cela me permet de d�finir une seule fois 
les privil�ges n�cessaires. Ensuite, je peux cr�er les utilisateurs et leur attribuer ces r�les.
Dans ce cas pr�cis, l'utilisation d'un r�le peut sembler superflue, mais dans une base de donn�es plus 
grande avec davantage d'utilisateurs, cela r�duit consid�rablement la charge de travail.
De cette mani�re, tous les privil�ges n�cessaires sont regroup�s dans un seul mot : le r�le.*/

use db_space_invaders;
--Administrateurs
    --Role
    CREATE role "Administrateurs"@"localhost";
    --Droits pour le role Administrateur
    GRANT CREATE, SELECT, UPDATE, DELETE ON * to "Administrateurs"@"localhost" WITH GRANT OPTION;
    --Cr�er l'utilisateur administrateur.
    CREATE USER "Administrateur"@"localhost" IDENTIFIED BY "AdminPass";
    --Attribuer les droits 
    GRANT "Administrateurs"@"localhost" TO "Administrateur"@"localhost";

--Joueur
    --Role
    CREATE role "Joueurs"@"localhost";
    --Droits pour le role Joueur
    GRANT SELECT ON t_arme TO "Joueurs"@"localhost";
    GRANT CREATE, SELECT ON t_commande TO "Joueurs"@"localhost";
    --Cr�er l'utilisateur joueur
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
    --Cr�er l'utlisateur Gestionnaire de boutique 
    CREATE USER "GestionnaireDeBoutique"@"localhost" IDENTIFIED BY "GestionnairePass";
    --Attribuer les droits
    GRANT "GestionnairesDeBoutique"@"localhost" TO "GestionnaireDeBoutique"@"localhost";

--V�rifier les privil�ges de chaque r�le
    SHOW GRANTS FOR "Administrateurs"@"localhost";
    SHOW GRANTS FOR "Joueurs"@"localhost";
    SHOW GRANTS FOR "GestionnairesDeBoutique"@"localhost";