using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;



namespace TankBattleV2
{
    /// <summary>
    /// Cette classe contient toute la logique du joueur et hérite de deux interfaces, permettant de bouger et tirer.
    /// </summary>
    public class Player : Entity, IMovable, IShootable
    {
        private Vector2 MarginSide = new Vector2(50, Config.WINDOW_WIDTH - 50);     //Variable contenant la marge max entre la limite de la map et le joueur.

        //Proviens de l'interface IMovable
        /// <summary>
        /// Propriété vélocité issue de l'interface IMovable
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Propriété de la vitesse issue de l'interface IMovable
        /// </summary>
        public float Speed { get; set; }

        //Proviens de l'interface IShootable
        /// <summary>
        /// Temps définissant le temps entre le dernier tir, issu de l'interface IShootable.
        /// </summary>
        public float TimeSinceLastShot { get; set; }
        /// <summary>
        /// Temps requis minimum entre chaque tir, issu de l'interface IShootable.
        /// </summary>
        public float TimeBetweenEveryShot { get; set; }
        /// <summary>
        /// Propriété définissant la direction du projectile. Issu de l'interface IShootable.
        /// </summary>
        public Vector2 Direction { get; set; }
        

        //Munitions
        private int Ammo;
        private Vector2 AmmoCapacityLocation = new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 115);
        private bool isReloading = false;
        private float TimeSinceReloadingStarted;
        private float TimeForReloading;
        private Color AmmoStringColor = Color.White;

        //Life
        /// <summary>
        /// Booléen définissant si le joueur est encore en vie.
        /// </summary>
        public bool isAlive = true;
        private Texture2D HealthPointTexture;
        private readonly Vector2 HEALTH_SPRITE_DEFAULT_POS;

        //Protection
        private MouseState _previousMouseState;     //Etat précédent de la souris.
        private float TimeSinceLastProtectionPlaced;
        private float TimeBetweenEveryProtectionPlacement;
        private bool isInSinglePlacementMode = false;   //Au lancement le joueur peut poser deux protections, puis après qu'une par une avec un délai de 20 secondes.
        private int structurePlaced = 0;                //Nombre de structure placée, permet d'activer le mode singleplacement quand il en a posé autant que MAX_STRUCTURE
        private const int MAX_STRUCTURE = 2;            //Limite de structure avant que le single mod s'active.
        private Rectangle placementProtectionLimit;     //Concerne une zone ou le joueur peut poser des protections, étant donné qu'il ne peut pas les placer partout (ex: derrière le tank = impossible)

        /// <summary>
        /// Constructeur de la classe Player
        /// </summary>
        /// <param name="position">Position du joueur.</param>
        public Player(Vector2 position) : base(position)
        {
            Texture = EntityConfig.Player.Texture;
            SpriteFont = GameRoot.spriteFont;
            SpriteBatch = GameRoot.spriteBatch;
            HealthPoint = EntityConfig.Player.HealthPoint;
            HealthSpritePosition = EntityConfig.Player.HealthPointSpritePosition;
            Scale = EntityConfig.Player.Scale;
            HitBox = EntityConfig.Player.HitBox;
            Speed = EntityConfig.Player.Speed;
            TimeBetweenEveryShot = EntityConfig.Bullet.CoolDownShoot;
            Ammo = EntityConfig.Player.AmmoCapacity;
            TimeForReloading = EntityConfig.Player.TimeForReloading;
            HealthPointTexture = EntityConfig.Player.HealthPointTexture;
            HEALTH_SPRITE_DEFAULT_POS = EntityConfig.Player.HealthPointSpritePosition;
            TimeBetweenEveryProtectionPlacement = EntityConfig.Protection.CoolDownProtectionPose;
        }

        /// <summary>
        /// Méthode exécutant le code lors de la création du Joueur.
        /// </summary>
        public override void Initialize()
        {
            //La munition du joueur va tout le temps vers le haut.
            Direction = new Vector2(0, -1);
            Position = new Vector2(Config.WINDOW_WIDTH / 2 - (Texture.Width / 2 * Scale), Config.WINDOW_HEIGHT - 175);    //Positionner le joueur en bas au centre de l'écran.
            HitBox = new Rectangle((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale), (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));  //Créer la HitBox du joueur.
            placementProtectionLimit = new Rectangle(
                0,  //Le rectangle commence sur le côté gauche donc 0.
                (int)(EntityConfig.Tank.LIMITE_POSITION_Y + EntityConfig.Tank.Texture.Height / 2 * EntityConfig.Tank.Scale), //Le rectangle commence au bout du canon du tank. Donc (Pos Y du tank) + la moitiée de la hauteur de sa texture (car le point est au centre) multipliée par sa scale, car sa taille peut changer.
                Config.WINDOW_WIDTH, //Elle prend tout l'écran.
                (int)(Config.WINDOW_HEIGHT - (EntityConfig.Tank.LIMITE_POSITION_Y + EntityConfig.Tank.Texture.Height / 2 * EntityConfig.Tank.Scale) - (Config.WINDOW_HEIGHT - Position.Y) - (Texture.Height / 2 * Scale))); //Le rectangle se finit devant le bout du fusil du joueur, donc la hauteur de l'écran (1920) - la zone supprimée en haut (car sinon le rectangle est décalé étant donné qu'on l'a abaissé.) - la hauteur du joueur en calculant a nouveau sa 1/2 texture en comptant la scale.
            
        }

