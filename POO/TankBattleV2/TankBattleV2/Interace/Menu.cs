using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    }
    public class Menu
    {
        //Liste contenant tout les titre des actions.
        private List<Action> ButtonActionTitle = new List<Action>();

        //Liste contenant tout les boutons.
        private List<Rectangle> Buttons = new List<Rectangle>();

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
            height = 100;
            int padding = 50; // Espace (vertical) entre chaque boutons.
            int menuHeight = ButtonActionTitle.Count * height + (ButtonActionTitle.Count - 1) * padding; //calculer la hauteur de tout les boutons.

            //Position Y le plus haut.
            int defaultPosY = (Config.WINDOW_HEIGHT - menuHeight) / 2;

            for (int i = 0; i < ButtonActionTitle.Count; i++)
            {
                // Calculer la taille du texte pour ajuster la largeur du bouton.
                Vector2 TextSize = spriteFont.MeasureString(ButtonActionTitle[i].ToString());

                // Largeur ajustée au texte, avec un minimum de 200 pixels.
                width = (TextSize.X > 200 ? (int)(TextSize.X + 100) : 200);
                x = Config.WINDOW_WIDTH / 2 - width / 2; // Centrer horizontalement
                y = defaultPosY + i * (height + padding);     // Positionner chaque bouton verticalement

                Rectangle mainbutton = new Rectangle(x, y, width, height);
                Buttons.Add(mainbutton);
            }
        }


        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].Contains(GlobalHelpers.Input.GetMousePosition()) && GlobalHelpers.Input.isLeftClicking())
                {
                    OnClick(ButtonActionTitle[i]);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Dessiner la HitBox
            Texture2D rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            rectangleTexture.SetData(new Color[] { Color.Black });

            for(int i = 0; i < Buttons.Count; i++)
            {
                spriteBatch.Draw(rectangleTexture, Buttons[i], Color.Red * 1f);

                //Ecrire le texte du bouton, et le centrer en calculant la longueur du texte (pas le nbre de char).
                spriteBatch.DrawString(spriteFont, ButtonActionTitle[i].ToString(), 
                    new Vector2(Buttons[i].X + (Buttons[i].Width - spriteFont.MeasureString(ButtonActionTitle[i].ToString()).X) / 2,
                    Buttons[i].Y + (Buttons[i].Height - spriteFont.MeasureString(ButtonActionTitle[i].ToString()).Y) / 2), Color.White);
            }
            
            spriteBatch.End();
        }
        private void OnClick(Action action)
        {
            switch (action)
            {
                case Action.Start:
                    if (GameRoot.CurrentGameState == GameState.Menu)
                        GameRoot.lvl = new Level(1);
                    GameRoot.CurrentGameState = GameState.Playing;
                    break;
                case Action.Resume:
                    GameRoot.CurrentGameState = GameState.Playing;
                    break;
                case Action.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    GameRoot.CurrentGameState = GameState.Menu;
                    break;
            }
        }
    }
}
