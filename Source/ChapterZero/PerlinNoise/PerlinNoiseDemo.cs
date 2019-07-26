using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PerlinNoise
{
    public class PerlinNoiseDemo
    {        
        public static void CreateDemoImageBlend(string imageFolderPath,string outputFilePath)
        {
            int octaveCount = 8;
            //Color gradientStart1 = new Color(0, 255, 255);
            //Color gradientEnd1 = new Color(0, 255, 0);
            var grassPath = imageFolderPath + @"\grass.png";
            var sandPath = imageFolderPath + @"\sand.png";
            Color[][] grassImage = ImageHelper.LoadImage(grassPath);
            Color[][] sandImage = ImageHelper.LoadImage(sandPath);

            int width = grassImage[0].Length;
            int height = grassImage[1].Length;

            float[][] perlinNoise = PerlinNoise.GeneratePerlinNoise(width, height, octaveCount);
            perlinNoise = ImageHelper.AdjustLevels(perlinNoise, 0.2f, 0.8f);
            Color[][] perlinImage = ImageHelper.BlendImages(grassImage, sandImage, perlinNoise);
            string perlinImagePath = $"{outputFilePath}/DemoImageBlend.png";
            ImageHelper.SaveImage(perlinImage, perlinImagePath);
        }
    }
}
