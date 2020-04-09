using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake;
using System;

namespace SnakeGame
{
    public class SnakeBody
    {
        public int Index = 1;
        public Vector2 LastPosition;
        public Vector2 CurrentPosition;
        public Keys Direction;
        private SnakeBody _body;
        public SnakeBody()
        {

        }
        public SnakeBody(Vector2 position,Keys direction)
        {
            LastPosition = position;
            Direction = direction;            
        }
        public SnakeBody(Vector2 position,Keys direction,int index)
        {
            LastPosition = position;
            Direction = direction;
            Index = index;
        }
        public bool IsOverlapWithHead(Vector2 headPosition)
        {
            //var distance = Vector2.Distance(CurrentPosition, headPosition);
            return Vector2.Distance(CurrentPosition,headPosition) <= 7;
        }
    }
}
