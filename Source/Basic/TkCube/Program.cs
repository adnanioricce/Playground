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
            var shaderProgram = ShaderProgram.CreateShaderProgram("Shaders/vertex.shader", "Shaders/fragment.shader");            
            var vertices = GetCubeData();
            var attributes = new[]{
                    new VertexAttribute("aPosition", 3, VertexAttribPointerType.Float, Vertex.Size, 0),
                    new VertexAttribute("vColor", 4, VertexAttribPointerType.Float, Vertex.Size, 3 * sizeof(float)),
                    new VertexAttribute("aTexCoord", 2, VertexAttribPointerType.Float, Vertex.Size, 7 * sizeof(float))
                };
            var indices = new uint[]
            {
                0, 1, 3,   
                1, 2, 3
            };            
            var vertexArray = VertexArray.CreateVertexArray(vertices, shaderProgram, ElementBuffer.CreateElementBuffer(indices), "Assets/Textures/wall.jpg",attributes );
            game.AddVertexArrays(vertexArray);
            game.Run(60.0);
        }
        static Vertex[] GetCubeData()
        {
            return new Vertex[]
            {
                new Vertex(new Vector3(0.5f,  0.5f, 0.0f),Color4.Lime,new Vector2(1.0f,1.0f)),
                new Vertex(new Vector3(0.5f, -0.5f, 0.0f),Color4.Magenta,new Vector2(1.0f,0.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, 0.0f),Color4.NavajoWhite,new Vector2(0.0f,0.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f, 0.0f ),Color4.Moccasin, new Vector2(0.0f,1.0f))               
            };                
        }
    }
}
