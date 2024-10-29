using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    internal class Tank : Entity, IMovable, IShootable
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public float TimeSinceLastShot { get; set; }
        public float TimeBetweenEveryShot { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 SpawnPoint { get; private set; }

        private float LifeBarScale;

        public Tank(Vector2 position) : base(position)
        {
            Texture = EntityConfig.Tank.Texture;
            SpriteFont = GameRoot.spriteFont;
            SpriteBatch = GameRoot.spriteBatch;
            HealthPoint = EntityConfig.Tank.HealthPoint;
            Scale = EntityConfig.Tank.Scale;
            LifeBarScale = EntityConfig.Tank.LifeBarScale;
            TimeBetweenEveryShot = EntityConfig.Shell.CoolDownShoot;
        }

        public override void Initialize()
        {
            /* Durant cette partie de génération aléatoire de position, ChatGPT m'est venu en aide car je ne savais comment utiliser cette outil (dictionnaire). 
             Cependant cela ne m'empêche pas d'avoir fait l'effort de comprendre derrière et de pouvoir le refaire seul. Je tenais quand même à le préciser 
            étant donné que cette technologie n'avait pas encore été vue en cours. *Les commentaires ont été réalisé par moi.**/

            //Permet de créer une liste en sélectionnant uniquement les positions étant non-occupées (donc dont le booléan attribué est true).
            // Créer une liste pour stocker les points d'apparition disponibles
            List<Vector2> availableSpawnPoints = new List<Vector2>();

            // Vérifier les points d'apparition et ajouter ceux qui sont disponibles
            foreach (var kvp in EntityConfig.Tank.spawnPoints)
            {
                if (kvp.Value) // Seulement les positions non-occupées
                {
                    availableSpawnPoints.Add(kvp.Key);
                }
            }

            // Vérifier si des points d'apparition sont disponibles
            if (availableSpawnPoints.Count > 0)
            {
                // Choisir un index aléatoire pour une position disponible
                int randomIndex = GlobalHelpers.Random.Next(0, availableSpawnPoints.Count);
                Position = availableSpawnPoints[randomIndex]; // Attribuer une position aléatoire

                // Marquer le point d'apparition en indisponible
                EntityConfig.Tank.spawnPoints[Position] = false;
            }
            else
            {
                // Si aucun point d'apparition n'est disponible, supprimer le tank
                EntityManager.Remove(this);
            }

            SpawnPoint = Position;
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale - 65));    //-65 afin d'éviter de prendre le canon en tant que collision, étant donné que c'est un rectangle, les parties vides se trouvant à cotér créeront des collisions inexistantes.
        }

        public override void Update(GameTime gameTime)
        {
            MoveToScene(gameTime);
            Shoot(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, Position, null, Color.White, MathF.PI, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
            //Dessiner la healthBar du tank
            SpriteBatch.Draw(EntityConfig.Tank.LifeBarTextures[HealthPoint], new Vector2(Position.X, Position.Y - 55 * LifeBarScale), null, Color.White, 0f, new Vector2(EntityConfig.Tank.LifeBarTextures[HealthPoint].Width / 2, EntityConfig.Tank.LifeBarTextures[HealthPoint].Height / 2), LifeBarScale, SpriteEffects.None, 0f);
        }

        private void MoveToScene(GameTime gameTime)
        {
            //Les tanks apparaissent avec une position négative en dehors de l'écran. Ils avancent jusqu'a une position Y précise, puis commencent à tirer.
            Position += new Vector2(0, (Position.Y < EntityConfig.Tank.LIMITE_POSITION_Y) ? 1 : 0);
            HitBox.Location = new Point((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale));

            //Dès qu'il arrive, lancer le timer de délai entre chaque tir.
            if (Position.Y == EntityConfig.Tank.LIMITE_POSITION_Y)
                TimeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        private void Shoot(GameTime gameTime)
        {
            //Si le temps entre le dernier tir est plus grand ou égal au temps requis entre chaque tir.
            if(TimeSinceLastShot >= TimeBetweenEveryShot && EntityManager.Player.isAlive)
            {
                //Reinitialiser le timer
                TimeSinceLastShot = 0;

                //Calculer la direction de la munitions.
                Direction = (EntityManager.Player.Position - Position);
                Direction = Vector2.Normalize(Direction);

                //Ajuster la sortie du missile sur le canon
                Vector2 CanonPosition = new Vector2(Position.X, Position.Y + 1800 * Scale);

                //Lancer un missile.
                EntityManager.Add(new Bullet(CanonPosition, Direction, EntityConfig.Shell.Texture));
            }
        }
    }
}
