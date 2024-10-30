using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    /// <summary>
    /// Classe servant à gérér toutes les entités, créer, supprimer, initialiser, etc...
    /// </summary>
    public static class EntityManager
    {
        //Contient toutes les méthodes que les entités doivent exécuter.
        //Liste qui contient chaque entités 
        /// <summary>
        /// Liste static contenant toutes les entités actuellement en vie de la partie.
        /// </summary>
        public static List<Entity> Entities = new List<Entity>();

        /// <summary>
        /// Instance unique du joueur.
        /// </summary>
        public static Player Player;

        /// <summary>
        /// Nombre de tank éliminé. Permet de vérifier quand un joueur termine le niveau.
        /// </summary>
        public static int TankKilled = 0;

        /// <summary>
        /// Correspond au niveau actuel.
        /// </summary>
        public static int LevelID = 1;

        /// <summary>
        /// Permet d'ajouter à la liste une entité
        /// </summary>
        /// <param name="entity">Entité qui sera ajoutée à la liste.</param>
        public static void Add(Entity entity)
        {
            Entities.Add(entity);

            //A chaque création, effectuer la méthode Initialize
            entity.Initialize();
        }
        /// <summary>
        /// Méthode permettant la suppresion d'une entité de la liste.
        /// </summary>
        /// <param name="entity">Entité qui sera supprimée.</param>
        public static void Remove(Entity entity)
        {
            Entities.Remove(entity);
        }
        /// <summary>
        /// Méthode initialisant les nouvelles entitées.
        /// </summary>
        /// <param name="levelDifficulty"></param>
        public static void Initialize(int levelDifficulty)
        {
            //Add player only if doesn't exist
            if(Player == null)
            {
                Player = new Player(EntityConfig.Player.Position);
                Add(Player);
            }
            //Add tank
            for (int i = 0; i < levelDifficulty; i++)
            {
                // Ajouter le nouveau tank avec la position valide
                Add(new Tank(EntityConfig.Tank.Position));
            }
            
        }
        /// <summary>
        /// Méthode effectuant les méthodes qui s'effectuent à chaque tics.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            foreach (Entity entity in Entities.ToArray())
            {
                entity.Update(gameTime);
            }
            CheckCollisions();
        }
        /// <summary>
        /// Méthode dessinant chaque élement.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Draw(GameTime gameTime)
        {
            foreach (Entity entity in Entities.ToArray())
            {
                entity.Draw(gameTime);
            }
        }
        /// <summary>
        /// Méthode permettant de complètement supprimer l'entièrté du contenu de la liste des entités
        /// </summary>
        public static void DeleteEntity()
        {
            //Supprimer chaque entité
            Entities.Clear();
        }
        /// <summary>
        /// Méthode vérifiant s'il y a une collision avec d'autre entité, et d'effectuer des actions en conséquences.
        /// </summary>
        private static void CheckCollisions()
        {
            // Liste pour stocker les entités à supprimer
            List<Entity> entitiesToRemove = new List<Entity>();

            // Parcours unique des paires d'entités
            for (int i = 0; i < Entities.Count; i++)
            {
                for (int j = i + 1; j < Entities.Count; j++) 
                {
                    //J est incrémenter de 1 de plus que i afin d'éviter deux fois la même entité.
                    Entity entity = Entities[i];
                    Entity second_entity = Entities[j];

                    if (entity.HitBox.Intersects(second_entity.HitBox))
                    {
                        // Enlever une vie aux entités concernées
                        entity.HealthPoint -= 1;
                        second_entity.HealthPoint -= 1;

                        // Ajouter à la liste de suppression si nécessaire
                        if (entity.HealthPoint <= 0 && !entitiesToRemove.Contains(entity))
                            entitiesToRemove.Add(entity);
                        if (second_entity.HealthPoint <= 0 && !entitiesToRemove.Contains(second_entity))
                            entitiesToRemove.Add(second_entity);
                    }
                }
            }

            // Supprimer les entités après la boucle
            foreach (Entity entity in entitiesToRemove)
            {
                //Si c'est le joueur, l'afficher commme mort.
                if (entity is Player player)
                    player.isAlive = false;

                Remove(entity);

                //Si c'est un tank, remettre sa position en disponible.
                if (entity is Tank tank)
                {
                    //Incrémenter le score
                    GameRoot.Score += 5;

                    //Incrementer le nombre de tank tuer.
                    TankKilled++;

                    //Si le mode infini est activé. Ajouter un tank à chaque mort.
                    if (GameSettings.InfiniteMode)
                        Add(new Tank(EntityConfig.Tank.Position));

                    //Vérifier si le joueur a tuer tous les tanks 
                    if (TankKilled >= GameSettings.Difficulty && !GameSettings.InfiniteMode)
                    {
                        //Supprimer le lvl 
                        GameRoot.lvl = null;

                        //Réinitialiser le nombre d'ennemi tuer.
                        TankKilled = 0;

                        //Augmenter la difficulté (+2 nmbre de tank) seulement s'il n'y a pas déjà 2 tanks. Sinon lancer le mode réaparition infinies.
                        if (GameSettings.Difficulty != 8)
                            GameSettings.Difficulty += 2;
                        else
                            GameSettings.InfiniteMode = true;

                        //Créer le lvl.
                        GameRoot.lvl = new Level(GameSettings.Difficulty);
                        LevelID++;
                    }

                    if (EntityConfig.Tank.spawnPoints.ContainsKey(tank.SpawnPoint))
                    {
                        // Mettre à jour la valeur
                        EntityConfig.Tank.spawnPoints[tank.SpawnPoint] = true;
                    }
                }
            }
        }

    }
}
