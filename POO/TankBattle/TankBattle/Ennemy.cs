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
        private GraphicsDevice _graphicsDevice;
        private SpriteFont spriteFont;
        private SpriteBatch _spriteBatch;

        //Tableau
        public Texture2D[] LifeBarTextures = new Texture2D[3];

        //Variables 
        public Vector2 Position { get; private set; }
        private const int _EnnemyPositionY = 125; //Position finale en Y du tank.
        private bool IsTankReadyToShoot = false;

        //Timer 
        private double _timerCoolDownShoot;
        private double _interval = GlobalHelpers.GenerateRandom(15, 25) / 10;   //interval entre chaque tir du tank.

        //HP
        public int HealthPoint = 2;

        public Ennemy(Game game) : base(game)
        {

        }
        public override void Initialize()
        {
            base.Initialize();

            // Calculer la position de départ initiale
            Position = new Vector2(GlobalHelpers.GenerateRandom(50, Config.WindowWidth - 50), GlobalHelpers.GenerateRandom(-250,-50 ));

            GameRoot.Tanks.Add(this);

            bool isValidPosition = false;
            int maxAttempts = 100;  // Limite d'essais
            int attempts = 0;       // Compteur d'essais

            // Boucle jusqu'à ce que la position soit valide ou que le nombre maximum d'essais soit atteint
            while (!isValidPosition && attempts < maxAttempts)
            {
                isValidPosition = true;  // Supposons que la position est correcte au départ
                attempts++; // Incrémenter le compteur à chaque essai

                for (int i = 0; i < GameRoot.Tanks.Count; i++)
                {
                    // Vérifier que nous ne comparons pas avec le tank actuel
                    if (GameRoot.Tanks[i].GetHashCode() != this.GetHashCode())
                    {
                        // Vérifier le chevauchement horizontal
                        if (Position.X <= GameRoot.Tanks[i].Position.X + GameRoot.Tanks[i].texture.Width &&
                            Position.X >= GameRoot.Tanks[i].Position.X - GameRoot.Tanks[i].texture.Width)
                        {
                            // Si les positions se chevauchent, générer une nouvelle position et recommencer la vérification
                            Position = new Vector2(GlobalHelpers.GenerateRandom(50, Config.WindowWidth - 50), GlobalHelpers.GenerateRandom(-350, -150));
                            isValidPosition = false;  // La position n'est pas correcte, donc il faut continuer à vérifier
                            break;  // Recommencer la vérification des tanks avec la nouvelle position
                        }
                    }
                }
            }
            // Si le nombre maximum d'essais est atteint sans trouver de position, supprimer le tank
            if (!isValidPosition)
            {
                // Retirer le tank de la liste et des composants
                GameRoot.Tanks.Remove(this);
                Game.Components.Remove(this);
            }
        }


        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger le sprite 
            texture = Game.Content.Load<Texture2D>("tank");
            spriteFont = Game.Content.Load<SpriteFont>("Font");

            LifeBarTextures[0] = Game.Content.Load<Texture2D>("LifeBar/dead");
            LifeBarTextures[1] = Game.Content.Load<Texture2D>("LifeBar/mid");
            LifeBarTextures[2] = Game.Content.Load<Texture2D>("LifeBar/full");
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

            //Dessiner la barre de vie du tank.
            _spriteBatch.Draw(LifeBarTextures[HealthPoint], new Vector2(Position.X, Position.Y - 95), null, Color.White, 0f, new Vector2(LifeBarTextures[HealthPoint].Width / 2, LifeBarTextures[HealthPoint].Height / 2), 1.5f, SpriteEffects.None, 0f);

            //_spriteBatch.DrawString(spriteFont, HealthPoint.ToString(), new Vector2(Position.X, Position.Y - 105), Color.White);
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
