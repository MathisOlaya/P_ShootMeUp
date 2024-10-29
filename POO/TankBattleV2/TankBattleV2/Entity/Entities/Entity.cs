using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankBattleV2
{
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

        public Entity(Vector2 position)
        {
            Position = position;
        }
        public abstract void Draw(GameTime gameTime);

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);
    }
}
