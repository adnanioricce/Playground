using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public static class TextureExtensions
    {
        public static Texture2D CreateTexture(this GraphicsDevice graphicsDevice,Color color, int width = 16, int height = 16)
        {

            return SetTextureData(new Texture2D(graphicsDevice, width, height),color, width, height);
        }
        public static Texture2D SetTextureData(Texture2D _whiteTexture,Color color, int width, int height)
        {
            Color[] colors = CreateColor(color, width, height);
            _whiteTexture.SetData(colors);
            return _whiteTexture;
        }
        public static Color[] CreateColor(Color color, int width, int height)
        {            
            return Enumerable.Repeat(color, width * height).ToArray();
        }
    }
}
