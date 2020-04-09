using System;

namespace GameOfLife
{
	public static class GridHelper
	{
        private static Random randomGenerator = new Random(400);
        private static object lockObject = new object();
        public static int GetRandomPosition()
        {
            lock (lockObject)
            {
                return randomGenerator.Next(1, 400);
            }
        }
        public static int GetRandomState()
        {
            lock (lockObject)
            {
                return randomGenerator.Next(-1, 1) + 1;
            }
        }
		public static int Floor(int index,int scale) {

			return (index + scale) % scale;
		}
	}
}
