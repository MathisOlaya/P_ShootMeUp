using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class PlayerBullet : DrawableGameComponent
    {
        private Vector2 DefaultBulletPosition;

        public PlayerBullet(Game game, Vector2 playerPosition) : base(game) 
        {
            DefaultBulletPosition = playerPosition;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            //Charger le sprite de la munition

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
