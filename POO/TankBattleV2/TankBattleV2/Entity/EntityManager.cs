﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    public static class EntityManager
    {
        //Contient toutes les méthodes que les entités doivent exécuter.
        //Liste qui contient chaque entités 
        public static List<Entity> Entities = new List<Entity>();

        public static Player Player;

        public static int TankKilled = 0;

        public static int LevelID = 1;


        public static void Add(Entity entity)
        {
            Entities.Add(entity);

            //A chaque création, effectuer la méthode Initialize
            entity.Initialize();
        }
        public static void Remove(Entity entity)
        {
            Entities.Remove(entity);
        }
        public static void Initialize(int levelDifficulty)
        {
            //Add player only if doesn't exist
            if(Player == null)
            {
                Player = new Player(EntityConfig.Player.Texture, GameRoot.spriteFont, GameRoot.spriteBatch, EntityConfig.Player.Position, EntityConfig.Player.HealthPoint, EntityConfig.Player.HealthPointSpritePosition, EntityConfig.Player.Scale, EntityConfig.Player.HitBox, EntityConfig.Player.Speed, EntityConfig.Bullet.CoolDownShoot, EntityConfig.Player.AmmoCapacity, EntityConfig.Player.TimeForReloading, EntityConfig.Player.HealthPointTexture);
                Add(Player);
            }
            //Add tank
            for (int i = 0; i < levelDifficulty; i++)
            {
                // Ajouter le nouveau tank avec la position valide
                Add(new Tank(EntityConfig.Tank.Texture, GameRoot.spriteFont, GameRoot.spriteBatch, EntityConfig.Tank.Position, EntityConfig.Tank.HealthPoint, EntityConfig.Tank.HealthPointSpritePosition, EntityConfig.Tank.Scale, EntityConfig.Tank.HitBox, EntityConfig.Tank.LifeBarScale, EntityConfig.Shell.CoolDownShoot));
            }
            Console.Clear();
            foreach (var kvp in EntityConfig.Tank.spawnPoints)
            {
                // `kvp.Key` est la clé, et `kvp.Value` est la valeur
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
            
        }
        public static void Update(GameTime gameTime)
        {
            foreach (Entity entity in Entities.ToArray())
            {
                entity.Update(gameTime);
            }
            CheckCollisions();
        }
        public static void Draw(GameTime gameTime)
        {
            foreach (Entity entity in Entities.ToArray())
            {
                entity.Draw(gameTime);
            }
        }
        public static void DeleteEntity()
        {
            //Supprimer chaque entité
            Entities.Clear();
        }
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
                        Add(new Tank(EntityConfig.Tank.Texture, GameRoot.spriteFont, GameRoot.spriteBatch, EntityConfig.Tank.Position, EntityConfig.Tank.HealthPoint, EntityConfig.Tank.HealthPointSpritePosition, EntityConfig.Tank.Scale, EntityConfig.Tank.HitBox, EntityConfig.Tank.LifeBarScale, EntityConfig.Shell.CoolDownShoot));

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
