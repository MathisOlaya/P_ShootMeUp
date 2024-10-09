using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{ 
    public class Level
    {
        private int LevelDifficulty;
        public Level(int levelDifficulty) 
        {
            LevelDifficulty = levelDifficulty;

            LoadContent();
        }

        public void LoadContent()
        {
            //Initialiser les entités
            EntityManager.Initialize(LevelDifficulty);
        }

        public void Update(GameTime gameTime)
        {
            //Update toutes les entités
            EntityManager.Update(gameTime);
        }
    }
}
