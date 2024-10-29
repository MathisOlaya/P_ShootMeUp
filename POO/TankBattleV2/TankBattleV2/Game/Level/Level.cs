using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{ 
    /// <summary>
    /// Classe permettant la création de niveau. (Initialisement des entités, etc...).
    /// </summary>
    public class Level
    {
        private int LevelDifficulty;
        /// <summary>
        /// Constructeur du level.
        /// </summary>
        /// <param name="levelDifficulty"></param>
        public Level(int levelDifficulty) 
        {
            LevelDifficulty = levelDifficulty;

            LoadContent();
        }

        /// <summary>
        /// Méthode permettant de créer les entités, en fonction du niveau défini au préalabe.
        /// </summary>
        public void LoadContent()
        {
            //Initialiser les entités
            EntityManager.Initialize(LevelDifficulty);
        }

        /// <summary>
        /// Méthode s'occupant d'update chaque entité
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //Update toutes les entités
            EntityManager.Update(gameTime);
        }
    }
}
