using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TkCube
{
    /// <summary>
    /// Delegate for a GL draw function. May be a vertex array Id or a vertex object Id
    /// </summary>
    /// <param name="vertexObjectId">The vertex object or vertex array Id</param>
    public delegate void DrawFunction(int vertexObjectId, int verticesCount);
    public static class DrawFunctions
    {                
        public static void DrawCube(int vertexArrayId,int verticesCount)
        {
            GL.BindVertexArray(vertexArrayId);
            GL.DrawArrays(PrimitiveType.Quads, 0, verticesCount);
        }
    }
}
