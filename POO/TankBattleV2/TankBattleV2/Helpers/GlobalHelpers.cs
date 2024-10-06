using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GlobalHelpers
{
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
        public static bool isShooting() => Mouse.GetState().LeftButton == ButtonState.Pressed;
        public static bool isReloading() => Keyboard.GetState().IsKeyDown(Keys.R);
    }
    public static class Random
    {
        static System.Random rnd = new System.Random();
        public static int Next(int minValue, int maxValue)
        {
            return rnd.Next(minValue, maxValue);
        }
    }

}
