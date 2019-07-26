using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace GaussianDistribution
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Distribution _distribution;
        FrameCounter _frameCounter;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here           
            _distribution = new Distribution(100,10,GraphicsDevice);
            _frameCounter = new FrameCounter();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);                               
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(_frameCounter.Update(gameTime.GetElapsedSeconds() * 60))
                _distribution.Update(GraphicsDevice,gameTime);

            base.Update(gameTime);
        }        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            Console.WriteLine($"elapsed time: {gameTime.ElapsedGameTime.TotalSeconds}");
            if (_frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 60))
            {                
                Console.WriteLine($"FPS:{_frameCounter.AverageFramesPerSecond}");
                _distribution.Draw(spriteBatch);
            }
            Console.WriteLine($"FPS:{_frameCounter.AverageFramesPerSecond}");
            spriteBatch.Begin();
            //spriteBatch.DrawRectangle(Vector2.);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
