using Microsoft.Xna.Framework.Graphics;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    public static class Config
    {
        public static readonly int WINDOW_WIDTH = 1920;
        public static readonly int WINDOW_HEIGHT = 1080;
    }
    public static class GameSettings
    {
        public static Texture2D Map;
        public static int Difficulty = 2;
        public static bool InfiniteMode = false;
    }
}
