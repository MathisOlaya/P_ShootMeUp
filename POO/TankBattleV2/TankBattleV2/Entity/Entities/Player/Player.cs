using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace TankBattleV2
{
    public class Player : Entity, IMovable, IShootable
    {
        private Vector2 MarginSide = new Vector2(50, Config.WINDOW_WIDTH - 50);     //Variable contenant la marge max entre la limite de la map et le joueur.

        //Proviens de l'interface IMovable
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        //Proviens de l'interface IShootable
        public float TimeSinceLastShot { get; set; }
        public float TimeBetweenEveryShot { get; set; }
        public Vector2 Direction { get; set; }

        //Munitions
        private int Ammo;
        private Vector2 AmmoCapacityLocation = new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 75);
        private bool isReloading = false;
        private float TimeSinceReloadingStarted;
        private float TimeForReloading;
        private Color AmmoStringColor = Color.White;

        //Life
        public bool isAlive = true;
        private Texture2D HealthPointTexture;
        private readonly Vector2 HEALTH_SPRITE_DEFAULT_POS;

        private MouseState _previousMouseState;

        public Player(Texture2D texture, SpriteFont spriteFont, SpriteBatch spriteBatch, Vector2 position, int healthPoint, Vector2 healthSpritePosition, float scale, Rectangle hitBox, float speed, float coolDownShoot, int ammo, float timeForReloading, Texture2D healthPointTexture) : base(texture, spriteFont, spriteBatch, position, healthPoint, healthSpritePosition, scale, hitBox)
        {
            Speed = speed;
            TimeBetweenEveryShot = coolDownShoot;
            Ammo = ammo;
            TimeForReloading = timeForReloading;
            HEALTH_SPRITE_DEFAULT_POS = healthSpritePosition;
            HealthPointTexture = healthPointTexture;
        }

        public override void Initialize()
        {
            //La munition du joueur va tout le temps vers le haut.
            Direction = new Vector2(0, -1);
            Position = new Vector2(Config.WINDOW_WIDTH / 2 - (Texture.Width / 2 * Scale), Config.WINDOW_HEIGHT - 175);    //Positionner le joueur en bas au centre de l'écran.
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));  //Créer la HitBox du joueur.
        }

        public override void Update(GameTime gameTime)
        {
            MovePlayer();
            Shoot(gameTime);
            Protection();
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            //Dessiner le joueur
            SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
            //Dessiner le nombre de balles restantes dans le chargeur
            SpriteBatch.DrawString(SprintFont, Ammo.ToString(), AmmoCapacityLocation, AmmoStringColor);
            SpriteBatch.Draw(EntityConfig.Bullet.IconTexture, new Vector2(AmmoCapacityLocation.X - 30, AmmoCapacityLocation.Y + 5), null, Color.White, 0f, new Vector2(0,0) , 1.2f, SpriteEffects.None, 0f);
            //Dessiner le nombre de vies restantes au joueur.
            // Repositionner la position du premier casque, à la position par défaut. Sinon les casques avanceront de 50 à chaque tic jusqu'à sortir de l'écran
            HealthSpritePosition = HEALTH_SPRITE_DEFAULT_POS;
            // Ecrire le nombre de vie
            for (int i = 0; i < HealthPoint; i++)
            {
                SpriteBatch.Draw(HealthPointTexture, HealthSpritePosition, null, Color.White, 0f, new Vector2(HealthPointTexture.Width / 2, HealthPointTexture.Height / 2), 1.5f, SpriteEffects.None, 0f);
                HealthSpritePosition.X += 50;
            }

            SpriteBatch.End();
        }
        private void MovePlayer()
        {
            Velocity = GlobalHelpers.Input.GetMovementDirection() * Speed;
            Position += Velocity.ToPoint().ToVector2();
            Position = Vector2.Clamp(Position, new Vector2(MarginSide.X, Position.Y), new Vector2(MarginSide.Y, Position.Y));
            HitBox.Location = new Point((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale));
        }
        private void Shoot(GameTime gameTime)
        {
            //Regarder si le joueur est en train de recharger
            if (GlobalHelpers.Input.isReloading()) isReloading = true;

            //S'il recharge, augmenter le timer d'attente.
            if (isReloading)
            {
                //Changer la couleur de la capacité en rouge
                AmmoStringColor = Color.Red;

                //Augmenter le timer
                TimeSinceReloadingStarted += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Vérifier si le joueur a assez attendu
                if(TimeSinceReloadingStarted >= TimeForReloading)
                {
                    //Réinitialiser le timer
                    TimeSinceReloadingStarted = 0;

                    //Remettre les munitions complètes
                    Ammo = EntityConfig.Player.AmmoCapacity;

                    //Il ne recharge plus
                    isReloading = false;

                    //Re-afficher les munitions en blancs
                    AmmoStringColor = Color.White;
                }
            }

            //Incrementér le timer du cooldown entre chaque tir à chaque tic.
            TimeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Vérifier si le joueur tir, et si le temps requis entre chaque tir est passé. Et si le joueur à encore des balles dans son chargeur. Et s'il n'est pas en train de recharger.
            if (GlobalHelpers.Input.isShooting() && TimeSinceLastShot >= TimeBetweenEveryShot && Ammo > 0 && !isReloading)
            {
                //Réinitialiser le timer
                TimeSinceLastShot = 0;

                /*Calculer la position du canon, car la position du joueur se trouve au centre du sprite, alors que 
                 * la sortie du canon est positionnée ailleurs. Cependant il n'existe aucun calcul me permettant de trouver la position. 
                J'ai donc du calculer à taton*/
                Vector2 CanonPosition = new Vector2(Position.X + 28 * EntityConfig.Player.Scale, Position.Y - 120 * EntityConfig.Player.Scale);

                //Créer une munition
                EntityManager.Add(new Bullet(EntityConfig.Bullet.Texture, SprintFont, GameRoot.spriteBatch, CanonPosition, EntityConfig.Bullet.HealthPoint, new Vector2(0, 0), EntityConfig.Bullet.Scale, EntityConfig.Bullet.HitBox, Direction, EntityConfig.Bullet.Speed));

                //Retirer une munition du chargeur 
                Ammo--;
            }
        }
        private void Protection()
        {
            if (GlobalHelpers.Input.isPlacingProtection && _previousMouseState.RightButton == ButtonState.Released)
            {
                EntityManager.Add(new Protection(EntityConfig.Protection.Texture, SprintFont, SpriteBatch, GlobalHelpers.Input.GetMousePosition(), EntityConfig.Protection.HealthPoint, new Vector2(0, 0), EntityConfig.Protection.Scale, EntityConfig.Protection.HitBox));
            }
            //Enregistrer l'état précédent de la souris. Permet de s'assure que le joueur à bien relacher le clic droit avant d'en reposer un.
            _previousMouseState = GlobalHelpers.Input.GetMouseState();
        }
    }
}
