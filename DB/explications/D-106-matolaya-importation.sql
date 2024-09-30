/* 1.   Importer et créer la base de données.

        Il nous est donné un fichier d'importation SQL contenant diverses données (db_space_invaders.sql). Pour l'importer dans PhpMyAdmin
        en utilisant docker, il faut suivre ses étapes. Commencer par ouvrir l'invite de commande (CMD). *TIPS : Ouvrer le cmd depuis le dossier ou se trouve le fichier sql,
        cela évite de devoir recopier le path en entier.* Puis taper la commande suivante : */

        docker exec -i nom_du_conteneur mysql -u nom_d_utilisateur_sql -pmot_de_passe_sql < path/to/sqlfile

        --Dans mon cas, cela donnera : 
        docker exec -i db mysql -u root -proot < db_space_invaders.sql

        /*Voici une explication détaillée de la commande : 
            docker : Sert à utiliser Docker afin de gérér des conteneurs.
            exec : Exécute une commande dans un conteneur actif.
            -i : Mode interactif pour envoyer des données à MySQL
            db : nom du conteneur Docker où la commande est exécutée.
            mysql : Lance le cleint MySQL dans le conteneur spécifié.
            -u root : Se connecter à MySql avec l'utilisateur root.
            -proot : Se connecter au compte root avec le mot de passe root.
            < db_space_invaders.sql : Spécifier la localisation et le fichier sql à exécuter.*/