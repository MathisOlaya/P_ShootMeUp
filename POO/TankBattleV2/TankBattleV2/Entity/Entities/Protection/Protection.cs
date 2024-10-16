﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    public class Protection : Entity
    {
        public Protection(Texture2D texture, SpriteFont spriteFont, SpriteBatch spriteBatch, Vector2 position, int healthPoint, Vector2 healthSpritePosition, float scale, Rectangle hitBox) : base(texture, spriteFont, spriteBatch, position, healthPoint, healthSpritePosition, scale, hitBox)
        {
        }

        public override void Initialize()
        {
            //Calculer la hitbox
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
        }

        public override void Update(GameTime gameTime)
        {
            Console.Write("");
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
            SpriteBatch.End();
        }
    }
}
