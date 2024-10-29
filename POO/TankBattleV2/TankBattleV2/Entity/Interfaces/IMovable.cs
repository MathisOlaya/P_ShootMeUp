using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Diagnostics.Tracing;

namespace TankBattleV2
{
    /// <summary>
    /// Interface définissant les propriétés qu'une classe doit avoir lorsqu'elle à la capacité de se déplacer.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Défini la vélocité de l'entité
        /// </summary>
        public Vector2 Velocity { get;  set; }
        /// <summary>
        /// Défini la vitesse de déplacement de l'entité
        /// </summary>
        public float Speed { get;  set; }
    }
}
