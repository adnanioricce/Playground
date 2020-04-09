using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    public static class Utils
    {
        public static int Width;
        public static int Height;
        public static Vector2 Move(Vector2 _position,Keys direction,int index)
        {
            if (direction == Keys.Left)
            {
               return new Vector2(_position.X + ((32 + index) * index), _position.Y);
            }
            if (direction == Keys.Right)
            {
                return new Vector2(_position.X - ((32 + index) * index), _position.Y);
            }
            if (direction == Keys.Up)
            {
                return new Vector2(_position.X, _position.Y + ((32 + index) * index));
            }
            if (direction == Keys.Down)
            {
                return new Vector2(_position.X, _position.Y - ((32 + index) * index));
            }
            return _position;
        }
    }
}
