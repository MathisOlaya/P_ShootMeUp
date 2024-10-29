using Microsoft.Xna.Framework.Graphics;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    /// <summary>
    /// Classe concernant la configuration de l'écran.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Propriété retournant la largeur de l'écran.
        /// </summary>
        public static readonly int WINDOW_WIDTH = 1920;
        /// <summary>
        /// Propriété retournant la hauteur de l'écran.
        /// </summary>
        public static readonly int WINDOW_HEIGHT = 1080;
    }
    /// <summary>
    /// Classe concernant les paramètres de la partie.
    /// </summary>
    public static class GameSettings
    {
        /// <summary>
        /// Concerne la texture de la map
        /// </summary>
        public static Texture2D Map;
        /// <summary>
        /// Concerne la difficulté du niveau, (nombre de tank).
        /// </summary>
        public static int Difficulty = 2;
        /// <summary>
        /// Défini si un tank réaparait lorsqu'un meurt.
        /// </summary>
        public static bool InfiniteMode = false;
    }
}
