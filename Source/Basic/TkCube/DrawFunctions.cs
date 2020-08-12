using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

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
            GL.DrawElements(PrimitiveType.Triangles, 12,DrawElementsType.UnsignedInt, 0);
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                Console.WriteLine(string.Format("Error on {0} trying to draw elements. Error Code:{1}", nameof(DrawCube), error));
            }
        }
    }
}
