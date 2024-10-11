using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TankBattleV2
{
    public class Buttons
    {
        private int Height, Width;
        private int Padding;
        private int X, Y;
        List<Action> ButtonActionTitle;
        SpriteFont SpriteFont;

        public Rectangle button { get; private set; }

        public Buttons(int height, int padding, List<Action> buttonActionTitle, SpriteFont spriteFont)
        {
            Height = height;
            Padding = padding;
            SpriteFont = spriteFont;
            ButtonActionTitle = buttonActionTitle;

            Initialize();
        }
        private void Initialize()
        {
            int menuHeight = ButtonActionTitle.Count * Height + (ButtonActionTitle.Count - 1) * Padding; //calculer la hauteur de tout les boutons.

            //Position Y le plus haut.
            int defaultPosY = (Config.WINDOW_HEIGHT - menuHeight) / 2;

            for (int i = 0; i < ButtonActionTitle.Count; i++)
            {
                // Calculer la taille du texte pour ajuster la largeur du bouton.
                Vector2 TextSize = SpriteFont.MeasureString(ButtonActionTitle[i].ToString());

                // Largeur ajustée au texte, avec un minimum de 200 pixels.
                Width = (TextSize.X > 200 ? (int)(TextSize.X + 100) : 200);
                X = Config.WINDOW_WIDTH / 2 - Width / 2; // Centrer horizontalement
                Y = defaultPosY + i * (Height + Padding);     // Positionner chaque bouton verticalement

                button = new Rectangle(X, Y, Width, Height);

                //Ajouter au dictionnaire. Sachant que par défaut, il est faux.
                Menu.Buttons.Add(button, false);
            }


        }
    }
}
