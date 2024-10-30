using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    /// <summary>
    /// Classe servant à créer les munitions joueurs et/ou tanks
    /// </summary>
    public class Bullet : Entity, IMovable
    {
        /// <summary>
        /// Propriété vélocité issue de l'interface IMovable
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Propriété de la vitesse issue de l'interface IMovable.
        /// </summary>
        public float Speed { get; set; }

        private Vector2 Direction;

        /// <summary>
        /// Constructeur de la bullet.
        /// </summary>
        /// <param name="position">Position de départ de la munition</param>
        /// <param name="direction">Direction de la munition</param>
        /// <param name="texture">Texture de la munition</param>
        
        public Bullet(Vector2 position, Vector2 direction, Texture2D texture, float scale) : base(position)
        {
            Texture = texture;
            SpriteFont = GameRoot.spriteFont;
            SpriteBatch = GameRoot.spriteBatch;
            HealthPoint = EntityConfig.Bullet.HealthPoint;
            Scale = scale;
            HitBox = EntityConfig.Bullet.HitBox;
            Speed = EntityConfig.Bullet.Speed;
            Direction = new Vector2();
            Position = position;
            Direction = direction;
        }
        /// <summary>
        /// Méthode étant effectuée lors de la création de la bullet.
        /// </summary>
        public override void Initialize()
        {
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
        }
        /// <summary>
        /// Méthode étant effectuée à chaque tics, cette méthode est propore à chaque instance de bullet.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            UpdatePosition();
            CheckLimitePosition();
        }
        /// <summary>
        /// Méthode qui dessine la bullet.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, Position, null, Color.White, (float)(MathF.Atan2(Direction.Y, Direction.X) - Math.PI / 2), new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
        }
        /// <summary>
        /// Méthode qui update la position de la munition, appelée à chaque tics.
        /// </summary>
        private void UpdatePosition()
        {
            Velocity = Speed * Direction;
            Position += Velocity;
            HitBox.Location = new Point((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale));
        }
        /// <summary>
        /// Méthode regardant à chaque tic si la munition est encore dans l'écran, sinon la supprimer.
        /// </summary>
        private void CheckLimitePosition()
        {
            if (isOutOfBounds)
                EntityManager.Remove(this);
        }
        /// <summary>
        /// Propriété retournant si la Position est dans la limite ou pas.
        /// </summary>
        /// <returns>True si elle est en dehors de l'écran.</returns>
        private bool isOutOfBounds => Position.Y < 0 || Position.Y > Config.WINDOW_HEIGHT;
    }
}
