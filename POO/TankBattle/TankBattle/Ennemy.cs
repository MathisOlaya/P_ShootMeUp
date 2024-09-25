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
        public Texture2D texture; //Texture du joueur
        private SpriteFont spriteFont;
        private SpriteBatch _spriteBatch;

        //Variables 
        public Vector2 Position { get; private set; }
        private const int _EnnemyPositionY = 100; //Position finale en Y du tank.
        private bool IsTankReadyToShoot = false;

        //Timer 
        private double _timerCoolDownShoot;
        private double _interval = 3f;

        public Ennemy(Game game) : base(game)
        {
            
        }
        public override void Initialize()
        {
            base.Initialize();


            //Calculer la position de départ, le faire apparaitre en dehors de l'écran, et le faire avancer pour une animation.
            Position = new Vector2(GlobalHelpers.GenerateRandom(50, Config.WindowWidth - 50), -150);

            //Permet de vérifier que tous les tanks de la liste ont une valeur. Donc si le dernier à une valeur, ils en tous.
            if (GameRoot.Tanks[GameRoot.TANK_NUMBERS - 1].Position.Y != 0)
            {
                //Sélectionner tous les tanks de la liste.
                for (int i = 0; i < GameRoot.Tanks.Count; i++)
                {
                    //Permet de vérifier que le tank que nous vérifions n'est pas celui que nous venons de créer. Sinon la position sera tjrs pareille.
                    if (GameRoot.Tanks[i].GetHashCode() != this.GetHashCode())
                    {
                        //Console.WriteLine("\nTank : " + this.GetHashCode() + " Position " + Position + "\nTank : " + GameRoot.Tanks[i].GetHashCode() + " Position " + GameRoot.Tanks[i].Position);
                        if (this.Position.X <= GameRoot.Tanks[i].Position.X + GameRoot.Tanks[i].texture.Width && this.Position.X >= GameRoot.Tanks[i].Position.X - GameRoot.Tanks[i].texture.Width)
                        {
                            Position = new Vector2(GlobalHelpers.GenerateRandom(50, Config.WindowWidth - 50), -150);
                        }
                    }
                }
            }



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

            //Si le tank est positionné (prêt a tirer), lancer le timer de ShootCoolDown
            if(IsTankReadyToShoot)
            {
                _timerCoolDownShoot += gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Vérifier si le CoolDown est fini et si le joueur est vivant, si oui tirer et le réinitialiser
            if(_timerCoolDownShoot >= _interval && GameRoot.Player != null)
            {
                //Lancer un missile depuis le tank.
                Shell shell = new Shell(Game, Position);
                Game.Components.Add(shell);
                //Réinitialiser le chrono
                _timerCoolDownShoot = 0;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Position, null, Color.White, MathF.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        private void MoveToScene()
        {
            if (Position.Y != _EnnemyPositionY)
            {
                Position  = new Vector2(Position.X, Position.Y + 1);
            }
            else
                IsTankReadyToShoot = true;
        }
    }
}
