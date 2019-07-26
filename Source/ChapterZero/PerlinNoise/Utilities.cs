namespace PerlinNoise
{
    public static class Utilities
    {
        public static T[][] GetEmptyArray<T>(int width,int height)
        {
            return new T[2][]
            {
                new T[width],
                new T[height]
            };
        }

    }
}
