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
    /// Cette classe contient la logique pour poser et dessiner une protection
    /// </summary>
    public class Protection : Entity
    {
        /// <summary>
        /// Constructeur de la protection
        /// </summary>
        /// <param name="position">Définis la position de la protection</param>
        /// <param name="hitBox">Hitbox de la protection.</param>
        public Protection(Vector2 position, Rectangle hitBox) : base(position)
        {
            Texture = EntityConfig.Protection.Texture;
            SpriteFont = GameRoot.spriteFont;
            SpriteBatch = GameRoot.spriteBatch;
            HealthPoint = EntityConfig.Protection.HealthPoint;
            Scale = EntityConfig.Protection.Scale;
            HitBox = hitBox;
        }
        /// <summary>
        /// Méthode qui s'effectue lors de la création de la protection
        /// </summary>
        public override void Initialize()
        {
            //Calculer la hitbox
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
        }

        /// <summary>
        /// Méthode qui s'effectue à chaque tics.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Console.Write("");
        }
        /// <summary>
        /// Méthode dessinant la protection.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
        }
    }
}
