using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Threading.Tasks;

namespace TkCube
{
    /// <summary>
    /// Delegate for a GL draw function. May be a vertex array Id or a vertex object Id
    /// </summary>
    /// <param name="vertexObjectId">The vertex object or vertex array Id</param>
    public delegate void DrawFunction(VertexArray vertexArray, int verticesCount);
    public static class DrawFunctions
    {
        private static Vector3[] CubePositions = Program.CubePositions();
        public static void DrawCube(VertexArray vertexArray,int verticesCount)
        {
            vertexArray.Shader.BindTextures();
            GL.BindVertexArray(vertexArray.Id);
            vertexArray.ElementBuffer.Bind();
            vertexArray.SetProjection(vertexArray.Camera);                        
            for (int i = 0; i < CubePositions.Length; i++)
            {
                //var model = GameWindow.Camera.Model * Matrix4.CreateTranslation(CubePositions[i]);
                float angle = 20.0f * i;
                var model = vertexArray.Camera.Model * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle));                
                model = model * Matrix4.CreateScale(new Vector3(0.5f,0.5f,0.5f)) * Matrix4.CreateTranslation(CubePositions[i]);
                vertexArray.Shader.SetMatrix4("model", model);
                GL.DrawArrays(PrimitiveType.Triangles, 0, verticesCount);
            }
            vertexArray.ElementBuffer.UnBind();
            vertexArray.Shader.UnbindTexture();
            vertexArray.UnBind();
            Logger.Log(nameof(DrawFunctions), nameof(DrawCube));            
        }
        public static void DrawElements(VertexArray vertexArray, int verticesCount)
        {
            GL.BindVertexArray(vertexArray.Id);
            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }
    }
}
