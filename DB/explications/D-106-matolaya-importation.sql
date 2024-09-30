/* 1.   Importer et cr�er la base de donn�es.

        Il nous est donn� un fichier d'importation SQL contenant diverses donn�es (db_space_invaders.sql). Pour l'importer dans PhpMyAdmin
        en utilisant docker, il faut suivre ses �tapes. Commencer par ouvrir l'invite de commande (CMD). *TIPS : Ouvrer le cmd depuis le dossier ou se trouve le fichier sql,
        cela �vite de devoir recopier le path en entier.* Puis taper la commande suivante : */

        docker exec -i nom_du_conteneur mysql -u nom_d_utilisateur_sql -pmot_de_passe_sql < path/to/sqlfile

        --Dans mon cas, cela donnera : 
        docker exec -i db mysql -u root -proot < db_space_invaders.sql

        /*Voici une explication d�taill�e de la commande : 
            docker : Sert � utiliser Docker afin de g�r�r des conteneurs.
            exec : Ex�cute une commande dans un conteneur actif.
            -i : Mode interactif pour envoyer des donn�es � MySQL
            db : nom du conteneur Docker o� la commande est ex�cut�e.
            mysql : Lance le cleint MySQL dans le conteneur sp�cifi�.
            -u root : Se connecter � MySql avec l'utilisateur root.
            -proot : Se connecter au compte root avec le mot de passe root.
            < db_space_invaders.sql : Sp�cifier la localisation et le fichier sql � ex�cuter.*/