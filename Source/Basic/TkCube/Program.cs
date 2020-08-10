using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TkCube
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var game = new Game(800,600,"TkCube");
            var shader = new Shader("Shaders/vertex.shader", "Shaders/fragment.shader");
            var vertexObject = VertexObject.CreateVertexObject(GetCubeData(), shader, DrawFunctions.DrawCube);                        
            //game.AddBuffer(vertexObject.Id);
            game.AddBuffer(vertexObject);
            //game.LoadBufferData(vertexObject.Id, GetCubeData());            
            game.Run(60.0);
        }
        static float[,] GetCubeData()
        {                        
            return new float[,]{
                {-1.0f, 0.0f, 0.0f},
                {0.0f, 1.0f, 0.0f},
                {1.0f, 0.0f, 0.0f},
                {0.0f, -1.0f, 0.0f},
                {0.0f, 0.0f, 1.0f},
                {0.0f, 0.0f, -1.0f}
            };             
        }
    }
}
