using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace TkCube
{
    public class VertexArray : IDisposable
    {
        public virtual int Id { get; protected set; }
        public virtual VertexBuffer VertexBuffer { get; protected set; }
        public virtual ShaderProgram Shader { get; protected set; }
        public virtual Texture Texture { get; protected set; }
        public virtual ElementBuffer ElementBuffer { get; protected set; }        
        private readonly List<VertexAttribute> _vertexAttributes = new List<VertexAttribute>();
        protected VertexArray(int vertexArrayId, VertexBuffer vertexBuffer, ShaderProgram shader, Texture texture,ElementBuffer elementBuffer, params VertexAttribute[] vertexAttributes)
        {
            Id = vertexArrayId;
            VertexBuffer = vertexBuffer;
            Shader = shader;
            Texture = texture;
            ElementBuffer = elementBuffer;            
            _vertexAttributes.AddRange(vertexAttributes);            
        }
        public static VertexArray CreateVertexArray(Vertex[] vertices, ShaderProgram shader, ElementBuffer elementBuffer, string textureFilepath, params VertexAttribute[] vertexAttributes)
        {
            var vertexArrayId = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayId);
            var vertexBuffer = VertexBuffer.CreateVertexObject(vertices);
            var texture = Texture.LoadTexture(textureFilepath);
            var vertexArray = new VertexArray(vertexArrayId, vertexBuffer, shader, texture, elementBuffer, vertexAttributes);
            vertexArray.SetVertexAttributes();
            return vertexArray;
        }
        public void SetVertexAttributes()
        {
            _vertexAttributes.ForEach(attribute => attribute.Set(this.Shader));
        }        
        public void Draw(DrawFunction drawFunction)
        {            
            this.Shader.Use();
            this.Texture.Bind();
            this.ElementBuffer.Bind();
            drawFunction(this.Id, this.VertexBuffer.VerticesCount);
            //this._drawFunction(this.Id,this.VertexBuffer.VerticesCount);
        }
        
        public void Dispose()
        {
            this.VertexBuffer.Dispose();
            this.Shader.Dispose();
            GL.DeleteVertexArray(this.Id);
        }
    }
}
