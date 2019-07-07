using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GaussianDistribution
{
    public class Bar
    {
        public Texture2D Texture { get; private set; }                       
        public Vector2 Position { get; set; }
        public Bar (int width,int height,GraphicsDevice graphicsDevice)
        {
            Texture = new Texture2D(graphicsDevice, width, height);            
        }
        public void AddHeight(double distribution,GraphicsDevice graphicsDevice)
        {
            this.Texture = new Texture2D(graphicsDevice,this.Texture.Width,this.Texture.Height + (int)distribution);            
            Color[] colors = new Color[Texture.Width * Texture.Height];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = Color.Black;
            Texture.SetData(colors);
            Console.WriteLine($"Log on {nameof(AddHeight)},texture height:{this.Texture.Height},width:{this.Texture.Width},time:{DateTime.UtcNow}");            
        }
    }
}