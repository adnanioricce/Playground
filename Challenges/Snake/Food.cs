using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Snake
{
    public class Food
    {
        private Texture2D _texture;
        private Vector2 _position;
        public Food()
        {
            _texture = TextureHelper.Create2DRectangleTexture(32,32);
            _position = new Vector2(0,0);
            SetPosition();
        }
        public void Update(SnakeBody snake)
        {
            if (IsOverlaped(snake._position))
            {
                SetPosition();
            }
        }        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, _position,Color.White);
            spriteBatch.End();
        }
        private void SetPosition()
        {
            _position.X = RandomSeeder.Seed(800);
            _position.Y = RandomSeeder.Seed(800);
        }
        private bool IsOverlaped(Vector2 reference)
        {
            var distance = Distance(_position, reference);
            Debug.WriteLine($"Distance:{distance}");
            if(distance <= 32)
            {
                return true;
            }            
            return false;
        }
        private double Distance(Vector2 p1,Vector2 p2)
        {
            //Vectors have a method called Distance(Vector2.Distance) that should do the job
            //But... 
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