        /// <summary>
        /// Méthode s'effectuant a chaque tic, permet de bouger le joueur, le faire tirer, et poser des protections
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            MovePlayer();
            Shoot(gameTime);
            Protection(gameTime);
        }
        /// <summary>
        /// Méthode dessinant le joueur et différents objets.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            //Dessiner le joueur
            SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
            //Dessiner le nombre de balles restantes dans le chargeur
            SpriteBatch.DrawString(SpriteFont, Ammo.ToString(), AmmoCapacityLocation, AmmoStringColor);
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

            //Dessiner le temps restant avant le placement de la prochaine protection
            double TimeLeft = Math.Round(TimeBetweenEveryProtectionPlacement - TimeSinceLastProtectionPlaced);
            if(TimeLeft > 0)
                SpriteBatch.DrawString(SpriteFont, TimeLeft.ToString(), new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 155), Color.White);
            else
                SpriteBatch.DrawString(SpriteFont, 0.ToString(), new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 155), Color.White);
            
        }
        /// <summary>
        /// Méthode vérifiant si le joueur appuie sur une touche pour avancer, et le faire avancer si oui.
        /// </summary>
        public void MovePlayer()
        {
            Velocity = GlobalHelpers.Input.GetMovementDirection() * Speed;
            Position += Velocity.ToPoint().ToVector2();
            Position = Vector2.Clamp(Position, new Vector2(MarginSide.X, Position.Y), new Vector2(MarginSide.Y, Position.Y));
            HitBox.Location = new Point((int)(Position.X - Texture.Width / 2 * Scale), (int)(Position.Y - Texture.Height / 2 * Scale));
        }
        /// <summary>
        /// Méthode permettant au joueur de tirer, recharger etc...
        /// </summary>
        /// <param name="gameTime"></param>
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
                EntityManager.Add(new Bullet(CanonPosition, Direction, EntityConfig.Bullet.Texture, EntityConfig.Bullet.Scale));

                //Retirer une munition du chargeur 
                Ammo--;
            }
        }
        /// <summary>
        /// Méthode permettant de poser des protections.
        /// </summary>
        /// <param name="gameTime"></param>
        private void Protection(GameTime gameTime)
        {
            //Calculer la hitbox de la protection pour vérifier qu'elle soit dans la zone.
            Rectangle protectionHitBox = new Rectangle((int)(GlobalHelpers.Input.GetMousePosition().X - EntityConfig.Protection.Texture.Width / 2 * EntityConfig.Protection.Scale), (int)(GlobalHelpers.Input.GetMousePosition().Y - EntityConfig.Protection.Texture.Height / 2 * EntityConfig.Protection.Scale), (int)(EntityConfig.Protection.Texture.Width * EntityConfig.Protection.Scale), (int)(EntityConfig.Protection.Texture.Height * EntityConfig.Protection.Scale));

            if (GlobalHelpers.Input.isPlacingProtection && _previousMouseState.RightButton == ButtonState.Released && placementProtectionLimit.Intersects(protectionHitBox))
            {
                if (structurePlaced < MAX_STRUCTURE)
                {
                    if(PlaceProtection(protectionHitBox))
                        structurePlaced++;
                }
                if (structurePlaced == MAX_STRUCTURE)
                {
                    //Activer le mode de placement unique
                    isInSinglePlacementMode = true;
                }

                //Si il est en structure unique, on verifie son temps d'attente
                if(isInSinglePlacementMode && TimeSinceLastProtectionPlaced >= TimeBetweenEveryProtectionPlacement)
                {
                    //Alors poser une structure
                    if (PlaceProtection(protectionHitBox))
                    {
                        //Reset le timer 
                        TimeSinceLastProtectionPlaced = 0;
                    }
                }

            }
            //S'il est en placement unique, alors incrémenter son temps d'attente
            if (isInSinglePlacementMode)
            {
                //Incrémenter le timer de cooldown
                TimeSinceLastProtectionPlaced += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            //Enregistrer l'état précédent de la souris. Permet de s'assure que le joueur à bien relacher le clic droit avant d'en reposer un.
            _previousMouseState = GlobalHelpers.Input.GetMouseState();
        }
        private bool PlaceProtection(Rectangle hitbox)
        {
            //Par défaut, on part du principe qu'il peut poser.
            bool canPlace = true;

            // Vérifier s'il y a des protections déjà posées et si la nouvelle protection intersecte avec une existante.
            foreach (Protection p in EntityManager.Protections)
            {
                if (hitbox.Intersects(p.HitBox))
                {
                    canPlace = false;
                    return false;
                }
            }
            

            // Si aucune intersection n'a été trouvée, on ajoute la nouvelle protection.
            if (canPlace)
            {
                Protection protection = new Protection(GlobalHelpers.Input.GetMousePosition(), hitbox);
                EntityManager.Add(protection);
                return true;
            }

            //return false par défaut
            return false;
        }
    }
}
