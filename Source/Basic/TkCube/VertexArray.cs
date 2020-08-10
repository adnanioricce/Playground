using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace TkCube
{
    public class VertexArray : IDisposable
    {
        public virtual int Id { get; protected set; }
        public virtual VertexBuffer VertexBuffer { get; protected set; }
        public virtual Shader Shader { get; protected set; }
        private DrawFunction _drawFunction;
        private readonly List<VertexAttribute> _vertexAttributes = new List<VertexAttribute>();
        protected VertexArray(int vertexArrayId, VertexBuffer vertexBuffer,Shader shader,DrawFunction drawFunction,params VertexAttribute[] vertexAttributes)
        {
            Id = vertexArrayId;
            VertexBuffer = vertexBuffer;
            Shader = shader;
            _drawFunction = drawFunction;
            _vertexAttributes.AddRange(vertexAttributes);
        }
        public static VertexArray CreateVertexArray(Vertex[] vertices, Shader shader,DrawFunction drawFunction,params VertexAttribute[] vertexAttributes)
        {
            var vertexArrayId = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayId);
            var vertexBuffer = VertexBuffer.CreateVertexObject(vertices);
            var vertexArray = new VertexArray(vertexArrayId, vertexBuffer, shader, drawFunction,vertexAttributes);
            vertexArray.SetVertexAttributes();
            return vertexArray;
        }        
        public void SetVertexAttributes()
        {
            _vertexAttributes.ForEach(attribute => attribute.Set(this.Shader));
        }
        public void Draw()
        {
            this.Shader.Use();
            this._drawFunction(this.Id,this.VertexBuffer.VerticesCount);
        }
        
        public void Dispose()
        {
            this.VertexBuffer.Dispose();
            this.Shader.Dispose();
            GL.DeleteVertexArray(this.Id);
        }
    }
}
