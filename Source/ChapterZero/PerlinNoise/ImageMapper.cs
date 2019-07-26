using System;
using Microsoft.Xna.Framework;

namespace PerlinNoise
{
    public static class ImageHelper
    {
        public static Color[][] BlendImages(Color[][] image1, Color[][] image2, float[][] perlinNoise)
        {
            int width = image1.Length;
            int height = image1[0].Length;

            Color[][] image = new Color[2][]
            {
                new Color[width],
                new Color[height]
            };
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    image[i][j] = Color.Lerp(image1[i][j], image2[i][j], perlinNoise[i][j]);
                }
            }
            return image;
        }
        public static float[][] AdjustLevels(float[][] image, float low, float high)
        {
            int width = image.Length;
            int height = image[0].Length;

            float[][] newImage = Utilities.GetEmptyArray<float>(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float col = image[i][j];

                    if (col < low)
                    {
                        newImage[i][j] = 0;
                    }
                    else if (col > high)
                    {
                        newImage[i][j] = 1;
                    }
                    else
                    {
                        newImage[i][j] = (col - low) / (high - low);
                    }
                }
            }

            return newImage;
        }
        public static Color[][][] AnimateTransition(Color[][] image1, Color[][] image2, int frameCount)
        {
            Color[][][] animation = new Color[frameCount][][];

            float low = 0;
            float increment = 1.0f / frameCount;
            float high = increment;

            float[][] perlinNoise = AdjustLevels(
               PerlinNoise.GeneratePerlinNoise(image1[0].Length,image1[1].Length, 9),
               0.2f, 0.8f); //initial adjustment gives more frames with action.

            for (int i = 0; i < frameCount; i++)
   {
                AdjustLevels(perlinNoise, low, high);
                float[][] blendMask = AdjustLevels(perlinNoise, low, high);
                animation[i] = BlendImages(image1, image2, blendMask);
                //SaveImage(animation[i], "blend_animation" + i + ".png");
                SaveImage(ColorMapper.MapToGrey(blendMask), "blend_mask" + i + ".png");
                low = high;
                high += increment;
            }

            return animation;
        }
        
        public static void SaveImage(Microsoft.Xna.Framework.Color[][] image, string fileName)
        {
            int width = image.Length;
            int height = image[0].Length;

            var bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bitmap.SetPixel(i, j, image[i][j].ConvertToDrawingColor());
                }
            }

            bitmap.Save(fileName);
        }
        public static Microsoft.Xna.Framework.Color[][] LoadImage(string imagePath)
        {
            var bmp = new System.Drawing.Bitmap(imagePath);
            int width = bmp.Width;
            int height = bmp.Height;
            Microsoft.Xna.Framework.Color[][] image = Utilities.GetEmptyArray<Microsoft.Xna.Framework.Color>(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    System.Drawing.Color pixel = bmp.GetPixel(i, j);
                    image[i][j] = new Microsoft.Xna.Framework.Color(pixel.A, pixel.R, pixel.G, pixel.B);
                }
            }
            return image;
        }
        
    }
}
