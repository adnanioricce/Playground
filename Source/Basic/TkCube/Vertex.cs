using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TkCube
{
    public struct Vertex
    {
        public const int Size = (3 + 4) * 4;
        public Vector3 Position;
        public Color4 Color;
        public Vertex(Vector3 position,Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
