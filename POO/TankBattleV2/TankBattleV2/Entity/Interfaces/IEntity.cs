using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    public interface IEntity
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
