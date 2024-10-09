using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattleV2;

namespace EntityConfig
{
    public static class Player
    {
        public static Texture2D Texture;

        public static Texture2D HealthPointTexture;

        //Position de départ, mais également la position continue du joueur.
        public static Vector2 Position;

        public static int HealthPoint = 3;

        public static Vector2 HealthPointSpritePosition = new Vector2(50, Config.WINDOW_HEIGHT - 65);

        public static float Scale = 0.75f;

        public static Rectangle HitBox;

        public static float Speed = 12f;

        public static int AmmoCapacity = 15;

        public static float TimeForReloading = 1.5f;
    }
    public static class Bullet
    {
        public static Texture2D Texture;

        public static Texture2D IconTexture;

        public static int HealthPoint = 1;

        public static float Scale = Player.Scale / 5;

        public static Rectangle HitBox;

        public static float Speed = 15f;

        public static float CoolDownShoot = 0.2f;
    }
    public static class Tank
    {
        public static Texture2D Texture;

        public static Texture2D[] LifeBarTextures = new Texture2D[3];

        public static Vector2 Position;

        public static int HealthPoint = 2;

        public static Vector2 HealthPointSpritePosition = new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 50);

        public static float LifeBarScale = Player.Scale * 3;

        public static float Scale = Player.Scale / 11;

        public static Rectangle HitBox;

        public static float Speed = 15;

        public static Dictionary<Vector2, bool> spawnPoints = new Dictionary<Vector2, bool>()
        {
            {new Vector2(100, -150), true},
            {new Vector2(325, -150), true},
            {new Vector2(530, -150), true},
            {new Vector2(710, -150), true},
            {new Vector2(900, -150), true},
            {new Vector2(1100, -150), true},
            {new Vector2(1320, -150), true},
            {new Vector2(1570, -150), true},
            {new Vector2(1800, -150), true},
        };
    }
    public static class Shell
    {
        public static Texture2D Texture;

        public static int HealthPoint = 1;

        public static float Scale = Player.Scale / 4;

        public static Rectangle HitBox;

        public static float Speed = 8f;

        public static float CoolDownShoot = 1.2f;
    }
    public static class Protection
    {
        public static Texture2D Texture;

        public static int HealthPoint = 5;

        public static float Scale = Player.Scale / 4;

        public static Rectangle HitBox;

        public static float CoolDownProtectionPose = 20f;
    }
}
