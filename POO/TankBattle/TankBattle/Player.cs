using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.Tracing;

namespace TankBattle
{
    public class Player : DrawableGameComponent
    {
        private Texture2D texture; //Texture du joueur
        private SpriteFont spriteFont;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //HitBox du joueur

        //Constantes
        private readonly Vector2 DEFAULT_POS = new(0, 0);
        private Vector2 Position = new(0, 0);

        //variables
        private Vector2 Velocity;
        private const float SPEED = 20;

        public Player(Game game, int window_width, int window_height) : base(game)
        {
            DEFAULT_POS = new Vector2(window_width / 2, window_height - window_height / 8);
            Position = DEFAULT_POS;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Ajouter le sprite du joueur
            texture = Game.Content.Load<Texture2D>("player");
            spriteFont = Game.Content.Load<SpriteFont>("Font");
        }
        public override void Update(GameTime gameTime)
        {
            Velocity = Input.GetMovementDirection() * SPEED;
            Position += Velocity.ToPoint().ToVector2();
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
