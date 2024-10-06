using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Diagnostics.Tracing;

namespace TankBattleV2
{
    public interface IMovable
    {
        public Vector2 Velocity { get;  set; }
        public float Speed { get;  set; }
    }
}
