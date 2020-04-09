using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake;
using System;
using System.Diagnostics;

namespace SnakeGame
{
    public class Food
    {
        private Texture2D _texture;
        private Vector2 _position;
        public Food()
        {
            _texture = TextureHelper.Create2DRectangleTexture(32,32,Color.Pink);
            _position = new Vector2(0,0);
            SetPosition();
        }
        public void Update(Snake snake)
        {
            if (IsOverlaped(snake._position))
            {
                SetPosition();
                snake.AddBody(_position);
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
            _position.X = RandomSeeder.Seed(768);
            _position.Y = RandomSeeder.Seed(768);
            if(_position.X > Utils.Width)
            {
                _position.X = 0;
            }
            if(_position.X < 0)
            {
                _position.X = Utils.Width;
            }
            if(_position.Y > Utils.Height )
            {
                _position.Y = 0;
            }
            if(_position.Y < 0)
            {
                _position.Y = Utils.Height;
            }
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
