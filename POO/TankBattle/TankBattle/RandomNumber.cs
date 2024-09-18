using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public static class RandomNumber
    {
        public static int Generate(int min, int max)
        {
            Random rnd = new Random();

            //Generate random number between min and max 
            return rnd.Next(min, max);
        }
    }
}
