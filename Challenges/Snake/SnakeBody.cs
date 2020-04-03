using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
//using Color = Microsoft.Xna.Framework.Color;
namespace Snake
{
    public class SnakeBody
    {
        public Vector2 _position;
        public Texture2D _texture;                
        private GraphicsDevice _graphics;
        private Keys _direction;
        private Vector2 _borders;
        public SnakeBody(Vector2 position,Vector2 borders,GraphicsDevice graphics)
        {
            _position = position;
            _graphics = graphics;
            _borders = borders;
            _texture = CreateSnakeTexture(graphics);            
        }
        public void Update()
        {
            HandleInput();
            Debug.WriteLine($"Key:{_direction.ToString()},position = x:{_position.X},Y:{_position.Y},Texture = Width:{_texture.Width},Height:{_texture.Height},data is null:{_texture is null}");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture,_position,Color.White);
            spriteBatch.End();
        }
        private void Move(Keys direction)
        {            
            if(direction == Keys.Left){
                _position.X -= 3;                   
            }
            if(direction == Keys.Right){
                _position.X += 3;                   
            }
            if(direction == Keys.Up){
                _position.Y -= 3;                   
            }
            if(direction == Keys.Down){
                _position.Y += 3;                   
            }
            _direction = direction;
            
        }
        private Texture2D CreateSnakeTexture(GraphicsDevice graphics)
        {
            var colors = new Color[32 * 32];
            for(int i = 0;i < colors.Length;++i){
                colors[i] = Color.White;
            }
            var texture = new Texture2D(graphics,32,32);
            texture.SetData(colors);
            return texture;
        }
        private void HandleInput()
        {
            var input = Keyboard.GetState();
            if (input.IsKeyDown(Keys.Left)) {
                //_position.X += 1;
                Move(Keys.Left);                
            }
            else if (input.IsKeyDown(Keys.Right)) {
                // _position.X -= 1;
                Move(Keys.Right);
            }
            else if (input.IsKeyDown(Keys.Up)) {
                // _position.Y += 1;
                Move(Keys.Up);
            }
            else if (input.IsKeyDown(Keys.Down))
            {
                // _position.Y -= 1;
                Move(Keys.Down);
            }
            else
            {
                Move(_direction);
            }
            
            
        }

    }
}