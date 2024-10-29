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
    /// <summary>
    /// Classe définissant les différents attributs du joueur.
    /// </summary>
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
    /// <summary>
    /// Classe définissant les différents attributs de la munition du joueur.
    /// </summary>
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
    /// <summary>
    /// Classe définissant les différents attributs du tank.
    /// </summary>
    public static class Tank
    {
        public static Texture2D Texture;

        public static Texture2D[] LifeBarTextures = new Texture2D[3];

        public static Vector2 Position;

        public static readonly int LIMITE_POSITION_Y = 175;

        public static int HealthPoint = 2;

        public static Vector2 HealthPointSpritePosition = new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 50);

        public static float LifeBarScale = Player.Scale * 3;

        public static float Scale = Player.Scale / 11;

        public static Rectangle HitBox;

        public static float Speed = 15;

        public static Dictionary<Vector2, bool> spawnPoints;

        /// <summary>
        /// Méthode permettant d'initialiser et/ou de réinitialiser le contenu du dictionnaire.
        /// </summary>
        public static void SetDefaultSpawnPoints()
        {
            spawnPoints = new Dictionary<Vector2, bool>()
            {
                { new Vector2(100, -125), true },
                { new Vector2(325, -170), true },
                { new Vector2(530, -140), true },
                { new Vector2(710, -125), true },
                { new Vector2(900, -180), true },
                { new Vector2(1100, -105), true },
                { new Vector2(1320, -150), true },
                { new Vector2(1570, -155), true },
                { new Vector2(1800, -190), true }
            };
        }
    }
    /// <summary>
    /// Classe définissant les différents attributs du missile du tank.
    /// </summary>
    public static class Shell
    {
        public static Texture2D Texture;

        public static int HealthPoint = 1;

        public static float Scale = Player.Scale / 4;

        public static Rectangle HitBox;

        public static float Speed = 8f;

        public static float CoolDownShoot = 1.8f;
    }
    /// <summary>
    /// Classe définissant les différents attributs d'une protection..
    /// </summary>
    public static class Protection
    {
        public static Texture2D Texture;

        public static int HealthPoint = 5;

        public static float Scale = Player.Scale * 5;

        public static Rectangle HitBox;

        public static float CoolDownProtectionPose = 20f;
    }
}
