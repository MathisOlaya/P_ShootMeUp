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
        protected SpriteFont SprintFont;
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

        public Entity(Texture2D texture, SpriteFont spriteFont, SpriteBatch spriteBatch, Vector2 position, int healthPoint, Vector2 healthSpritePosition, float scale, Rectangle hitBox)
        {
            Texture = texture;
            SprintFont = spriteFont;
            SpriteBatch = spriteBatch;
            Position = position;
            HealthPoint = healthPoint;
            HealthSpritePosition = healthSpritePosition;
            Scale = scale;
            HitBox = hitBox;
        }
        public abstract void Draw(GameTime gameTime);

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);
    }
}
