using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public static class GlobalHelpers
    {
        private static Random _rnd = new Random();
        //Generate random number between min and max
        public static int GenerateRandom(int min, int max) => _rnd.Next(min, max);
    }
}
