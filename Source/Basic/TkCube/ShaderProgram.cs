using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
namespace TkCube
{
    public class ShaderProgram : IDisposable
    {
        private int _handle;
        private bool _disposedValue = false;
        public int Id { get { return _handle; } }
        protected ShaderProgram()
        {
            _handle = GL.CreateProgram();
        }
        public ShaderProgram(int handle)
        {
            _handle = handle;
        }                
        ~ShaderProgram()
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
        public void Link()
        {
            GL.LinkProgram(_handle);
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
        public void AddShader(string shaderFilepath,ShaderType type)
        {
            using var shader = Shader.CreateShader(shaderFilepath, type);            
            GL.AttachShader(_handle, shader.Id);
        }
        public static ShaderProgram CreateShaderProgram(string vertexShaderPath,string fragmentShaderPath)
        {
            using var vertexShader = Shader.CreateShader(vertexShaderPath, ShaderType.VertexShader);            
            using var fragmentShader = Shader.CreateShader(fragmentShaderPath, ShaderType.FragmentShader);                
            return ShaderProgram.CreateShaderProgram(vertexShader.Id, fragmentShader.Id);            
        }
        public static ShaderProgram CreateShaderProgram(params int[] shaderIds)
        {
            var programId = GL.CreateProgram();
            for (int i = 0; i < shaderIds.Length; ++i)
            {
                GL.AttachShader(programId, shaderIds[i]);                
            }
            GL.LinkProgram(programId);
            for (int i = 0; i < shaderIds.Length; i++)
            {                
                GL.DetachShader(programId, shaderIds[i]);
                var infoLog = GL.GetShaderInfoLog(shaderIds[i]);
            }            
            return new ShaderProgram(programId);
        }
        public static string LoadShaderCode(string filepath)
        {
            using (var reader = new StreamReader(filepath, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        public static void Log(int shaderId)
        {
            var infoLog = GL.GetShaderInfoLog(shaderId);
            if (infoLog != String.Empty)
            {
                Console.WriteLine(infoLog);
                return;
            }
            Console.WriteLine("No error detected on shader {0}", shaderId);
        }
    }
}
