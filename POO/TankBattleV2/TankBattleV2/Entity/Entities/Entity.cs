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
        /// <summary>
        /// Contient la texture qu'une entité possède
        /// </summary>
        protected Texture2D Texture;
        /// <summary>
        /// Font d'écriture.
        /// </summary>
        protected SpriteFont SpriteFont;
        /// <summary>
        /// Propriété contenant le SpriteBatch du projet.
        /// </summary>
        protected SpriteBatch SpriteBatch;

        //Localisation
        /// <summary>
        /// Vector2 contenant la position X et Y de l'entité
        /// </summary>
        public Vector2 Position { get; set; }

        //Points de vie
        /// <summary>
        /// Propriété contenant le nombre de point de vie de l'entité
        /// </summary>
        public int HealthPoint;
        /// <summary>
        /// Correspond à une location de la bar de vie de l'entité
        /// </summary>
        protected Vector2 HealthSpritePosition;

        //Taille
        /// <summary>
        /// Propriété correspondant à l'échelle utilisée pour la taille de l'entité.
        /// </summary>
        protected float Scale;

        //HitBox 
        /// <summary>
        /// Rectangle définissant la HitBox de l'entité.
        /// </summary>
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
