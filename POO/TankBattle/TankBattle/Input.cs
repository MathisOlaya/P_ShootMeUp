using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    static class Input
    {
        public static Vector2 GetMovementDirection()
        {
            //Variable contenant la direction du joueur
            Vector2 direction = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                direction.X -= 1;
            if(Keyboard.GetState().IsKeyDown(Keys.D))
                direction.X += 1;

            //retourner la direction
            return direction;
        }
    }
}
