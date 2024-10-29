using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattleV2
{
    public enum Action
    {
        Resume, 
        Start,
        Exit, 
        Settings,
        Restart,
    }
    public class Menu
    {
        //Liste contenant tout les titre des actions.
        private List<Action> ButtonActionTitle = new List<Action>();

        //Liste contenant tout les boutons, avec un bool attribué regardant si le curseur se trouve dessus isHover.
        public static Dictionary<Rectangle, bool> Buttons = new Dictionary<Rectangle, bool>();

        //Coordoonées
        int x, y, width, height;

        //Ecrire
        SpriteFont spriteFont;

        public Menu(List<Action> buttonActionTitle, SpriteFont sprifeFont)
        {
            ButtonActionTitle = buttonActionTitle;
            this.spriteFont = sprifeFont;
            Initialize();
        }

        public void Initialize()
        {
            //Reset la liste de boutons
            Buttons.Clear();

            //Créer un nouveau bouton, qui ajoutera automatique au dictionnaire.
            Buttons buttons = new Buttons(100, 50, ButtonActionTitle, spriteFont);
        }
        

        public void Update(GameTime gameTime)
        {

            for (int i = 0; i < Buttons.Count; i++)
            {
                //Regarder si le curseur est sur le bouton
                if(Buttons.Keys.ElementAt(i).Contains(GlobalHelpers.Input.GetMousePosition()))
                {
                    //Si le curseur se trouve par dessus, alors changer la valeur en true pour le bouton correspondant.
                    Buttons[Buttons.Keys.ElementAt(i)] = true;

                    //Si en plus il clique, effectuer l'action.
                    if (GlobalHelpers.Input.isLeftClicking())
                    {
                        OnClick(ButtonActionTitle[i]);
                    }
                }
                else
                {
                    //Sinon elle est fausse
                    Buttons[Buttons.Keys.ElementAt(i)] = false;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Texture2D rectangleTexture;
                // Dessiner la HitBox et choisir la couleur en fonction du bouton.
                if (Buttons[Buttons.Keys.ElementAt(i)] == true)
                {
                    rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                    rectangleTexture.SetData(new Color[] { Color.Gray });
                }
                else
                {
                    rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                    rectangleTexture.SetData(new Color[] { Color.Black });
                }
                

                //Si le rectangle à la valeur true pour le hover, alors on dessine plus clair.
                if (Buttons[Buttons.Keys.ElementAt(i)] == true)
                    spriteBatch.Draw(rectangleTexture, Buttons.Keys.ElementAt(i), Color.Gray * 1f);
                else if (Buttons[Buttons.Keys.ElementAt(i)] == false)
                    spriteBatch.Draw(rectangleTexture, Buttons.Keys.ElementAt(i), Color.Black * 1f);

                //Ecrire le texte du bouton, et le centrer en calculant la longueur du texte (pas le nbre de char).
                Rectangle Button = Buttons.Keys.ElementAt(i);
                spriteBatch.DrawString(spriteFont, ButtonActionTitle[i].ToString(),
                    new Vector2(Button.X + (Button.Width - spriteFont.MeasureString(ButtonActionTitle[i].ToString()).X) / 2,
                    Button.Y + (Button.Height - spriteFont.MeasureString(ButtonActionTitle[i].ToString()).Y) / 2), Color.White);
            }
        }
        private void OnClick(Action action)
        {
            switch (action)
            {
                case Action.Start:
                    if (GameRoot.CurrentGameState == GameState.Menu)
                        GameRoot.lvl = new Level(GameSettings.Difficulty);
                    GameRoot.CurrentGameState = GameState.Playing;
                    
                    //Supprimer tout les boutons.
                    Buttons.Clear();
                    break;
                case Action.Resume:
                    GameRoot.CurrentGameState = GameState.Playing;
                    //Supprimer tout les boutons.
                    Buttons.Clear();
                    break;
                case Action.Exit:
                    Environment.Exit(0);
                    break;
                case Action.Restart:
                    // Réinitialiser les variables de jeu importantes
                    GameRoot.Score = 0;
                    EntityManager.TankKilled = 0;
                    EntityManager.Player.HealthPoint = 3;
                    GameSettings.Difficulty = 2; //difficulté par défaut
                    GameSettings.InfiniteMode = false;

                    //Supprimer toutes les entités et le lvl
                    EntityManager.DeleteEntity();
                    GameRoot.lvl = null;

                    //Afficher l'instance du joueur comme nul
                    EntityManager.Player = null;

                    // Recréer le niveau
                    GameRoot.lvl = new Level(GameSettings.Difficulty);

                    //Reset le dictionnaire des positions des tanks
                    foreach (var key in EntityConfig.Tank.spawnPoints.Keys.ToList())
                    {
                        EntityConfig.Tank.spawnPoints[key] = true;
                    }

                    //Joueur
                    GameRoot.CurrentGameState = GameState.Playing;
                    break;
                case Action.Settings:
                    break;
            }
        }
    }
}
