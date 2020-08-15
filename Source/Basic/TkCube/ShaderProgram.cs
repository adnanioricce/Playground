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
        protected readonly List<Texture> _textures = new List<Texture>();
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
        public void SetInt(string variableName, int value)
        {
            GL.Uniform1(GetUniformLocation(variableName), value);
        }
        public void SetMatrix4(string variableName, Matrix4 value)
        {
            GL.UniformMatrix4(GetUniformLocation(variableName), false,ref value);
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
                var programInfoLog = GL.GetProgramInfoLog(programId);
            }            
            return new ShaderProgram(programId);
        }               
    }
}
