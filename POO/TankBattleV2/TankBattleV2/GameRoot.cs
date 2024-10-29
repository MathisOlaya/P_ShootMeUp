﻿using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TankBattleV2;

namespace TankBattleV2
{
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        DeadScreen,
    }
    public class GameRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        public static SpriteBatch spriteBatch;
        public static SpriteFont spriteFont;

        //Game
        public static int Score { get; set; }
        public static GameState CurrentGameState = GameState.Menu;  //Par défaut il est dans le menu.

        public static Level lvl;
        public static Menu menu;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Changer la taille de la fenêtre.
            _graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT;
            //Appliquer les changements de taille.
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Charger la font. 
            spriteFont = Content.Load<SpriteFont>("Fonts/Font");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger toutes les textures dès le lancement
            Visuals.LoadTextures(Content);

            //Créer le menu du lancement du jeu.
            menu = new Menu(new List<Action> { Action.Start, Action.Settings, Action.Exit }, spriteFont);
        }

        protected override void Update(GameTime gameTime)
        {
            //Si le joueur appuie sur ESC, mettre pause
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                CurrentGameState = GameState.Paused;
            //Si le joueur est mort et qu'il existe evidemment (au debut il n'existe pas de suite)
            if(EntityManager.Player != null)
                if (EntityManager.Player.HealthPoint <= 0)
                    CurrentGameState = GameState.DeadScreen;

            switch (CurrentGameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    break;
                case GameState.Playing:
                    menu = null;
                    lvl.Update(gameTime);
                    break;
                case GameState.Paused:
                    //Créer un nouveau menu seulement s'il n'existe pas. Sinon cela va en créer une infinité.
                    if(menu == null)
                        menu = new Menu(new List<Action>{Action.Resume, Action.Exit}, spriteFont);
                    menu.Update(gameTime);
                    break;
                case GameState.DeadScreen:
                    //Créer un nouveau menu seulement s'il n'existe pas. Sinon cela va en créer une infinité.
                    if (menu == null)
                        menu = new Menu(new List<Action> { Action.Restart, Action.Exit }, spriteFont);
                    menu.Update(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();
            spriteBatch.Draw(GameSettings.Map, Vector2.Zero, null, Color.White , 0f, Vector2.Zero, ((float)Config.WINDOW_WIDTH / (float)GameSettings.Map.Width), SpriteEffects.None, 0f);
            //Effectuer la méthode Draw pour chaque méthode
            EntityManager.Draw(gameTime);
            if(menu != null)
                menu.Draw(spriteBatch);

            
            //Seulement afficher quand il joue.
            if (CurrentGameState == GameState.Playing)
                spriteBatch.DrawString(spriteFont, Score.ToString(), new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 75), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
