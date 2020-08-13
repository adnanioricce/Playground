using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TkCube
{
    public class BufferHelper
    {
        public void LoadBufferData(int bufferId,Vertex[] vertices, BufferTarget target = BufferTarget.ArrayBuffer,BufferUsageHint hintUsage = BufferUsageHint.StaticDraw)
        {
            var bufferDataTypeSize = Marshal.SizeOf(vertices[0]);
            GL.BindBuffer(target, bufferId);
            GL.BufferData(target, vertices.Length * bufferDataTypeSize, vertices, hintUsage);
        }
        //public void LoadA
    }
}
