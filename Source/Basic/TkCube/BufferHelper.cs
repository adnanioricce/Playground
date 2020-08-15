using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TkCube
{
    public static class BufferHelper
    {
        public static void LoadBufferData(int bufferId,Vertex[] vertices, BufferTarget target = BufferTarget.ArrayBuffer,BufferUsageHint hintUsage = BufferUsageHint.StaticDraw)
        {
            var bufferDataTypeSize = Marshal.SizeOf(vertices[0]);
            GL.BindBuffer(target, bufferId);
            GL.BufferData(target, vertices.Length * bufferDataTypeSize, vertices, hintUsage);
        }
        public static void LoadBufferData(int bufferId, uint[] indices, BufferTarget target = BufferTarget.ElementArrayBuffer, BufferUsageHint hintUsage = BufferUsageHint.StaticDraw)
        {
            var bufferDataTypeSize = Marshal.SizeOf(indices[0]);
            GL.BindBuffer(target, bufferId);
            GL.BufferData(target, indices.Length * bufferDataTypeSize, indices, hintUsage);
        }
        //public void LoadA
    }
}
