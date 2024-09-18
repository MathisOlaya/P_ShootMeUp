using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Ennemy : DrawableGameComponent
    {
        private Texture2D texture; //Texture du joueur
        private SpriteFont spriteFont;
        private SpriteBatch _spriteBatch;

        //Variables 
        private Vector2 _EnnemyPosition;
        private const int _EnnemyPositionY = 100; //Position finale en Y du tank.

        public Ennemy(Game game) : base(game)
        {
            
        }
        public override void Initialize()
        {
            base.Initialize();

            //Calculer la position de départ, le faire apparaitre en dehors de l'écran, et le faire avancer pour une animation.
            _EnnemyPosition = new Vector2(RandomNumber.Generate(50, Config.WindowWidth - 50), -150);
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger le sprite 
            texture = Game.Content.Load<Texture2D>("tank");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Regarder si le tank ne se trouve pas sur la ligne, et le faire avancer si ce n'est pas le cas.
            MoveToScene();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, _EnnemyPosition, null, Color.White, MathF.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        private void MoveToScene()
        {
            if(_EnnemyPosition.Y != _EnnemyPositionY)
            {
                _EnnemyPosition.Y += 1;
            }
        }
    }
}
