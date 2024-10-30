using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TankBattleV2;

namespace TankBattleV2
{
    /// <summary>
    /// Défini l'état de la partie
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Le joueur se trouve dans le menu de départ.
        /// </summary>
        Menu,
        /// <summary>
        /// Le joueur se trouve dans la partie.
        /// </summary>
        Playing,
        /// <summary>
        /// Le joueur a mis pause à la partie.
        /// </summary>
        Paused,
        /// <summary>
        /// Le joueur est mort.
        /// </summary>
        DeadScreen,
    }
    /// <summary>
    /// Racine du jeu, elle contrôle l'entièrté du jeu
    /// </summary>
    public class GameRoot : Game
    {
        /// <summary>
        /// Gestion de la configuration d'affichage graphique du jeu.
        /// </summary>
        private GraphicsDeviceManager _graphics;

        /// <summary>
        /// Gestionnaire de rendu des éléments visuels à l'écran.
        /// </summary>
        public static SpriteBatch spriteBatch;

        /// <summary>
        /// Police utilisée pour afficher du texte dans le jeu.
        /// </summary>
        public static SpriteFont spriteFont;

        /// <summary>
        /// Bool qui dit si oui ou non le score a été sauvegardé.
        /// </summary>
        public static bool isScoreSave = false;

        //Game
        /// <summary>
        /// Propriété contenant le score actuelle de la partie.
        /// </summary>
        public static int Score { get; set; }
        /// <summary>
        /// Propriété contenant l'état actuelle de la partie. Par défaut il arrive dans le menu.
        /// </summary>
        public static GameState CurrentGameState = GameState.Menu;  //Par défaut il est dans le menu.

        /// <summary>
        /// Instance du lvl, permettant de créer des niveaux a partir d'une difficulté prédéfinies.
        /// </summary>
        public static Level lvl;
        /// <summary>
        /// Instance du menu, permettant de créer des interfaces.
        /// </summary>
        public static Menu menu;

        /// <summary>
        /// Constructeur de Gameroot
        /// </summary>
        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        /// <summary>
        /// Méthode s'effectuant 
        /// </summary>
        protected override void Initialize()
        {
            //Changer la taille de la fenêtre.
            _graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT;
            //Appliquer les changements de taille.
            _graphics.ApplyChanges();
            base.Initialize();
            EntityConfig.Tank.SetDefaultSpawnPoints();

            //Créer le dossier TankBattle s'il n'existe pas.
            if (!Directory.Exists(Config.SAVE_PATH_TANKBATTLE))
                Directory.CreateDirectory(Config.SAVE_PATH_TANKBATTLE);
            if(!Directory.Exists(Config.SAVE_PATH_DATA))
                Directory.CreateDirectory(Config.SAVE_PATH_DATA);

            //Créer le fichier contenant son meilleur score s'il n'existe pas. Using permet de fermer le flux après la création. Sinon cela peut generer des erreurs.
            if(!File.Exists(Config.SAVE_PATH_DATA_SCORE))
                using (File.Create(Config.SAVE_PATH_DATA_SCORE)) { }
        }
        /// <summary>
        /// Méthode étant utilisée pour charger les assets et d'autre choses
        /// </summary>
        protected override void LoadContent()
        {
            //Charger la font. 
            spriteFont = Content.Load<SpriteFont>("Fonts/Font");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Charger toutes les textures dès le lancement
            Visuals.LoadTextures(Content);

            //Créer le menu du lancement du jeu.
            menu = new Menu(new List<Action> { Action.Start, Action.Exit }, spriteFont);
        }

        /// <summary>
        /// Méthode s'effectuant à chaque tics, et est utilisée pour les calculs et actions.
        /// </summary>
        /// <param name="gameTime">Délai entre chaque utilisation de la méthode</param>
        protected override void Update(GameTime gameTime)
        {
            //Si le joueur appuie sur ESC, mettre pause
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                CurrentGameState = GameState.Paused;
            //Si le joueur est mort et qu'il existe evidemment (au debut il n'existe pas de suite)
            if(EntityManager.Player != null)
                if (EntityManager.Player.HealthPoint <= 0)
                {
                    CurrentGameState = GameState.DeadScreen;

                    //Sauvegarder son score.
                    if (!isScoreSave)
                    {
                        File.AppendAllText(Config.SAVE_PATH_DATA_SCORE, Score.ToString() + Environment.NewLine);
                        isScoreSave = true;
                    }
                }
                    

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
                        menu = new Menu(new List<Action>{Action.Resume, Action.Restart, Action.Exit}, spriteFont);
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

        /// <summary>
        /// Méthode servant à dessiner les différentes entités, éléments etc...
        /// </summary>
        /// <param name="gameTime">Délai entre chaque utilisation de la méthode</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();
            spriteBatch.Draw(GameSettings.Map, Vector2.Zero, null, Color.White , 0f, Vector2.Zero, ((float)Config.WINDOW_WIDTH / (float)GameSettings.Map.Width), SpriteEffects.None, 0f);
            //Effectuer la méthode Draw pour chaque méthode
            EntityManager.Draw(gameTime);
            if(menu != null)
                menu.Draw(spriteBatch);

            //Si il y a un nouveau lvl, l'écrire
            spriteBatch.DrawString(spriteFont, EntityManager.LevelID.ToString(), new Vector2(50, 50), Color.White);

            //Seulement afficher quand il joue ou quand  il est mort 
            if (CurrentGameState == GameState.Playing)
                spriteBatch.DrawString(spriteFont, Score.ToString(), new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 75), Color.White);
            //S'il est mort, afficher son meilleur score, et son score actuel.
            if (CurrentGameState == GameState.DeadScreen)
            {
                //PB
                spriteBatch.DrawString(spriteFont, "Score : " + Score.ToString(), new Vector2(Config.WINDOW_WIDTH / 2 - spriteFont.MeasureString("Score : " + Score.ToString()).X / 2 * 2, Config.WINDOW_HEIGHT / 2 - 325), Color.YellowGreen, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                //Actuel
                spriteBatch.DrawString(spriteFont, "HiScore : " + GetBestScore(), new Vector2(Config.WINDOW_WIDTH / 2 - spriteFont.MeasureString("HiScore : " + GetBestScore()).X / 2 * 2, Config.WINDOW_HEIGHT / 2 - 250), Color.Yellow, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// Permet de retourner le meilleur score du joueur
        /// </summary>
        /// <returns>Chaine de caractère</returns>
        private string GetBestScore()
        {
            //Liste contenant tout les scores du fichier txt.
            List<string> Scores = new List<string>(); 

            int HighestScore = 0;
            int Score = 0;  

            //Ajouter chaque ligne a la liste
            foreach(var line in File.ReadAllLines(Config.SAVE_PATH_DATA_SCORE))
            {
                Scores.Add(line);
            }
            
            //Sauvegarder l'entier le plus haut
            foreach(string  line in Scores)
            {
                try
                {
                    //Essayer de convertir la ligne en entier
                    Score = int.Parse(line);

                    //Comparer si le score est plus grand que le highScore, si oui le save
                    if(Score >= HighestScore)
                        HighestScore = Score;
                }
                catch { }
            }
            return HighestScore.ToString();
        }
    }
}
