using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TkCube
{
    public class Game : GameWindow
    {
        //Tem alguma maneira de definir uma lista de "comandos" que eu quero executar todo frame?
        private readonly List<VertexObject> _vertexes = new List<VertexObject>();                        
        public Game(int width,int height,string title) : base(width,height,GraphicsMode.Default,title)
        {                        
            //GL.Enable(EnableCap.DepthTest);
        }
                             
        public void AddBuffer(VertexObject vertexObject)
        {
            _vertexes.Add(vertexObject);
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
            _vertexes.ForEach(vbo => vbo.Draw());
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }        
    }
}
