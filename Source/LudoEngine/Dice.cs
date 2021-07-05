using System;
using System.Collections.Generic;
using System.Text;

namespace LudoEngine
{
    public class Dice
    {
        public static int Roll() 
        {
            var rnd = new Random();
            return rnd.Next(1,7);
        }
    }
}
