using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Cube
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        
        private BasicEffect _effect;
        private VertexBuffer _vertexBuffer;
        private Vector3 _cameraPosition;
        private Vector3 _cameraTarget;
        private Vector3 _cameraLookAtVector;
        private Vector3 _cameraUpVector;
        private Matrix _projection;
        private Matrix _view;
        private Matrix _world;
        private bool _orbit;
        //private IndexBuffer _indexBuffer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            base.Initialize();
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.DisplayMode.AspectRatio, 1f, 1000f);
            _effect = new BasicEffect(GraphicsDevice);
            _cameraPosition = new Vector3(0f, 0f, 100f);
            _cameraLookAtVector = new Vector3(-50, 12f, -10f); 
            _cameraUpVector = Vector3.UnitZ;
            _view = Matrix.CreateLookAt(_cameraPosition, _cameraLookAtVector, new Vector3(0, 1f, 0));
            _world = Matrix.CreateWorld(_cameraPosition, Vector3.Forward, Vector3.Up);
            _vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), 8, BufferUsage.WriteOnly);
            _vertexBuffer.SetData<VertexPositionTexture>(Cubes.GetSimpleCubeData());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            CheckCameraMovement();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.DrawGround();                     

            base.Draw(gameTime);
        }
        private void CheckCameraMovement()
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
            {
                _cameraPosition.X += 10f;
                _cameraLookAtVector.X += 10f;                
            }
            if (state.IsKeyDown(Keys.Right))
            {
                _cameraPosition.X -= 10f;
                _cameraLookAtVector.X -= 10f;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                _cameraPosition.Y += 10f;
                _cameraLookAtVector.Y += 10f;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                _cameraPosition.Y -= 10f;
                _cameraLookAtVector.Y -= 10f;
            }
            if (state.IsKeyDown(Keys.OemPlus))
            {
                _cameraPosition.Z -= 10f;
            }
            if (state.IsKeyDown(Keys.OemMinus))
            {
                _cameraPosition.Z += 10f;
            }
            if (state.IsKeyDown(Keys.Space))
            {
                _orbit = !_orbit;
            }

            if (_orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                _cameraPosition = Vector3.Transform(_cameraPosition, rotationMatrix);
            }
            Debug.WriteLine($"Camera Position {_cameraPosition},Camera Target {_cameraTarget},orbit:{_orbit}");
        }
        private void DrawGround()
        {
            _effect.Projection = _projection;
            _effect.View = _view;
            _effect.World = _world;
            _effect.VertexColorEnabled = true;
            GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 8);
                //GraphicsDevice.DrawUserPrimitives(
                //    // We’ll be rendering two trinalges
                //    PrimitiveType.TriangleList,
                //    // The array of verts that we want to render
                //    _vertexBuffer,
                //    // The offset, which is 0 since we want to start
                //    // at the beginning of the floorVerts array
                //    0,
                //    // The number of triangles to draw
                //    2);
            }
        }
    }
}
