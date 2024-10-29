using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankBattleV2
{
    /// <summary>
    /// Classe Entité, qui défini les propriétés de base qu'une entité doit avoir.
    /// </summary>
    public abstract class Entity : IEntity
    {
        //Contient toutes les variables, ... à donner au entités enfants.
        //Visibles
        protected Texture2D Texture;
        protected SpriteFont SpriteFont;
        protected SpriteBatch SpriteBatch;

        //Localisation
        public Vector2 Position { get; set; }

        //Points de vie
        public int HealthPoint;
        protected Vector2 HealthSpritePosition;

        //Taille
        protected float Scale;

        //HitBox 
        public Rectangle HitBox;

        /// <summary>
        /// Constructeur de base d'une entité
        /// </summary>
        /// <param name="position"></param>
        public Entity(Vector2 position)
        {
            Position = position;
        }
        /// <summary>
        /// Permet de dessiner certains élements choisi.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// Méthode permettant de faire des calculs et qui s'effectue lors de la création de l'entité
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Méthode s'effectuant à chaque tic, et permettant de faire des calculs, actions, etc...
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
    }
}
