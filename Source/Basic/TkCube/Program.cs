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
            var vertices = GetCubeData();
            var vertexArray = VertexArray.CreateVertexArray(vertices);
            var shaderProgram = ShaderProgram.CreateShaderProgram("Shaders/vertex.shader", "Shaders/fragment.shader");                        
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
            var elementBuffer = ElementBuffer.CreateElementBuffer(indices);
            var containerTexture = Texture.LoadTexture("./Assets/Textures/container.jpg");
            var niceFaceTexture = Texture.LoadTexture("./Assets/Textures/awesomeface.png");
            shaderProgram.Use();
            shaderProgram.SetInt("texture1", 0);
            shaderProgram.SetInt("texture2", 1);
            var camera = Camera.CreateCamera(game.Width, game.Height);
            shaderProgram.Textures.AddRange(new Texture[] { containerTexture, niceFaceTexture });
            vertexArray.ElementBuffer = elementBuffer;
            vertexArray.Shader = shaderProgram;                             
            vertexArray.Camera = camera;
            vertexArray.AddVertexAttributes(attributes);
            vertexArray.SetVertexAttributes();
            game.AddVertexArrays(vertexArray);            
            game.Run(60.0);
        }
        public static Vector3[] CubePositions()
        {
            return new Vector3[]
            {
                new Vector3(0.0f,0.0f,0.0f),
                new Vector3(2.0f,5.0f,-15.0f),
                new Vector3(-1.5f,-2.2f,-2.5f),
                new Vector3(-3.8f,-2.0f,-12.3f),
                new Vector3(-2.4f,-0.4f,-3.5f),
                new Vector3(-1.7f,3.0f,-7.5f),
                new Vector3(1.3f,3.0f,-7.5f),
                new Vector3(1.5f,2.0f,-2.5f),
                new Vector3(1.5f,0.2f,-1.5f),
                new Vector3(-1.3f,1.0f,-1.5f),
            };
        }
        static Vertex[] GetCubeData()
        {
            return new Vertex[]
            {
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f),Color4.Transparent,new Vector2(0.0f, 0.0f)),
                new Vertex(new Vector3(0.5f, -0.5f, -0.5f),Color4.Transparent,new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(0.5f,  0.5f, -0.5f),Color4.Transparent,new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(0.5f,  0.5f, -0.5f),Color4.Transparent,new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f),Color4.Transparent,new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f),Color4.Transparent,new Vector2(0.0f, 0.0f)),

                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f),Color4.Transparent,new Vector2(0.0f, 0.0f)),
                new Vertex(new Vector3(0.5f, -0.5f,  0.5f),Color4.Transparent,new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(0.5f,  0.5f,  0.5f),Color4.Transparent,new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(0.5f,  0.5f,  0.5f),Color4.Transparent,new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f),Color4.Transparent,new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f),Color4.Transparent, new Vector2(0.0f, 0.0f)),

                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f),Color4.Transparent, new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f),Color4.Transparent, new Vector2(0.0f, 0.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),

                new Vertex(new Vector3(0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(0.5f,  0.5f, -0.5f),Color4.Transparent, new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(0.5f, -0.5f,  0.5f),Color4.Transparent, new Vector2(0.0f, 0.0f)),
                new Vertex(new Vector3(0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),

                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(0.5f, -0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(0.5f, -0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f),Color4.Transparent, new Vector2(0.0f, 0.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),

                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f)),
                new Vertex(new Vector3(0.5f,  0.5f, -0.5f),Color4.Transparent, new Vector2(1.0f, 1.0f)),
                new Vertex(new Vector3(0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(1.0f, 0.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f),Color4.Transparent, new Vector2(0.0f, 0.0f)),
                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f),Color4.Transparent, new Vector2(0.0f, 1.0f))
            };                          
        }
        static Vertex[] GetSimpleRectangleData()
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
