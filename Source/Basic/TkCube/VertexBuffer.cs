using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TkCube
{
    public class VertexBuffer : IDisposable
    {
        protected readonly int _bufferDataTypeSize = 0;
        protected readonly List<VertexAttribute> _vertexAttributes = new List<VertexAttribute>();
        public virtual int Id { get; protected set; }
        public virtual int VerticesCount { get; protected set; }        
        protected VertexBuffer(int vertexBufferId, int bufferDataTypeSize,int verticesCount) : this(vertexBufferId, bufferDataTypeSize)
        {
            VerticesCount = verticesCount;
        }
        protected VertexBuffer(int vertexBufferId, int bufferDataTypeSize)
        {
            Id = vertexBufferId;
            _bufferDataTypeSize = bufferDataTypeSize;
        }        
        protected VertexBuffer(int vertexBufferId) : this(vertexBufferId,Vertex.Size)
        {
            Id = vertexBufferId;
        }
        protected VertexBuffer(){}       
        public static VertexBuffer CreateVertexObject(Vertex[] vertices) 
        {
            var bufferId = GL.GenBuffer();
            var bufferDataTypeSize = Marshal.SizeOf(vertices[0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * bufferDataTypeSize, vertices, BufferUsageHint.StreamDraw);
            return new VertexBuffer(bufferId, bufferDataTypeSize, vertices.Length);
        }                
        public void AddVertexAttributes(params VertexAttribute[] vertexAttributes)
        {            
            _vertexAttributes.AddRange(vertexAttributes);            
        }        
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.Id);
        }                    
        public void LoadData(float[,] vertices)
        {
            var bufferDataTypeSize = Marshal.SizeOf(vertices[0, 0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.Id);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * bufferDataTypeSize, vertices, BufferUsageHint.StaticDraw);            
        }

        public void Dispose()
        {
            GL.DeleteBuffer(this.Id);
        }
    }
}
