﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TkCube.Graphics.Vertices.Buffers
{
    internal interface IBuffer : IDisposable
    {
        void Bind();        
        void LoadData<TVertex>(TVertex[] vertices) where TVertex : struct;
    }
}
