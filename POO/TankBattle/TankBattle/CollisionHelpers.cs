
using Microsoft.Xna.Framework;

namespace TankBattle
{
    public static class CollisionHelpers
    {
        public static bool IsCollidingWith(Vector2 bullet, Ennemy entity)
        {
            return (bullet.Y < entity.Position.Y + (entity.texture.Height * 1f) / 2 &&
                   bullet.Y > entity.Position.Y - (entity.texture.Height * 1f) / 2 &&
                   bullet.X > entity.Position.X - (entity.texture.Width * 1f) / 2 &&
                   bullet.X < entity.Position.X + (entity.texture.Width * 1f) / 2);
        }

        public static bool IsCollidingWith(Vector2 bullet, Player entity)
        {
            return (bullet.Y < entity.Position.Y + (Player.texture.Height * 1f) / 4 &&
                   bullet.Y > entity.Position.Y - (Player.texture.Height * 1f) / 4 &&
                   bullet.X > entity.Position.X - (Player.texture.Width * 1f) / 4 &&
                   bullet.X < entity.Position.X + (Player.texture.Width * 1f) / 4);
        }
        public static bool IsCollidingWith(Vector2 bullet, Protection entity)
        {
            return (bullet.Y < entity.Position.Y + (Player.texture.Height * 1f) / 4 &&
                   bullet.Y > entity.Position.Y - (Player.texture.Height * 1f) / 4 &&
                   bullet.X > entity.Position.X - (Player.texture.Width * 1f) / 4 &&
                   bullet.X < entity.Position.X + (Player.texture.Width * 1f) / 4);
        }
    }
}
