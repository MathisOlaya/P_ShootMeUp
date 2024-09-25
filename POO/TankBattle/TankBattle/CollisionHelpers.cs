
using Microsoft.Xna.Framework;

namespace TankBattle
{
    public static class CollisionHelpers
    {
        public static bool IsCollidingWith(Vector2 object1, Ennemy object2)
        {
            return (object1.Y < object2.EnnemyPosition.Y + (object2.texture.Height * 1f) / 2 &&
                   object1.Y > object2.EnnemyPosition.Y - (object2.texture.Height * 1f) / 2 &&
                   object1.X > object2.EnnemyPosition.X - (object2.texture.Width * 1f) / 2 &&
                   object1.X < object2.EnnemyPosition.X + (object2.texture.Width * 1f) / 2);
        }

        public static bool IsCollidingWith(Vector2 object1, Player object2)
        {
            return (object1.Y < object2.Position.Y + (Player.texture.Height * 1f) / 2 &&
                   object1.Y > object2.Position.Y - (Player.texture.Height * 1f) / 2 &&
                   object1.X > object2.Position.X - (Player.texture.Width * 1f) / 2 &&
                   object1.X < object2.Position.X + (Player.texture.Width * 1f) / 2);
        }
    }
}
