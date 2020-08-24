using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Starfield
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector3 camTarget;
        Vector3 camPosition;
        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;
        bool orbit = false;
        BasicEffect basicEffect;
        StarField starfield;        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            basicEffect = new BasicEffect(GraphicsDevice);
            camTarget = new Vector3(0f, 40f, 10f);
            camPosition = new Vector3(0f, 40f, -100f);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f),GraphicsDevice.DisplayMode.AspectRatio,1f, 1000f);
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,new Vector3(0f, 1f, 0f));
            worldMatrix = Matrix.CreateWorld(camPosition, new Vector3(0,0,-1),Vector3.Up);
            
            basicEffect.VertexColorEnabled = true;
            
            Settings.ScreenSpace.X = GraphicsDevice.Adapter.CurrentDisplayMode.Width/2;
            Settings.ScreenSpace.Y = GraphicsDevice.Adapter.CurrentDisplayMode.Height/2;
            starfield = new StarField();
            starfield.SetEffect(basicEffect);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //if(GamePad.)
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camPosition.X += 1f;
                camTarget.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camPosition.X -= 1f;
                camTarget.X -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camPosition.Y += 1f;
                camTarget.Y += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camPosition.Y -= 1f;
                camTarget.Y -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                camPosition.Z -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                camPosition.Z += 1f;                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                orbit = !orbit;
            }

            if (orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                camPosition = Vector3.Transform(camPosition,rotationMatrix);
            }
            Console.WriteLine($"cam position coordinates X:{camPosition.X} ,Y:{camPosition.Y},Z:{camPosition.Z}");
            Console.WriteLine($"cam target coordinates X:{camTarget.X} ,Y:{camTarget.Y},Z:{camTarget.Z}");
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,Vector3.Down);
            starfield.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;
            GraphicsDevice.Clear(Color.Black);            
            starfield.Draw(GraphicsDevice);                       
            base.Draw(gameTime);
        }
    }
}
