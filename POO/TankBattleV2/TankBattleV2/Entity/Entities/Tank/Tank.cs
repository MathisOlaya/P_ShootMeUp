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
        public int HowLongHeAdvanced { get; set; }

        private float LifeBarScale;

        public Tank(Texture2D texture, SpriteFont spriteFont, SpriteBatch spriteBatch, Vector2 position, int healthPoint, Vector2 healthSpritePosition, float scale, Rectangle hitBox, float lifeBarScale, float coolDownShoot) : base(texture, spriteFont, spriteBatch, position, healthPoint, healthSpritePosition, scale, hitBox)
        {
            LifeBarScale = lifeBarScale;
            TimeBetweenEveryShot = coolDownShoot;
        }

        public override void Initialize()
        {
            /* Durant cette partie de génération aléatoire de position, ChatGPT m'est venu en aide car je ne savais comment utiliser cette outil (dictionnaire). 
             Cependant cela ne m'empêche pas d'avoir fait l'effort de comprendre derrière et de pouvoir le refaire seul. Je tenais quand même à le préciser 
            étant donné que cette technologie n'avait pas encore été vue en cours. *Les commentaires ont été réalisé par moi.**/

            //Permet de créer une liste en sélectionnant uniquement les positions étant non-occupées (donc dont le booléan attribué est true).
            var availableSpawnPoints = EntityConfig.Tank.spawnPoints
                .Where(sp => sp.Value) //Seulement les positions non-occupées.
                .Select(sp => sp.Key) //Renseigner quel élement enregistrer dans la liste.
                .ToList(); //Spécifier le format.

            //Permet de vérifier si des points d'apparitions sont disponibles. Sinon, ils sont tous utilisés.
            if (availableSpawnPoints.Count > 0)
            {
                //Si il y a des points d'apparations disponibles, en choisir un aléatoirement afin d'éviter de prendre celui le plus à gauche à chaque fois.
                int randomIndex = GlobalHelpers.Random.Next(0, availableSpawnPoints.Count);
                Position = availableSpawnPoints[randomIndex]; //Attribuer une position a partir de la liste de positions disponible en utilisant un index aléatoire.

                //Puis, marquer le point d'apparation en indisponible, donc en changeant le bool.
                EntityConfig.Tank.spawnPoints[Position] = false;
            }
            else
            {
                //Si il n'y a plus de place,  supprimer le tank, sinon il va se superposer sur les autres.
                EntityManager.Remove(this);
            }
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

            if (Position.Y < EntityConfig.Tank.LIMITE_POSITION_Y)
                HowLongHeAdvanced += 1;

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
                EntityManager.Add(new Bullet(EntityConfig.Shell.Texture, SprintFont, SpriteBatch, CanonPosition, EntityConfig.Shell.HealthPoint, new Vector2(0, 0), EntityConfig.Shell.Scale, EntityConfig.Shell.HitBox, Direction, EntityConfig.Shell.Speed));
            }
        }
    }
}
