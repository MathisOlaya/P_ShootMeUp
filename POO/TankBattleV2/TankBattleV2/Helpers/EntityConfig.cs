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
        /// <summary>
        /// Propriété contenant la texture 2D du joueur.
        /// </summary>
        public static Texture2D Texture;

        /// <summary>
        /// Propriété contenant la texture représentant la vie restante du joueur.
        /// </summary>
        public static Texture2D HealthPointTexture;

        //Position de départ, mais également la position continue du joueur.
        /// <summary>
        /// Défini la position du joueur.
        /// </summary>
        public static Vector2 Position;

        /// <summary>
        /// Propriété définissant le nombre de point de vie que l'entité possède.
        /// </summary>
        public static int HealthPoint = 3;

        /// <summary>
        /// Propriété définissant la position de la texture affichant les points de vie du joueur.
        /// </summary>
        public static Vector2 HealthPointSpritePosition = new Vector2(50, Config.WINDOW_HEIGHT - 65);

        /// <summary>
        /// Propriété définissant l'échelle d'affichage du joueur.
        /// </summary>
        public static float Scale = 0.75f;

        /// <summary>
        /// Propriété définissant la zone de collision du joueur.
        /// </summary>
        public static Rectangle HitBox;

        /// <summary>
        /// Propriété définissant la vitesse de déplacement du joueur.
        /// </summary>
        public static float Speed = 12f;

        /// <summary>
        /// Propriété définissant le nombre de munition que le joueur possède dans son arme.
        /// </summary>
        public static int AmmoCapacity = 15;

        /// <summary>
        /// Propriété définissant le temps requis pour recharger son arme.
        /// </summary>
        public static float TimeForReloading = 1.5f;
    }
    /// <summary>
    /// Classe définissant les différents attributs de la munition du joueur.
    /// </summary>
    public static class Bullet
    {
        /// <summary>
        /// Propriété définissant la texture de la munition
        /// </summary>
        public static Texture2D Texture;

        /// <summary>
        /// Propriété définissant l'icone de la munition.
        /// </summary>
        public static Texture2D IconTexture;

        /// <summary>
        /// Propriété définissant le nombre de point de vie de la munition.
        /// </summary>
        public static int HealthPoint = 1;

        /// <summary>
        /// Propriété définissant l'échelle d'affichage de la munition.
        /// </summary>
        public static float Scale = Player.Scale / 5;

        /// <summary>
        /// Propriété définissant la zone de collision pour la munition.
        /// </summary>
        public static Rectangle HitBox;

        /// <summary>
        /// Propriété définissant la vitesse de déplacement de la munition.
        /// </summary>
        public static float Speed = 15f;

        /// <summary>
        /// Propriété définissant le temps requis entre chaque tour.
        /// </summary>
        public static float CoolDownShoot = 0.2f;
    }
    /// <summary>
    /// Classe définissant les différents attributs du tank.
    /// </summary>
    public static class Tank
    {
        /// <summary>
        /// Propriété définissant la texture du Tank.
        /// </summary>
        public static Texture2D Texture;

        /// <summary>
        /// Tableau contenant les différentes textures des points de vie du tank.
        /// </summary>
        public static Texture2D[] LifeBarTextures = new Texture2D[3];

        /// <summary>
        /// Propriété définissant la position X et Y du tank.
        /// </summary>
        public static Vector2 Position;

        /// <summary>
        /// Propriété définissant la limite jusque ou le tank peut avancer sur l'axe Y.
        /// </summary>
        public static readonly int LIMITE_POSITION_Y = 175;

        /// <summary>
        /// Propriété définissant le nombre de point de vie du tank.
        /// </summary>
        public static int HealthPoint = 2;

        /// <summary>
        /// Propriété définissant la position de la texture affichant les points de vie du tank.
        /// </summary>
        public static Vector2 HealthPointSpritePosition = new Vector2(Config.WINDOW_WIDTH - 50, Config.WINDOW_HEIGHT - 50);

        /// <summary>
        /// Propriété définissant l'échelle d'affichage de la bar de point de vie du tank.
        /// </summary>
        public static float LifeBarScale = Player.Scale * 3;

        /// <summary>
        /// Propriété définissant l'échelle d'affichage du tank.
        /// </summary>
        public static float Scale = Player.Scale / 11;

        /// <summary>
        /// Propriété définissant la zone de collision du tank.
        /// </summary>
        public static Rectangle HitBox;

        /// <summary>
        /// Propriété définissant la vitesse de déplacement du tank.
        /// </summary>
        public static float Speed = 15;

        /// <summary>
        /// Dictionnaire contenant une position d'apparition, ainsi qu'un booléen disant si oui ou non la position est occupée.
        /// </summary>
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
        /// <summary>
        /// Propriété définissant la texture de la munition du tank.
        /// </summary>
        public static Texture2D Texture;

        /// <summary>
        /// Propriété définissant le nombre de point de vie de la munition du tank.
        /// </summary>
        public static int HealthPoint = 1;

        /// <summary>
        /// Propriété définissant l'échelle d'affichage de la munition du tank.
        /// </summary>
        public static float Scale = Player.Scale / 4;

        /// <summary>
        /// Propriété définissant la zone de collision de la munition du tank.
        /// </summary>
        public static Rectangle HitBox;

        /// <summary>
        /// Propriété définissant la vitesse de déplacement de la munition.
        /// </summary>
        public static float Speed = 8f;

        /// <summary>
        /// Propriété définissant le temps d'attente entre chaque tir du tank.
        /// </summary>
        public static float CoolDownShoot = 2.2f;
    }
    /// <summary>
    /// Classe définissant les différents attributs d'une protection..
    /// </summary>
    public static class Protection
    {
        /// <summary>
        /// Propriété définissant la texture d'affichage de la protection.
        /// </summary>
        public static Texture2D Texture;

        /// <summary>
        /// Propriété définissant le nombre de point de vie de la protection.
        /// </summary>
        public static int HealthPoint = 5;

        /// <summary>
        /// Propriété définissant l'échelle d'affichage de la protection.
        /// </summary>
        public static float Scale = Player.Scale * 5;

        /// <summary>
        /// Propriété définissant la zone de collision de la protection.
        /// </summary>
        public static Rectangle HitBox;

        /// <summary>
        /// Propriété définissant le temps d'attente entre le posement de chaque protection.
        /// </summary>
        public static float CoolDownProtectionPose = 20f;
    }
}
