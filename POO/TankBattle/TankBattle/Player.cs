using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.Diagnostics.Tracing;

namespace TankBattle
{
    public class Player : DrawableGameComponent
    {
        private Texture2D texture; //Texture du joueur
        private SpriteFont spriteFont;
        private SpriteBatch _spriteBatch;

        //Constantes
        private readonly Vector2 DEFAULT_POS = new(0, 0);
        private Vector2 Position = new(0, 0);

        //variables
        private Vector2 Velocity;
        private const float SPEED = 20;

        //Limite du terrain de 50 pixels de marges
        private const int MarginLeftSize = 50;
        private readonly int MarginRightSize = Config.WindowWidth - 50;

        //Gun--------------------------------------------------------------------------
        //Timer du cooldown pour le tir
        private double _timerCoolDownShoot;
        private double _interval = 0.2f;

        //Munitions
        private int Ammo = 30;  //Chargeur contenant les munitions de l'arme.
        private bool isReloading = false;
        private double _timerGunReloading;
        private double _RealodingTime = 1.5f;

        public Player(Game game) : base(game)
        {
            DEFAULT_POS = new Vector2(Config.WindowWidth / 2, Config.WindowHeight - Config.WindowHeight / 8);
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
            //A chaque tics, rajouter le temps passé dans le timer 
            _timerCoolDownShoot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Si il ne dépasse pas les limites, on le fait bouger 
            if (CheckLimit())
            {
                Velocity = Input.GetMovementDirection() * SPEED;
                Position += Velocity.ToPoint().ToVector2();
                //Permet de limiter la position du joueur.
                Position = Vector2.Clamp(Position, new Vector2(MarginLeftSize + 1, Position.Y), new Vector2(MarginRightSize - 1, Position.Y));
            }

            //Regarder si le joueur à tirer.
            if (Input.IsShooting() && _timerCoolDownShoot >= _interval && Ammo > 0 && isReloading == false)
            {
                PlayerBullet bullet = new PlayerBullet(Game, Position);
                Game.Components.Add(bullet);

                //Réinitialiser le timer
                _timerCoolDownShoot = 0f;

                //Retirer une munition du chargeur 
                Ammo--;
            }
            //Regarder si le joueur appuie sur 'R' pour recharger son arme.
            if(Input.IsReloading()) isReloading = true;
            //Regarder s'il est en train de recharger.
            CheckReload(gameTime);

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 0.5f, SpriteEffects.None, 0f);
            //isReloading = false ? _spriteBatch.DrawString(spriteFont, Ammo.ToString(), new Vector2(Config.WindowWidth - 50, Config.WindowHeight - 50), Color.White) : _spriteBatch.DrawString(spriteFont, Ammo.ToString(), new Vector2(Config.WindowWidth - 50, Config.WindowHeight - 50), Color.Red);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void CheckReload(GameTime gameTime)
        {
            //checker si le joueur est en train de charger.
            if (isReloading)
            {
                //Lancer un timer du temps de chargement
                _timerGunReloading += gameTime.ElapsedGameTime.TotalSeconds;

                //Regarder si le joueur a attendu suffisement de temps.
                if(_timerGunReloading >= _RealodingTime)
                {
                    //S'il a assez attendu.
                    isReloading = false;
                    //Les munitions repassent à 30.
                    Ammo = 30;
                    //Et le temps se réinitialise.
                    _timerGunReloading = 0f;
                }
            }
        }
        private bool CheckLimit() => Position.X >= MarginLeftSize && Position.X <= MarginRightSize;
    }
}
