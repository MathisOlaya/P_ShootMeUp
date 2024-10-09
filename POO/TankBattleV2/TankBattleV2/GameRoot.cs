using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TankBattleV2
{
    public class GameRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        public static SpriteBatch spriteBatch;
        public static SpriteFont spriteFont;

        //Game
        public static int Score { get; set; }
        public static float GameTimer;

        Level lvl;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Changer la taille de la fenêtre.
            _graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT;
            //Appliquer les changements de taille.
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Charger la font. 
            spriteFont = Content.Load<SpriteFont>("Fonts/Font");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger toutes les textures dès le lancement
            Visuals.LoadTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                lvl = new Level(1);
            }
                
            if(lvl != null)
                lvl.Update(gameTime);
            Game(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Effectuer la méthode Draw pour chaque méthode
            EntityManager.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, Score.ToString(), new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 75), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private static void Game(GameTime gameTime)
        {
            GameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
            Score += (int)GameTimer;
        }
    }
}
