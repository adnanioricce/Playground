using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake;
using System.Collections.Generic;
using System.Diagnostics;

namespace SnakeGame
{
    public delegate Vector2 SnakeGrow(Vector2 nextPosition,int index);
    public class Snake
    {
        public Vector2 _position;
        public Vector2 _bodyPosition;
        public Texture2D _texture;                        
        public Keys _direction;
        private bool isOverlaped = false;
        private readonly Queue<Vector2> snakePositions = new Queue<Vector2>();
        private readonly List<SnakeBody> body = new List<SnakeBody>();
        public Snake(Vector2 position,GraphicsDevice graphics)
        {
            _position = position;
            _bodyPosition = _position;
            _texture = CreateSnakeTexture(graphics);                        
        }

        public void AddBody(Vector2 position)
        {
            SnakeGrow snakeGrow;
            //snakePositions.Enqueue(position);
            if (body.Count >= 1)
            {
                body.Add(new SnakeBody(body[body.Count - 1].LastPosition, _direction));
            }
            else
            {
                body.Add(new SnakeBody(position, _direction));
                for (int i = 0; i < body.Count; i++)
                {
                    snakeGrow = (IsHorizontalMove(_direction) ? (SnakeGrow)MoveSnakeX : (SnakeGrow)MoveSnakeY);                    
                }
            }            
        }

        public void Update()
        {            
            Debug.WriteLine($"Key:{_direction.ToString()},position = x:{_position.X},Y:{_position.Y},Texture = Width:{_texture.Width},Height:{_texture.Height},data is null:{_texture is null}");            
            if (body.Count > 0)
            {
                if (Vector2.Distance(_position,_bodyPosition) > 5f)
                {                    
                    _bodyPosition = _position;
                    body[0].LastPosition = body[0].CurrentPosition;
                    body[0].CurrentPosition = _bodyPosition;
                }                
                for (int i = 1; i < body.Count; ++i)
                {
                    body[i].LastPosition = body[i].CurrentPosition;
                    body[i].CurrentPosition = body[i - 1].LastPosition;
                    isOverlaped = body[i].IsOverlapWithHead(_position);
                }
            }
            if (_position.X > Utils.Width) {
                _position.X = 0;
            }
            if(_position.X < 0)
            {
                _position.X = Utils.Width;
            }
            if(_position.Y > Utils.Height)
            {
                _position.Y = 0;
            }
            if(_position.Y < 0)
            {
                _position.Y = Utils.Height;
            }
            Move(_direction);                       
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isOverlaped)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(_texture, _position, Color.White);
                for (int i = 0; i < body.Count; ++i)
                {
                    //var position = snakePositions.Dequeue();
                    spriteBatch.Draw(_texture, body[i].CurrentPosition, Color.White);
                }
                spriteBatch.End();
            }
            else
            {
                Debug.WriteLine("Game over");
            }
        }
        private void Move(Keys direction)
        {                                
            if(direction == Keys.Left){
                _position.X -= 8;                                
            }
            if(direction == Keys.Right){
                _position.X += 8;
                
            }
            if(direction == Keys.Up){
                _position.Y -= 8;
                
            }
            if(direction == Keys.Down){
                _position.Y += 8;
                
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
        private Vector2 MoveSnakeY(Vector2 nextPosition,int index)
        {
            return new Vector2(_position.X, _position.Y - (30 * index));
        }       
        private Vector2 MoveSnakeX(Vector2 nextPosition,int index)
        {
            return new Vector2(_position.X - (30 * index),_position.Y);
        }       
        private bool IsHorizontalMove(Keys key)
        {
            return Keys.Right == key || Keys.Left == key;
        }
    }
}