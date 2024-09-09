/* Ce fichier SQL explique en d�tail chaque requ�tes SQL. En premier temps, j'afficherai la consigne de la requ�te donn�e par le chef de projet. Suivra ensuite la requ�te, puis finalement une 
explication d�taill�e de celle-ci.*/

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

/*Requ�tes 1 sur 10 : 
    La premi�re requ�te que l'on vous demande de r�aliser est de s�lectionner les 5 joueurs
    qui ont le meilleur score c'est-�-dire qui ont le nombre de points le plus �lev�. Les joueurs
    doivent �tre class�s dans l'ordre d�croissant*/

    SELECT idJoueur --Permet de s�lectionner uniquement l'id du joueur*/
    FROM t_joueur --Permet de pr�ciser sur quelle table nous souhaitons r�cup�rer l'id du joueur*/
    ORDER BY jouNombrePoints --Permet de trier les joueurs en fonction de leurs nombre de points, pour autant le nombre de point ne sera pas affich�. A noter que par d�faut, c'est trier de mani�re croissante.*/ 
    DESC LIMIT 5; --Et finalement, cela permet de limiter le nombre de r�sultat � 5 maximum.*/

/*Requ�tes 2 sur 10 : 
    Trouver le prix maximum, minimum et moyen des armes.
    Les colonnes doivent avoir pour nom � PrixMaximum �, � PrixMinimum � et � PrixMoyen)*/

    SELECT MIN(armPrix) as PrixMinimum, --Permet de s�lectionner le prix minimum de l'attribut 'armPrix', puis je le renomme afin que la table soit plus lisible.*/
    AVG(armPrix) as PrixMoyen , --Permet de s�lectionner la moyenne des prix de toute les armes, puis je le renomme en 'PrixMoyen' pour les m�mes raisons.*/
    MAX(armPrix) as PrixMaximum --Permet de s�lectionner le prix maximum de l'attribut 'armPrix', puis � nouveau le renommer.*/
    FROM t_arme; --Permet de sp�cifier de quelle tables tous les attributs viennent.

/*Requ�tes 3 sur 10 :
    Trouver le nombre total de commandes par joueur et trier du plus grand nombre au plus petit.
    La 1�re colonne aura pour nom "IdJoueur", la 2�me colonne aura pour nom "NombreCommandes"*/

    SELECT fkJoueur as idJoueur, --Permet de s�lectionner la fk du joueur que je renomme en idJoueur pour une meilleure compr�hension.
    COUNT(idCommande) as NombreCommandes --Permet de s�lectionner le nombre de fois que l'idcommande appara�t lorsque la requ�te s'effectue, je la renomme ensuite en 'NombreCommandes'.
    FROM t_commande --Sp�cifier de quelle table viennent les attributs ci-dessus.
    GROUP BY idJoueur --Permet de regrouper les lignes ayant des valeurs identiques dans la colonne idJoueur.
    ORDER BY NombreCommandes DESC; --Puis ordoner le r�sultat, en fonction de NombreCommande dans l'ordre d�croissant.

/*Requ�tes 4 sur 10 : 
    Trouver les joueurs qui ont pass� plus de 2 commandes.
    La 1�re colonne aura pour nom "IdJoueur", la 2�me colonne aura pour nom "NombreCommandes".*/

    SELECT fkJoueur as idJoueur, --Permet de s�lectionner la fk du joueur puis la renommer en idJoueur pour une meilleure compr�hension.
    COUNT(idCommande) as NombreCommandes --Permet de s�lectionner le nombre de fois que l'idcommande appara�t lorsque la requ�te s'effectue, je la renomme ensuite en 'NombreCommandes'.
    FROM t_commande --Sp�cifier de quelle table viennent les attributs ci-dessus.
    GROUP BY idJoueur --Permet de regrouper les lignes ayant des valeurs identiques dans la colonne idJoueur.
    HAVING NombreCommandes > 2; --Permet d'afficher le r�sultat seulement si le joueur a effectu� plus que deux commandes.

/*Requ�tes 5 sur 10 : 
    Trouver le pseudo du joueur et le nom de l'arme pour chaque commande*/

    SELECT jouPseudo, arm.armNom --Permet de s�lectionner le pseudo du joueur ainsi que le nom de l'arme.
    FROM t_joueur, t_commande co, t_detail_commande dc, t_arme arm --Puis s�lectionner toutes ces tables afin de pouvoir avoir acc�s � certains attributs. Puis les renommer afin d'y avoir acc�s plus rapidement.
    WHERE idJoueur = co.fkJoueur --Afficher les r�sultats seulement quand l'idJoueur est �gal � la fkJoueur de la table t_commande qui est renomm�e en 'co'.
    AND co.idCommande = dc.fkCommande --ET quand l'idCommande de la table commande est �gale � la fkCommande de la table T_detail_commande.
    AND dc.fKArme = arm.idArme; --ET quand la fkArme de la table t_detail_commande est �gale � l'idArme de la table t_arme.

/*Requ�tes 6 sur 10 :
    Trouver le total d�pens� par chaque joueur en ordonnant par le montant le plus �lev� en
    premier, et limiter aux 10 premiers joueurs.
    La 1�re colonne doit avoir pour nom "IdJoueur" et la 2�me colonne "TotalDepense"*/

    SELECT idJoueur, --Permet de s�lectionner l'id du joueur. 
    SUM(armPrix * dc.detQuantiteCommande) as PrixTotalDepense --Permet d'avoir la somme chaque armes achet�es par le joueur, puis de multiplier par la quantit�, et finalement la renommer en 'PrixTotalDepense'
    FROM t_joueur, t_commande co, t_detail_commande dc, t_arme arm --Puis s�lectionner toutes ces tables afin de pouvoir avoir acc�s � certains attributs. Puis les renommer afin d'y avoir acc�s plus rapidement.
    WHERE idJoueur = co.fkJoueur --Afficher seulement les r�sultats si l'idJoueur est �gale � la fkJoueur de la table t_commande.
    AND co.idCommande = dc.fkCommande --ET quand l'idCommande de la table t_commande est �gale � la fkCommande de la table t_detail_commande.
    AND dc.fkArme = arm.idArme --ET quand la fkArme de la table t_detail_commande est �gale � l'idArme de la table t_arme.
    GROUP BY idJoueur --Puis regrouper les lignes qui ont des valeurs identiques de la colonne idJoueur.
    ORDER BY PrixTotalDepense --Ordonner tout ce r�sultat dans l'ordre croissant (d�fault) en fonction de PrixTotalDepense.
    DESC LIMIT 10; --Et finalement, limiter le nombre de r�sultat � 10.