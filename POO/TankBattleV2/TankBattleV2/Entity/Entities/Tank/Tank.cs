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
    /// Classe qui contient toute la logique du tank, en comprenant les déplacements, tir, vie etc...
    /// </summary>
    public class Tank : Entity, IMovable, IShootable
    {
        /// <summary>
        /// Propriété vélocité issue de l'interface IMovable
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Propriété de la vitesse issue de l'interface IMovable
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Temps définissant le temps entre le dernier tir, issu de l'interface IShootable.
        /// </summary>
        public float TimeSinceLastShot { get; set; }
        /// <summary>
        /// Temps requis minimum entre chaque tir, issu de l'interface IShootable.
        /// </summary>
        public float TimeBetweenEveryShot { get; set; }
        /// <summary>
        /// Propriété définissant la direction du projectile. Issu de l'interface IShootable.
        /// </summary>
        public Vector2 Direction { get; set; }
        /// <summary>
        /// Propriété contenant le point d'apparition du Tank
        /// </summary>
        public Vector2 SpawnPoint { get; private set; }

        private float LifeBarScale;

        /// <summary>
        /// Constructeur de la classe tank
        /// </summary>
        /// <param name="position">Position du tank.</param>
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
        /// <summary>
        /// Méthode qui s'effectue lors de la création du tank. S'occupe de choisir la position du tank selon un dictionnaire.
        /// </summary>
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

        /// <summary>
        /// Méthode s'effectuant à chaque tic. S'occupe de le déplacer et de le faire tirer.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            MoveToScene(gameTime);
            Shoot(gameTime);
        }
        /// <summary>
        /// Méthode dessinant le tank et ses différents attributs.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, Position, null, Color.White, MathF.PI, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
            //Dessiner la healthBar du tank
            SpriteBatch.Draw(EntityConfig.Tank.LifeBarTextures[HealthPoint], new Vector2(Position.X, Position.Y - 55 * LifeBarScale), null, Color.White, 0f, new Vector2(EntityConfig.Tank.LifeBarTextures[HealthPoint].Width / 2, EntityConfig.Tank.LifeBarTextures[HealthPoint].Height / 2), LifeBarScale, SpriteEffects.None, 0f);
        }
        /// <summary>
        /// Permet de faire avancer le tank sur la scène.
        /// </summary>
        /// <param name="gameTime"></param>
        private void MoveToScene(GameTime gameTime)
        {
            //Les tanks apparaissent avec une position négative en dehors de l'écran. Ils avancent jusqu'a une position Y précise, puis commencent à tirer.
            Position += new Vector2(0, (Position.Y < EntityConfig.Tank.LIMITE_POSITION_Y) ? 1 : 0);
            HitBox.Location = new Point((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale));

            //Dès qu'il arrive, lancer le timer de délai entre chaque tir.
            if (Position.Y == EntityConfig.Tank.LIMITE_POSITION_Y)
                TimeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        /// <summary>
        /// Permet de faire tirer le tank.
        /// </summary>
        /// <param name="gameTime"></param>
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
                EntityManager.Add(new Bullet(CanonPosition, Direction, EntityConfig.Shell.Texture, EntityConfig.Shell.Scale));
            }
        }
    }
}
