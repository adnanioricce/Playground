using System;
using Microsoft.Xna.Framework;
namespace PerlinNoise
{
    public class ColorMapper
    {
        public Color GetColor(Color gradientStart,Color gradientEnd,float t)
        {
            float u = 1 - t;

            Color color = new Color(
               255,
               (int)(gradientStart.R * u + gradientEnd.R * t),
               (int)(gradientStart.G * u + gradientEnd.G * t),
               (int)(gradientStart.B * u + gradientEnd.B * t));
            return color;
        }
        public Color[][] MapGradient(Color gradientStart, Color gradientEnd, float[][] perlinNoise)
        {
            int width = perlinNoise.Length;
            int height = perlinNoise[0].Length;

            Color[][] image = new Color[2][] { new Color[width], new Color[height] };

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    image[i][j] = GetColor(gradientStart, gradientEnd, perlinNoise[i][j]);
                }
            }

            return image;
        }
        public static Color[][] MapToGrey(float[][] greyValues)
        {
            int width = greyValues.Length;
            int height = greyValues[0].Length;

            Color[][] image = Utilities.GetEmptyArray<Color>(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int grey = (int)(255 * greyValues[i][j]);
                    Color color = new Color(255, grey, grey, grey);

                    image[i][j] = color;
                }
            }

            return image;
        }
    }
}
