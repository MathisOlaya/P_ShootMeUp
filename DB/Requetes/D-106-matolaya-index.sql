/*Ce fichier contient les r�ponses concernant la cr�ation des index. Voici la consigne donn� par d�faut : 

En �tudiant le dump MySQL db_space_invaders.sql vous constaterez que vous ne trouvez
pas le mot cl� INDEX. */

--1     Pourtant certains index existent d�j�. Pourquoi ?

      /*Tout simplement car les cl�s primaires ainsi que les cl�s �trang�res 
        contiennent par d�faut des INDEX lors de la cr�ation*/

--2     Quels sont les avantages et les inconv�nients des index ? 
      
      /*Avantages : 
                    1) Ils permettent d'acc�lerer les requ�tes sans devoir parcourir toute la table. Cela est tres efficace dans des grandes base de donn�es.
                    2) Les index sur les colonnes utilis�es dans les jointures (JOIN) peuvent acc�l�rer ces op�rations.  
      
        Inconv�nients : 
                    1) Chaque index occupe plus de stockage et augmente donc la taille de la base de donn�es.
                    2) Les diff�rentes op�rations tels qu'INSERT, UPDATE et DELETE sont ralentis sur une table index�e car � chaque modification, les index doivent �tre mis � jour.*/
                
--3     Sur quel champ (de quelle table), cela pourrait �tre pertinent d?ajouter un index ?

      /*Sur les tables contenant beaucoup de donn�es, ou qu'on appelle fr�quemment. Dans notre cas
      sur le champ jouPseudo de la table t_joueur. Car ce champ est appel� � chaque connexion du joueur et est g�n�ralement unique.