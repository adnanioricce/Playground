using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RandomWalk
{
    public struct Pixel
    {        
        public Vector2 position;
        public Texture2D texture;
        private Vector2 nextPosition;
        private Vector2 lastPosition;
        public Pixel(GraphicsDevice graphicsDevice,Vector2 _position)
        {
            texture = new Texture2D(graphicsDevice, 6, 6);
            position = _position;
            lastPosition = _position;
            nextPosition = new Vector2();
            var colors = new Color[36];            
            for(int i = 0; i < 36; ++i)
            {
                colors[i] = Color.White;
            }
            texture.SetData(colors);
        }
        public void Update()
        {
            nextPosition = lastPosition + new Vector2(new Random().Next(-190, 190) / 100f, new Random().Next(-190, 190) / 100f);
            lastPosition = nextPosition;
            this.position = nextPosition;            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.texture, this.position, Color.White);
            spriteBatch.End();
        }
    }
}
