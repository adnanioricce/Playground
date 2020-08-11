using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
namespace TkCube
{
    public class Shader : IDisposable
    {
        private int _handle;
        private bool _disposedValue = false;
        private readonly List<VertexAttribute> _attributes = new List<VertexAttribute>();
        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource, FragmentShaderSource;
            // Loading Shaders
            using (var reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }           
            using (var reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }
            // Creating shader ids on GPU
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, VertexShaderSource);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, FragmentShaderSource);
            // Compiling shaders 
            GL.CompileShader(vertexShader);
            GL.CompileShader(fragmentShader);
            var (infoLogVert, infoLogFrag) = (GL.GetShaderInfoLog(vertexShader), GL.GetShaderInfoLog(fragmentShader));
            if (infoLogVert != String.Empty)
            {
                Console.WriteLine(infoLogVert);
            }
            if (infoLogFrag != String.Empty)
            {
                Console.WriteLine(infoLogFrag);
            }
            // Create program id on GPU
            _handle = GL.CreateProgram();
            // attack shaders to program;
            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);
            // link program on GPU
            GL.LinkProgram(_handle);
            // Cleaning
            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }
        ~Shader()
        {
            GL.DeleteProgram(_handle);
        }
        protected virtual bool Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                GL.DeleteProgram(_handle);

                _disposedValue = true;
            }
            return false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Use()
        {
            GL.UseProgram(_handle);
        }        
        public int GetAttribPointer(string variableName)
        {
            return GL.GetAttribLocation(_handle, variableName);
        }
        public int GetUniformLocation(string variableName)
        {
            return GL.GetUniformLocation(_handle, variableName);
        }
        public void SetUniform(string variableName, int value)
        {
            GL.Uniform1(GetUniformLocation(variableName), value);
        }
    }
}
