using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public static class RandomSeeder
    {
        private static Random randomGen = new Random(8000);
        public static int Seed(int max)
        {
            return randomGen.Next(max);
        }
        public static int Seed(int min,int max)
        {
            return randomGen.Next(min, max);
        }
        public static int Seed()
        {
            return randomGen.Next();
        }
    }
}
