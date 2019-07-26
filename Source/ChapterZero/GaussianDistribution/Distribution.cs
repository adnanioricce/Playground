using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace GaussianDistribution
{
    public class Distribution
    {        
        public Bar[] Bars { get; private set; } = new Bar[20];
        private readonly Random randomGenerator = new Random();
        public Distribution(int width,int height,GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < Bars.Length; i++)
            {
                Bars[i] = new Bar(width, height, graphicsDevice);
                Color[] colors = new Color[Bars[i].Texture.Width * Bars[i].Texture.Height];
                for (int j = 0; j < colors.Length; j++)
                    colors[j] = Color.Black;
            }
            Console.WriteLine();
        }        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int i = 0; i < Bars.Length; i++)
            {
                var rectangle = this.Bars[i].Texture.Bounds;
                rectangle.Location = new Point(Bars[i].Texture.Bounds.Width * i ,1);
                spriteBatch.DrawRectangle(rectangle, Color.Black);                
                Console.WriteLine($"on {nameof(Draw)} at {DateTime.UtcNow},bars props:{Bars[i].Texture.Format.ToString()}");
            }
            
            spriteBatch.End();
        }
        public void Update(GraphicsDevice graphicsDevice,GameTime gameTime)
        {
            for (int i = 0; i < Bars.Length; i++)
            {
                Bars[i] = new Bar(40, Bars[i].Texture.Height + GetGaussianDistribution(1.0,0.1), graphicsDevice);
                Console.WriteLine($"on {nameof(Update)} height:{Bars[i].Texture.Height},width:{Bars[i].Texture.Width} time:{DateTime.UtcNow}");
            }
        }       
        private int GetGaussianDistribution(double mean,double standardDeviation)
        {            
            double firstSample = 1.0 - randomGenerator.NextDouble();
            double secondSample = 1.0 - randomGenerator.NextDouble();
            double normalStandard = Math.Sqrt(-2.0 * Math.Log(firstSample)) * Math.Sin(2.0 * Math.PI * secondSample);
            double normal = mean + standardDeviation + normalStandard;
            Console.WriteLine($"normal distribution value :{normal}");
            return (int)normal;
        }
    }
}
