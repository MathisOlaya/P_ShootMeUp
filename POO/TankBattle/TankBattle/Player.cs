using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankBattle
{
    public class Player : DrawableGameComponent
    {
        private Texture2D texture; //Texture du joueur
        private SpriteFont spriteFont;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //HitBox du joueur
        Rectangle Hitbox;

        //Constantes
        private readonly Vector2 DEFAULT_POS = new(100, 100);

        //variables
        private Vector2 Velocity;
        private const float SPEED = 20;

        public Player(Game game, int window_width, int window_height) : base(game)
        {
            DEFAULT_POS = new Vector2(window_width / 2, window_height);
        }
        public override void Initialize()
        {
            base.Initialize();

            //Créer un rectangle qui servira d'hit box du joueur
            Hitbox = new Rectangle((int)DEFAULT_POS.X, (int)DEFAULT_POS.Y, texture.Width, texture.Height);
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
            Hitbox.Location += Velocity.ToPoint();
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Hitbox.Location.ToVector2(), null, Color.White, 0f, Hitbox.Location.ToVector2(), 0.6f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(spriteFont, Hitbox.Location.ToString(), new Vector2(20, 20), Color.Red);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
