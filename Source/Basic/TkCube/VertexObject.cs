using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TkCube
{
    public class VertexObject
    {
        protected readonly int _bufferDataTypeSize = 0;
        public virtual int Id { get; protected set; }
        public virtual int VerticesCount { get; protected set; }
        public virtual Shader Shader { get; protected set; }
        public DrawFunction DrawFunction { get; set; }
        protected VertexObject(int vertexObjectId, int verticesCount,int bufferDataTypeSize, Shader shader) : this(vertexObjectId, verticesCount,bufferDataTypeSize)
        {
            Shader = shader;
        }
        protected VertexObject(int vertexObjectId, int bufferDataTypeSize,int verticesCount) : this(vertexObjectId, bufferDataTypeSize)
        {
            VerticesCount = verticesCount;
        }
        protected VertexObject(int vertexObjectId, int bufferDataTypeSize)
        {
            Id = vertexObjectId;
            _bufferDataTypeSize = bufferDataTypeSize;
        }        
        protected VertexObject(int vertexObjectId) : this(vertexObjectId,sizeof(float))
        {
            Id = vertexObjectId;
        }
        protected VertexObject(){}
        protected void SetDefaultAttrib(int bufferDataTypeSize)
        {
            //TODO: leave shader configuration more dynamically
            GL.VertexAttribPointer(this.Shader.GetAttribPointer("aPosition"), 3, VertexAttribPointerType.Float, false, 3 * bufferDataTypeSize, 0);
            GL.EnableVertexAttribArray(this.Shader.GetAttribPointer("aPosition"));
        }
        public static VertexObject CreateVertexObject(float[,] vertices) 
        {
            var bufferId = GL.GenBuffer();
            var bufferDataTypeSize = Marshal.SizeOf(vertices[0, 0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * bufferDataTypeSize, vertices, BufferUsageHint.StaticDraw);
            return new VertexObject(bufferId,bufferDataTypeSize,vertices.Length);
        }
        public static VertexObject CreateVertexObject(float[,] vertices,Shader shader)
        {
            var vertexObject = CreateVertexObject(vertices);
            vertexObject.Shader = shader;            
            GL.VertexAttribPointer(vertexObject.Shader.GetAttribPointer("aPosition"), 3, VertexAttribPointerType.Float, false, vertices.GetLength(1) * vertexObject._bufferDataTypeSize, 0);
            GL.EnableVertexAttribArray(vertexObject.Shader.GetAttribPointer("aPosition"));
            return vertexObject;
        }
        public static VertexObject CreateVertexObject(float[,] vertices, Shader shader, DrawFunction drawFunction)
        {
            var vertexObject = CreateVertexObject(vertices, shader);
            vertexObject.DrawFunction = drawFunction;
            return vertexObject;
        }
        public void Draw()
        {
            this.Shader.Use();
            DrawFunction(this.Id);   
        }
              
        public void LoadData(float[,] vertices)
        {
            var bufferDataTypeSize = System.Runtime.InteropServices.Marshal.SizeOf(vertices[0, 0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.Id);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * bufferDataTypeSize, vertices, BufferUsageHint.StaticDraw);
            SetDefaultAttrib(bufferDataTypeSize);
        }

    }
}
