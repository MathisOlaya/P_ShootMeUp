using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    /// <summary>
    /// Interface définissant les méthodes qu'une entité doit avoir.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Méthode qui s'effectue lors de la création d'une entité
        /// </summary>
        void Initialize();
        /// <summary>
        /// Méthode s'effectuant à chaque tics, permettant de calculer, et/ou de faire des actions, etc...
        /// </summary>
        /// <param name="gameTime">Délai entre chaque tics.</param>
        void Update(GameTime gameTime);
        /// <summary>
        /// Méthode permettant de dessiner des élements.
        /// </summary>
        /// <param name="gameTime">Délai entre chaque tics.</param>
        void Draw(GameTime gameTime);
    }
}
