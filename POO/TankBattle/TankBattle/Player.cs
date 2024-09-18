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

        //Limite du terrain de 50 pixels de marges
        private const int MarginLeftSize = 50;
        private readonly int MarginRightSize;

        public Player(Game game, int window_width, int window_height) : base(game)
        {
            DEFAULT_POS = new Vector2(window_width / 2, window_height - window_height / 8);
            Position = DEFAULT_POS;
            MarginRightSize = window_width - 50;
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
            //Si il ne dépasse pas les limites, on le fait bouger 
            if (CheckLimit())
            {
                Velocity = Input.GetMovementDirection() * SPEED;
                Position += Velocity.ToPoint().ToVector2();
                //Permet de limiter la position du joueur.
                Position = Vector2.Clamp(Position, new Vector2(MarginLeftSize + 1, Position.Y), new Vector2(MarginRightSize - 1, Position.Y));
            }
            //Regarder si le joueur à tirer.
            if (Input.GetShootStatement())
            {

            }
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private bool CheckLimit() => Position.X >= MarginLeftSize && Position.X <= MarginRightSize;
    }
}
