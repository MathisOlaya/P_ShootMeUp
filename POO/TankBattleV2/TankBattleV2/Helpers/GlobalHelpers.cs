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
        public static Vector2 GetMousePosition() => Mouse.GetState().Position.ToVector2();

        public static MouseState GetMouseState() => Mouse.GetState();
        public static bool isLeftClicking() => Mouse.GetState().LeftButton == ButtonState.Pressed;
        public static bool isShooting() => Mouse.GetState().LeftButton == ButtonState.Pressed;
        public static bool isPlacingProtection => Mouse.GetState().RightButton == ButtonState.Pressed;
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
