using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using Microsoft.Xna.Framework.Input;
namespace TankBattleV2
{
    public interface IShootable
    {
        float TimeSinceLastShot { get; set; }
        float TimeBetweenEveryShot { get; set; }
        Vector2 Direction { get; set; }
        
    }
}
