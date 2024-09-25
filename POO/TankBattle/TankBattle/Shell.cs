using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Shell : DrawableGameComponent
    {
        private Texture2D texture; //Texture de la munition.
        private SpriteFont spriteFont;
        private SpriteBatch _spriteBatch;

        //Variables 
        private Vector2 _ShellPosition;
        private Vector2 _Direction;
        private const float _SPEED = 10;
        private Vector2 _Velocity;

        public Shell(Game game, Vector2 TankPosition) : base(game)
        {
            /*Calculer la position du canon, car la position du tank se trouve au centre du sprite, alors que 
             * la sortie du canon est positionnée ailleurs. Cependant il n'existe aucun calcul me permettant de trouver la position. 
            J'ai donc du calculer à taton*/
            _ShellPosition = new Vector2(TankPosition.X, TankPosition.Y +85);
        }
        public override void Initialize()
        {
            base.Initialize();

            //Calculer la direction du joueur depuis le tank : posJoueur - posTank .Normaliser afin d'avoir une valeur entre 0 et 1.
            _Direction = (GameRoot.Player.Position - _ShellPosition);
            _Direction.Normalize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger le sprite.
            texture = Game.Content.Load<Texture2D>("TankBullet");

            Console.Write("Player : " + GameRoot.Player.Position.ToString() + "\n" + "Shell : " + _ShellPosition); 
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Calculer la vélocité 
            _Velocity = _Direction * _SPEED;
            _ShellPosition += _Velocity;

            //Regarder si la "shell" rentre en collision avec le joueur.
            CheckCollisions();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, _ShellPosition, null, Color.White, (float)(MathF.Atan2(_Direction.Y, _Direction.X) - Math.PI / 2), new Vector2(texture.Width / 2, texture.Height / 2), 0.15f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        
        private void CheckCollisions()
        {
            if(CollisionHelpers.IsCollidingWith(_ShellPosition, GameRoot.Player))
            {
                //Afficher le joueur comme mort
                GameRoot.Player.IsAlive = false;

                //Supprimer le joueur
                Game.Components.Remove(GameRoot.Player);

                //Supprimer la munition
                Game.Components.Remove(this);
            }
        }
    }
}
