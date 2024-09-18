using Microsoft.Xna.Framework;

namespace TankBattle
{
    public static class Config
    {
        // Propriétés de la fenêtre
        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }

        // Méthode pour initialiser la configuration avec GraphicsDeviceManager
        public static void Initialize(GraphicsDeviceManager graphics)
        {
            WindowWidth = graphics.PreferredBackBufferWidth;
            WindowHeight = graphics.PreferredBackBufferHeight;
        }
    }
}
