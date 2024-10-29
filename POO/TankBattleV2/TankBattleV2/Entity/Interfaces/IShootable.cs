using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using Microsoft.Xna.Framework.Input;
namespace TankBattleV2
{
   /// <summary>
   /// Interface définissant les propriétés qu'une classe doit avoir lorsque celle-ci a la capacité de tirer.
   /// </summary>
    public interface IShootable
    {
        /// <summary>
        /// Propriété définissant le temps écoulé entre le dernier tir
        /// </summary>
        float TimeSinceLastShot { get; set; }
        /// <summary>
        /// Propriété définissant le temps requis entre chaque tir.
        /// </summary>
        float TimeBetweenEveryShot { get; set; }
        /// <summary>
        /// Propriété définissant la direction du missile.
        /// </summary>
        Vector2 Direction { get; set; }
        
    }
}
