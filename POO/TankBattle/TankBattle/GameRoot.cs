using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TankBattle
{
    public class GameRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Nombres de tanks
        public const int TANK_NUMBERS = 6;
        //Liste de tous les tanks
        public static List<Ennemy> Tanks = new List<Ennemy>();
        //Liste de toutes les protections
        public static List<Protection> Protections = new List<Protection>();
        //Créer une instance du joueur globale qui sera accessible partout.
        public static Player Player;
        //Correspond à l'état précedent de la souris.
        private MouseState previousMouseState;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            //Changer la taille de la fenêtre 
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Initialiser la classe Config afin d'attribuer certaines valeurs.
            Config.Initialize(_graphics);

            //Ajouter et créer le joueur
            Player = new Player(this);
            Components.Add(Player);

            //Ajouter un nouveau tank à la liste.
            for (int i = 0; i < TANK_NUMBERS; i++)
            {
                Components.Add(new Ennemy(this));
            }
            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Enregistrer l'état de la souris
            MouseState currentMouseState = Mouse.GetState();

            //Si le joueur clic (simple clic max !) et que le nombre de protections est plus petit que 2.
            if(currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released && Protections.Count < 2)
            {
                Protection protection = new Protection(this, new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y));
                Protections.Add(protection);
                this.Components.Add(protection);
            }

            // Mettre à jour l'état précédent de la souris pour la prochaine boucle de mise à jour
            previousMouseState = currentMouseState;


            // TODO: Add your update logic here

            base.Update(gameTime);

            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
