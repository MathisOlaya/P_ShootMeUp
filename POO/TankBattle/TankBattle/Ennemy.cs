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
        public Vector2 EnnemyPosition { get; private set; }
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
            EnnemyPosition = new Vector2(GlobalHelpers.GenerateRandom(50, Config.WindowWidth - 50), -150);
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

            //Vérifier si le CoolDown est fini, si oui tirer et le réinitialiser
            if(_timerCoolDownShoot >= _interval)
            {
                //Lancer un missile depuis le tank.
                Shell shell = new Shell(Game, EnnemyPosition);
                Game.Components.Add(shell);
                //Réinitialiser le chrono
                _timerCoolDownShoot = 0;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, EnnemyPosition, null, Color.White, MathF.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        private void MoveToScene()
        {
            if (EnnemyPosition.Y != _EnnemyPositionY)
            {
                EnnemyPosition  = new Vector2(EnnemyPosition.X, EnnemyPosition.Y + 1);
            }
            else
                IsTankReadyToShoot = true;
        }
    }
}
