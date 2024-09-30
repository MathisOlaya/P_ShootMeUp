/*Ce fichier contient les réponses concernant la création des index. Voici la consigne donné par défaut : 

En étudiant le dump MySQL db_space_invaders.sql vous constaterez que vous ne trouvez
pas le mot clé INDEX. */

--1     Pourtant certains index existent déjà. Pourquoi ?

      /*Tout simplement car les clés primaires ainsi que les clés étrangères 
        contiennent par défaut des INDEX lors de la création*/

--2     Quels sont les avantages et les inconvénients des index ? 
      
      /*Avantages : 
                    1) Ils permettent d'accélerer les requêtes sans devoir parcourir toute la table. Cela est tres efficace dans des grandes base de données.
                    2) Les index sur les colonnes utilisées dans les jointures (JOIN) peuvent accélérer ces opérations.  
      
        Inconvénients : 
                    1) Chaque index occupe plus de stockage et augmente donc la taille de la base de données.
                    2) Les différentes opérations tels qu'INSERT, UPDATE et DELETE sont ralentis sur une table indexée car à chaque modification, les index doivent être mis à jour.*/
                
--3     Sur quel champ (de quelle table), cela pourrait être pertinent d?ajouter un index ?

      /*Sur les tables contenant beaucoup de données, ou qu'on appelle fréquemment. Dans notre cas
      sur le champ jouPseudo de la table t_joueur. Car ce champ est appelé à chaque connexion du joueur et est généralement unique.