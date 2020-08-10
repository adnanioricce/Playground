using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TkCube
{
    public class GameWindow : OpenTK.GameWindow
    {
        //Tem alguma maneira de definir uma lista de "comandos" que eu quero executar todo frame?
        private readonly List<VertexBuffer> _vertexes = new List<VertexBuffer>();
        private readonly List<VertexArray> _vertexArrays = new List<VertexArray>();
        public GameWindow(int width,int height,string title) : base(width,height,GraphicsMode.Default,title)
        {                                    
        }
        public void AddVertexArrays(params VertexArray[] vertexArrays)
        {
            _vertexArrays.AddRange(vertexArrays);            
        }        
        //TODO:Change float[,] to Vector3[]                       
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            base.OnUpdateFrame(e);
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);            
            _vertexArrays.ForEach(vao => vao.Draw());
            GL.BindVertexArray(0);
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }        
    }
}
