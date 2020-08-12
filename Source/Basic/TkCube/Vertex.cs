﻿using OpenTK;
using OpenTK.Graphics;

namespace TkCube
{
    public struct Vertex
    {
        public const int Size = (3 + 4 + 2) * 4;
        public const int PositionStride = 3 * sizeof(float);
        public const int ColorStride = 4 * sizeof(float);
        public const int TextureCoordinateStride = 2 * sizeof(float);
        public Vector3 Position;
        public Color4 Color;
        public Vector2 TextureCoordinate;
        public Vertex(Vector3 position, Color4 color, Vector2 textureCoordinate)
        {
            Position = position;
            Color = color;
            TextureCoordinate = textureCoordinate;            
        }
    }
}
