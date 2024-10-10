using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattleV2.Interace
{
    public class Menu
    {
        //Liste contenant tout les titre des actions.
        private List<String> ButtonActionTitle = new List<string>();

        //Liste contenant tout les boutons.
        private List<Rectangle> Buttons = new List<Rectangle>();

        public Menu(List<string> buttonActionTitle)
        {
            ButtonActionTitle = buttonActionTitle;
            Initialize();
        }

        public void Initialize()
        {
            //Créer un bouton en fonction du nombre de paramètre 
            for(int i = 0; i < ButtonActionTitle.Count; i++)
            {
                int width = 250;
                int height = 100;
                int x = Config.WINDOW_WIDTH / 2 - width;
                int y = 100 * i + 200;
                Rectangle mainbutton = new Rectangle(x, y, width, height);
                Buttons.Add(mainbutton);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(Rectangle button in Buttons)
            {
                if (button.Contains(GlobalHelpers.Input.GetMousePosition()) && GlobalHelpers.Input.isLeftClicking())
                {
                    if(GameRoot.CurrentGameState == GameState.Menu)
                        GameRoot.lvl = new Level(1);

                    GameRoot.CurrentGameState = GameState.Playing;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Dessiner la HitBox
            Texture2D rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            rectangleTexture.SetData(new Color[] { Color.Red });

            foreach(Rectangle button in Buttons)
            {
                spriteBatch.Draw(rectangleTexture, button, Color.Red * 0.5f);
            }
            
            spriteBatch.End();
        }
    }
}
