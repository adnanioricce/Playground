using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame
{
    public class TextureHelper
    {
        private static GraphicsDevice _graphicsDevice;
        public static void SetGraphicsDevice(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }
        public static Texture2D Create2DRectangleTexture(int width, int height,Color color)
        {
            var colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = color;
            }
            var texture = new Texture2D(_graphicsDevice, 32, 32);
            texture.SetData(colors);
            return texture;
        }
    }
}
