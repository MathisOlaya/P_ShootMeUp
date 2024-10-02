using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.Diagnostics.Tracing;

namespace TankBattle
{
    public class Protection : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        //Variables
        private Texture2D texture;
        private Vector2 Position {  get; set; }

        public Protection(Game game, Vector2 mousePosition) : base(game)
        {
            Position = mousePosition;
        }
        public override void Initialize()
        {
            base.Initialize();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Game.Content.Load<Texture2D>("sand-bag");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 5f, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
