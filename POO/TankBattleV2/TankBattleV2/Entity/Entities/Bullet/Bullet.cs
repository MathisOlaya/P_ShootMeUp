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
    public class Bullet : Entity, IMovable
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        private Vector2 Direction;

        public Bullet(Texture2D texture, SpriteFont sprifeFont, SpriteBatch spriteBatch, Vector2 position, int healthPoint, Vector2 healthPointPosition, float scale, Rectangle hitBox, Vector2 direction, float speed) : base(texture, sprifeFont, spriteBatch, position, healthPoint, healthPointPosition, scale, hitBox)
        {
            Speed = speed;
            Direction = new Vector2();
            Position = position;
            Direction = direction;
        }

        public override void Initialize()
        {
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePosition();
            CheckLimitePosition();
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, null, Color.White, (float)(MathF.Atan2(Direction.Y, Direction.X) - Math.PI / 2), new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
            SpriteBatch.End();
        }
        private void UpdatePosition()
        {
            Velocity = Speed * Direction;
            Position += Velocity;
            HitBox.Location = new Point((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale));
        }
        private void CheckLimitePosition()
        {
            if (isOutOfBounds())
                EntityManager.Remove(this);
        }
        private bool isOutOfBounds() => Position.Y < 0 || Position.Y > Config.WINDOW_HEIGHT;
    }
}
