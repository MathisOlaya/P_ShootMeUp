using Microsoft.Xna.Framework;
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
            //Add player
            Player = new Player(EntityConfig.Player.Texture, GameRoot.spriteFont, GameRoot.spriteBatch, EntityConfig.Player.Position, EntityConfig.Player.HealthPoint, EntityConfig.Player.HealthPointSpritePosition, EntityConfig.Player.Scale, EntityConfig.Player.HitBox, EntityConfig.Player.Speed, EntityConfig.Bullet.CoolDownShoot, EntityConfig.Player.AmmoCapacity, EntityConfig.Player.TimeForReloading, EntityConfig.Player.HealthPointTexture);
            Add(Player);
            //Add tank
            for (int i = 0; i < levelDifficulty; i++)
            {
                // Ajouter le nouveau tank avec la position valide
                Add(new Tank(EntityConfig.Tank.Texture, GameRoot.spriteFont, GameRoot.spriteBatch, EntityConfig.Tank.Position, EntityConfig.Tank.HealthPoint, EntityConfig.Tank.HealthPointSpritePosition, EntityConfig.Tank.Scale, EntityConfig.Tank.HitBox, EntityConfig.Tank.LifeBarScale, EntityConfig.Shell.CoolDownShoot));
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
                    GameRoot.Score += 500;

                    //Permet de changer la valeur "Value" à partir de la clé qui est un position.
                    EntityConfig.Tank.spawnPoints[new Vector2(tank.Position.X, -150)] = true;
                    Add(new Tank(EntityConfig.Tank.Texture, GameRoot.spriteFont, GameRoot.spriteBatch, EntityConfig.Tank.Position, EntityConfig.Tank.HealthPoint, EntityConfig.Tank.HealthPointSpritePosition, EntityConfig.Tank.Scale, EntityConfig.Tank.HitBox, EntityConfig.Tank.LifeBarScale, EntityConfig.Shell.CoolDownShoot));

                }
            }
        }

    }
}
