using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace TkCube
{
    public class ElementBuffer
    {
        public int Id { get; protected set; }
        public int IndicesCount { get; protected set; }
        protected ElementBuffer(int id,int indicesCount)
        {
            Id = id;
            IndicesCount = indicesCount;
        }
        public static ElementBuffer CreateElementBuffer(uint[] indices)
        {
            //TODO:check for invalid input
            var indiceDataTypeSize = Marshal.SizeOf(indices[0]);
            var elementBufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * indiceDataTypeSize, indices, BufferUsageHint.StaticDraw);
            return new ElementBuffer(elementBufferId, indices.Length);
        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,this.Id);
        }
        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
