using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace TankBattleV2
{
    /// <summary>
    /// Classe permettant la création de bouton.
    /// </summary>
    public class Buttons
    {
        private int Height, Width;
        private int Padding;
        private int X, Y;
        List<Action> ButtonActionTitle;
        SpriteFont SpriteFont;

        /// <summary>
        /// Rectangle définissant la surface du bouton.
        /// </summary>
        public Rectangle Button { get; private set; }

        /// <summary>
        /// COnstructeur de la classe bouton.
        /// </summary>
        /// <param name="height">Hauteur du bouton.</param>
        /// <param name="padding">Espace</param>
        /// <param name="buttonActionTitle">Nom du bouton</param>
        /// <param name="spriteFont">Font d'écriture</param>
        public Buttons(int height, int padding, List<Action> buttonActionTitle, SpriteFont spriteFont)
        {
            Height = height;
            Padding = padding;
            SpriteFont = spriteFont;
            ButtonActionTitle = buttonActionTitle;

            Initialize();
        }
        /// <summary>
        /// Méthode s'effectuant lors de la création et s'occupant de créer les divers bouton.
        /// </summary>
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

                Button = new Rectangle(X, Y, Width, Height);

                //Ajouter au dictionnaire. Sachant que par défaut, il est faux.
                Menu.Buttons.Add(Button, false);
            }


        }
    }
}
