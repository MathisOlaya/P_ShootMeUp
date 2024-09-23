/* Ce fichier SQL explique en détail chaque requêtes SQL. En premier temps, j'afficherai la consigne de la requête donnée par le chef de projet. Suivra ensuite la requête, puis finalement une 
explication détaillée de celle-ci.*/

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

/*Requêtes 1 sur 10 : 
    La première requête que l'on vous demande de réaliser est de sélectionner les 5 joueurs
    qui ont le meilleur score c'est-à-dire qui ont le nombre de points le plus élevé. Les joueurs
    doivent être classés dans l'ordre décroissant*/

    SELECT idJoueur --Permet de sélectionner uniquement l'id du joueur*/
    FROM t_joueur --Permet de préciser sur quelle table nous souhaitons récupérer l'id du joueur*/
    ORDER BY jouNombrePoints --Permet de trier les joueurs en fonction de leurs nombre de points, pour autant le nombre de point ne sera pas affiché. A noter que par défaut, c'est trier de manière croissante.*/ 
    DESC LIMIT 5; --Et finalement, cela permet de limiter le nombre de résultat à 5 maximum.*/

/*Requêtes 2 sur 10 : 
    Trouver le prix maximum, minimum et moyen des armes.
    Les colonnes doivent avoir pour nom « PrixMaximum », « PrixMinimum » et « PrixMoyen)*/

    SELECT MIN(armPrix) as PrixMinimum, --Permet de sélectionner le prix minimum de l'attribut 'armPrix', puis je le renomme afin que la table soit plus lisible.*/
    AVG(armPrix) as PrixMoyen , --Permet de sélectionner la moyenne des prix de toute les armes, puis je le renomme en 'PrixMoyen' pour les mêmes raisons.*/
    MAX(armPrix) as PrixMaximum --Permet de sélectionner le prix maximum de l'attribut 'armPrix', puis à nouveau le renommer.*/
    FROM t_arme; --Permet de spécifier de quelle tables tous les attributs viennent.

/*Requêtes 3 sur 10 :
    Trouver le nombre total de commandes par joueur et trier du plus grand nombre au plus petit.
    La 1ère colonne aura pour nom "IdJoueur", la 2ème colonne aura pour nom "NombreCommandes"*/

    SELECT fkJoueur as idJoueur, --Permet de sélectionner la fk du joueur que je renomme en idJoueur pour une meilleure compréhension.
    COUNT(idCommande) as NombreCommandes --Permet de sélectionner le nombre de fois que l'idcommande apparaît lorsque la requête s'effectue, je la renomme ensuite en 'NombreCommandes'.
    FROM t_commande --Spécifier de quelle table viennent les attributs ci-dessus.
    GROUP BY idJoueur --Permet de regrouper les lignes ayant des valeurs identiques dans la colonne idJoueur.
    ORDER BY NombreCommandes DESC; --Puis ordoner le résultat, en fonction de NombreCommande dans l'ordre décroissant.

/*Requêtes 4 sur 10 : 
    Trouver les joueurs qui ont passé plus de 2 commandes.
    La 1ère colonne aura pour nom "IdJoueur", la 2ème colonne aura pour nom "NombreCommandes".*/

    SELECT fkJoueur as idJoueur, --Permet de sélectionner la fk du joueur puis la renommer en idJoueur pour une meilleure compréhension.
    COUNT(idCommande) as NombreCommandes --Permet de sélectionner le nombre de fois que l'idcommande apparaît lorsque la requête s'effectue, je la renomme ensuite en 'NombreCommandes'.
    FROM t_commande --Spécifier de quelle table viennent les attributs ci-dessus.
    GROUP BY idJoueur --Permet de regrouper les lignes ayant des valeurs identiques dans la colonne idJoueur.
    HAVING NombreCommandes > 2; --Permet d'afficher le résultat seulement si le joueur a effectué plus que deux commandes.

/*Requêtes 5 sur 10 : 
    Trouver le pseudo du joueur et le nom de l'arme pour chaque commande*/

    SELECT jouPseudo, arm.armNom --Permet de sélectionner le pseudo du joueur ainsi que le nom de l'arme.
    FROM t_joueur, t_commande co, t_detail_commande dc, t_arme arm --Puis sélectionner toutes ces tables afin de pouvoir avoir accès à certains attributs. Puis les renommer afin d'y avoir accès plus rapidement.
    WHERE idJoueur = co.fkJoueur --Afficher les résultats seulement quand l'idJoueur est égal à la fkJoueur de la table t_commande qui est renommée en 'co'.
    AND co.idCommande = dc.fkCommande --ET quand l'idCommande de la table commande est égale à la fkCommande de la table T_detail_commande.
    AND dc.fKArme = arm.idArme; --ET quand la fkArme de la table t_detail_commande est égale à l'idArme de la table t_arme.

