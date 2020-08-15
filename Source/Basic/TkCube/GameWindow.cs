using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TkCube
{
    public class GameWindow : OpenTK.GameWindow
    {
        //Tem alguma maneira de definir uma lista de "comandos" que eu quero executar todo frame?        
        private readonly List<VertexArray> _vertexArrays = new List<VertexArray>();        
        private static readonly Camera _camera = Camera.CreateCamera(800,600);
        private readonly Vector3[] positions = Program.CubePositions();
        public static Camera Camera { get { return _camera; } }
        private double _time = 0.0;
        public GameWindow(int width,int height,string title) : base(width,height,GraphicsMode.Default,title)
        {            
        }
        public void AddVertexArrays(params VertexArray[] vertexArrays)
        {
            _vertexArrays.AddRange(vertexArrays);            
        }
        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DebugOutput);
            GL.DebugMessageCallback(Logger.MessageCallBack, (IntPtr)0);            
            base.OnLoad(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Logger.Log(this.GetType().Name, nameof(OnUpdateFrame));
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if(_time >= 360.0)
            {
                _time = 0.0;
            }
            _time += e.Time * 30;
            _camera.Model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
            base.OnUpdateFrame(e);
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {            
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);            
            Logger.Log(this.GetType().Name, nameof(OnRenderFrame));
            
            _vertexArrays.ForEach(vao =>
            {                
                
                
                vao.Draw(DrawFunctions.DrawCube);
            });
            //GL.BindVertexArray(0);
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }        
    }
}
