using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace TkCube
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var game = new GameWindow(800,600,"TkCube");
            var shader = new Shader("Shaders/vertex.shader", "Shaders/fragment.shader");
            //var texture = Texture.LoadTexture("Assets/Textures/wall.jpg");
            //var vertexObject = VertexBuffer.CreateVertexObject(GetCubeData(), shader, DrawFunctions.DrawCube);                                    
            //game.AddBuffer(vertexObject);                        
            var vertices = GetCubeData();
            var attributes = new[]{
                    new VertexAttribute("aPosition", 3, VertexAttribPointerType.Float, Vertex.Size, 0),
                    new VertexAttribute("vColor", 4, VertexAttribPointerType.Float, Vertex.Size, 3 * sizeof(float)),
                    new VertexAttribute("aTexCoord", 2, VertexAttribPointerType.Float, Vertex.Size, 7 * sizeof(float))
                };
            var vertexArray = VertexArray.CreateVertexArray(vertices, shader, DrawFunctions.DrawCube, "Assets/Textures/wall.jpg",attributes );
            game.AddVertexArrays(vertexArray);           
            game.Run(60.0);
        }
        static Vertex[] GetCubeData()
        {
            return new Vertex[]
            {
                new Vertex(new Vector3(-1.0f, 0.0f, 0.0f),Color4.Lime,new Vector2(1.0f,1.0f)),
                new Vertex(new Vector3(0.0f, 1.0f, 0.0f),Color4.Magenta,new Vector2(1.0f,0.5f)),
                new Vertex(new Vector3(1.0f, 0.0f, 0.0f),Color4.NavajoWhite,new Vector2(1.0f,0.0f)),
                new Vertex(new Vector3(0.0f, -1.0f, 0.0f),Color4.Moccasin, new Vector2(0.0f,0.0f)),
                new Vertex(new Vector3(0.0f, 0.0f, 1.0f),Color4.DarkGreen, new Vector2(0.0f,1.0f)),
                new Vertex(new Vector3(0.0f, 0.0f, -1.0f),Color4.LightYellow,new Vector2(0.0f,0.5f))
            };
            //return new float[,]{
            //    {-1.0f, 0.0f, 0.0f},
            //    {0.0f, 1.0f, 0.0f},
            //    {1.0f, 0.0f, 0.0f},
            //    {0.0f, -1.0f, 0.0f},
            //    {0.0f, 0.0f, 1.0f},
            //    {0.0f, 0.0f, -1.0f}
            //};             
        }
    }
}
