using OpenTK;
using OpenTK.Graphics;

namespace TkCube
{
    public struct Vertex
    {
        public const int Size = (3 + 4 + 2) * 4;
        public Vector3 Position;
        public Color4 Color;
        public Vector2 Texture;
        public Vertex(Vector3 position, Color4 color, Vector2 texture)
        {
            Position = position;
            Color = color;
            Texture = texture;            
        }
    }
}