/*Requêtes 6 sur 10 :
    Trouver le total dépensé par chaque joueur en ordonnant par le montant le plus élevé en
    premier, et limiter aux 10 premiers joueurs.
    La 1ère colonne doit avoir pour nom "IdJoueur" et la 2ème colonne "TotalDepense"*/

    SELECT idJoueur, --Permet de sélectionner l'id du joueur. 
    SUM(armPrix * dc.detQuantiteCommande) as PrixTotalDepense --Permet d'avoir la somme chaque armes achetées par le joueur, puis de multiplier par la quantité, et finalement la renommer en 'PrixTotalDepense'
    FROM t_joueur, t_commande co, t_detail_commande dc, t_arme arm --Puis sélectionner toutes ces tables afin de pouvoir avoir accès à certains attributs. Puis les renommer afin d'y avoir accès plus rapidement.
    WHERE idJoueur = co.fkJoueur --Afficher seulement les résultats si l'idJoueur est égale à la fkJoueur de la table t_commande.
    AND co.idCommande = dc.fkCommande --ET quand l'idCommande de la table t_commande est égale à la fkCommande de la table t_detail_commande.
    AND dc.fkArme = arm.idArme --ET quand la fkArme de la table t_detail_commande est égale à l'idArme de la table t_arme.
    GROUP BY idJoueur --Puis regrouper les lignes qui ont des valeurs identiques de la colonne idJoueur.
    ORDER BY PrixTotalDepense --Ordonner tout ce résultat dans l'ordre croissant (défault) en fonction de PrixTotalDepense.
    DESC LIMIT 10; --Et finalement, limiter le nombre de résultat à 10.

/*Requêtes 7 sur 10 : 
    Récupérez tous les joueurs et leurs commandes, même s'ils n'ont pas passé de commande.
    Dans cet exemple, même si un joueur n'a jamais passé de commande, il sera quand
    même listé, avec des valeurs `NULL` pour les champs de la table `t_commande`.*/

    SELECT jou.*, co.* --Permet de sélectionner tous les élements de la table joueur et de la table commande. '*' signifie tout. 'jou' et 'co' sont les diminutifs des tables.
    FROM t_joueur jou --Permet de spécifier de quelles tables proviennent ces élements, en l'occurence, uniquement t_joueur que l'on renomme en jou pour un accès simplifié.
    LEFT JOIN t_commande co --Permet de sélectionner tous les élements n'appartenant pas qu'exclusivement à t_commande que l'on renomme en co.
    ON jou.idJoueur = co.fkJoueur --Seulement lorsque l'id du joueur est égal à la clé étrangère de la commande.
    LEFT JOIN t_detail_commande dc --Comme précédemment, tous les éléments n'appartenant pas qu'exclusivement à t_detail_commande que l'on renomme en dc.s
    ON co.idCommande = dc.fkCommande; --Seulement quand l'id de la commande est égal à la clé étramgère de détail de la commande.

/*Requêtes 8 sur 10 : 
    Récupérer toutes les commandes et afficher le pseudo du joueur s?il existe, sinon afficher `NULL` pour le pseudo.*/

     SELECT co.*, jou.* --Permet de sélectionner tout les élements de la table commande et de la table joueur.
     FROM t_commande co --Spécifier la table d'ou provient les élements, en l'occurence, t_commande que l'on renomme en co.
     LEFT JOIN t_joueur jou --Sélectionner tous les élemets n'appartenant pas qu'exclusivement à t_joueur que l'on renomme en jou.
     ON co.fkJoueur = jou.idJoueur; --Seulement quand la clé étrangère du joueur est égale à l'id du joueur.

/*Requêtes 9 sur 10 : 
    Trouver le nombre total d'armes achetées par chaque joueur (même si ce joueur n'a acheté aucune Arme).*/

    SELECT jouPseudo, --Sélectionner le pseudo du joueur.
    IFNULL(SUM(dc.detQuantiteCommande), 0) AS ArmeTotalesAchetees --Sélectionner la somme de quantitée commandée de la table detailCommande seulement si la valeur n'est pas null, sinon lui attribuer 0. Puis la renommer en ArmeTotalesAchetees
    FROM t_joueur jou --Spécifier de quelle table proviennent les éléments.
    LEFT JOIN t_commande c --Sélectionner tous les élemets n'appartenant pas qu'exclusivement à t_commande que l'on renomme en 'c'.
    ON c.fkJoueur = jou.idJoueur --Seulement quand la clé étrangère du joueur est égale à l'id du joueur.
    LEFT JOIN t_detail_commande dc  --Sélectionner tous les élemets n'appartenant pas qu'exclusivement à t_detail_commande que l'on renomme en dc.
    ON dc.fkCommande = c.idCommande --Seulement quand la clé étrangère de detail commande est égale à l'id de la commande.
    GROUP BY jou.jouPseudo; --Puis regrouper les lignes qui ont des valeurs identiques de la colonne jouPseudo.

/*Requêtes 10 sur 10 : 
    Trouver les joueurs qui ont acheté plus de 3 types d'armes différentes*/
    SELECT jou.jouPseudo, --Sélectionner le pseudo du joueur.
    COUNT(DISTINCT(dc.fkArme)) as ArmeDifferenteAchetees --Compter le nombre d'id d'arme différentes et le renommer en armeDifferentesAchetees. 
    FROM t_joueur jou --Spécifier de quelle table.
    LEFT JOIN t_commande c --Sélectionner tous les élemets n'appartenant pas qu'exclusivement à t_commande que l'on renomme en c.
    ON c.fkJoueur = jou.idJoueur --Seulement quand la clé étrangère du joueur est égale à l'id du joueur.
    LEFT JOIN t_detail_commande dc --Pareil, mais pour la table t_detail_commande renommée en dc.
    ON dc.fkCommande = c.idCommande --Pareil, mais pour les attributs fkCommande et idCommande.
    LEFT JOIN t_arme arm --Pareil, mais pour la table t_arme renommée en arm.
    ON arm.idArme = dc.fkArme --Pareil, mais pour les attributs idArme et fkArme.
    GROUP BY jou.jouPseudo --Puis regrouper les lignes qui ont des valeurs identiques de la colonne jouPseudo.
    HAVING ArmeDifferenteAchetees > 3; --Seulement quand le nombre de ligne est suppérieur à 3.