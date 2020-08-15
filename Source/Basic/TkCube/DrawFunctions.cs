using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

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
            vertexArray.SetProjection(GameWindow.Camera);
            GL.BindVertexArray(vertexArray.Id);
            for (int i = 0; i < CubePositions.Length; i++)
            {
                //var model = GameWindow.Camera.Model * Matrix4.CreateTranslation(CubePositions[i]);
                float angle = 20.0f * i;
                var model = GameWindow.Camera.Model * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle));                
                model = model * Matrix4.CreateScale(new Vector3(0.5f,0.5f,0.5f)) * Matrix4.CreateTranslation(CubePositions[i]);
                vertexArray.Shader.SetMatrix4("model", model);
                GL.DrawArrays(PrimitiveType.Triangles, 0, verticesCount);
            }
            
            Logger.Log(nameof(DrawFunctions), nameof(DrawCube));
            //var error = GL.GetError();
            //if (error != ErrorCode.NoError)
            //{                                        
            //    Console.WriteLine(string.Format("Error on {0} trying to draw elements. Error Code:{1}", nameof(DrawCube), error));
            //}
        }
    }
}
