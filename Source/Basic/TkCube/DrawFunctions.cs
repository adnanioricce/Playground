using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TkCube
{
    public delegate void DrawFunction(int vertexObjectId);
    public static class DrawFunctions
    {                
        public static void DrawCube(int vertexObjectId)
        {
            GL.BindVertexArray(vertexObjectId);
            GL.DrawArrays(PrimitiveType.Quads, 0, 6);
        }
    }
}
