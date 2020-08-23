using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace TkCube
{
    public class VertexArray : IDisposable
    {
        protected readonly List<Texture> _textures = new List<Texture>();        
        public virtual int Id { get; protected set; }
        public virtual VertexBuffer VertexBuffer { get; set; }
        public virtual ShaderProgram Shader { get; set; }
        public virtual List<Texture> Textures { get { return _textures; } }
        public virtual ElementBuffer ElementBuffer { get; set; }
        public Camera Camera { get; set; }
        private readonly List<VertexAttribute> _vertexAttributes = new List<VertexAttribute>();        
        protected VertexArray(int vertexArrayId, VertexBuffer vertexBuffer)
        {
            Id = vertexArrayId;
            VertexBuffer = vertexBuffer;
        }
        protected VertexArray(int vertexArrayId, VertexBuffer vertexBuffer, ShaderProgram shader, Texture texture,ElementBuffer elementBuffer, params VertexAttribute[] vertexAttributes)
        {
            Id = vertexArrayId;
            VertexBuffer = vertexBuffer;
            Shader = shader;
            Textures.Add(texture);
            ElementBuffer = elementBuffer;
            _vertexAttributes.AddRange(vertexAttributes);
        }
        public static VertexArray CreateVertexArray(Vertex[] vertices)
        {
            var vertexArrayId = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayId);
            var vertexBuffer = VertexBuffer.CreateVertexObject(vertices);
            return new VertexArray(vertexArrayId, vertexBuffer);
        }

        public void UnBind()
        {
            GL.BindVertexArray(0);
        }

        public static VertexArray CreateVertexArray(VertexBuffer vertexBuffer,ShaderProgram shader,ElementBuffer elementBuffer)
        {
            var vertexArrayId = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayId);
            var vertexArray = new VertexArray(vertexArrayId, vertexBuffer);
            vertexArray.Shader = shader;
            vertexArray.ElementBuffer = elementBuffer;            
            return vertexArray;
        }
        public static VertexArray CreateVertexArray(Vertex[] vertices, ShaderProgram shader, ElementBuffer elementBuffer, string textureFilepath, params VertexAttribute[] vertexAttributes)
        {
            var vertexArrayId = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayId);            
            var vertexBuffer = VertexBuffer.CreateVertexObject(vertices);            
            var texture = Texture.LoadTexture(textureFilepath);            
            var vertexArray = new VertexArray(vertexArrayId, vertexBuffer, shader, texture, elementBuffer, vertexAttributes);                        
            return vertexArray;
        }                
        
        public void AddTextures(params Texture[] textures)
        {
            _textures.AddRange(textures);
        }
        public void Draw(DrawFunction drawFunction)
        {
            //this.Shader.BindTextures();
            //this.ElementBuffer.Bind();
            drawFunction(this, this.VertexBuffer.VerticesCount);
            //this.Shader.UnbindTexture();
        }
        
        public void Dispose()
        {
            this.VertexBuffer.Dispose();
            this.Shader.Dispose();
            GL.DeleteVertexArray(this.Id);
        }
    }
}
