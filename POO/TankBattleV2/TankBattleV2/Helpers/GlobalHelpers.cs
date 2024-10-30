using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GlobalHelpers
{
    /// <summary>
    /// Classe permettant à l'utilisateur d'obtenir certaines infromations sur les entrées du clavier et de la souris..
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Méthode donnant la direction X du joueur. Soit gauche, soit droite.
        /// </summary>
        /// <returns>Retourn un vecteur, modifié uniquement sur l'axe X</returns>
        public static Vector2 GetMovementDirection()
        {
            //Variable contenant la direction du joueur
            Vector2 direction = Vector2.Zero;
            
            if(Keyboard.GetState().IsKeyDown(Keys.A))
                direction.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                direction.X += 1;
            
            return direction;
        }
        /// <summary>
        /// Méthode retournant la position du curseur.
        /// </summary>
        /// <returns>Un vecteur 2.</returns>
        public static Vector2 GetMousePosition() => Mouse.GetState().Position.ToVector2();

        /// <summary>
        /// Méthode donnant l'état actuelle de la souris.
        /// </summary>
        /// <returns>Un état de la souris.</returns>
        public static MouseState GetMouseState() => Mouse.GetState();

        /// <summary>
        /// Méthode retournant si oui ou non, le joueur est entrain de cliquer (gauche).
        /// </summary>
        /// <returns>True si oui, et False si non.</returns>
        public static bool isLeftClicking() => Mouse.GetState().LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Méthode retournant si oui ou non, le joueur est en train de tirer.
        /// </summary>
        /// <returns>True si oui, et False si non.</returns>
        public static bool isShooting() => Mouse.GetState().LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Propriété définissant si oui ou non le joueur est en train de poser une protection avec le clic droit.
        /// </summary>
        public static bool isPlacingProtection => Mouse.GetState().RightButton == ButtonState.Pressed;

        /// <summary>
        /// Propriété définissant si oui ou non le joueur est en train de recharger en appuyant sur 'R'.
        /// </summary>
        /// <returns></returns>
        public static bool isReloading() => Keyboard.GetState().IsKeyDown(Keys.R);
    }
    /// <summary>
    /// Classe permettant d'utiliser l'aléatoire.
    /// </summary>
    public static class Random
    {
        static System.Random rnd = new System.Random();
        /// <summary>
        /// Méthode générant une valeur aléatoire comprise entre deux valeurs.
        /// </summary>
        /// <param name="minValue">Valeur minimale</param>
        /// <param name="maxValue">Valeur maximale</param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue)
        {
            return rnd.Next(minValue, maxValue);
        }
    }

}
