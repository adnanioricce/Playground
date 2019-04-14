using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StillTryingWalking
{
    public class Pixel
    {
        public Vector2 position;
        public Texture2D texture;
        public Pixel(GraphicsDevice graphicsDevice,Vector2 _position)
        {
            texture = new Texture2D(graphicsDevice, 1, 1);
            position = _position;
            texture.SetData(new[] { Color.Black });
        }
    }
}
