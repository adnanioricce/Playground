using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using StillTryingWalking.Extensions;
using System.Collections.Generic;

namespace StillTryingWalking
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 startPosition; 
        List<Pixel> pixels;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }        
        protected override void Initialize()
        {
            pixels = new List<Pixel>();
            startPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixels.Add(new Pixel(GraphicsDevice, startPosition ));
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var walk = new Vector2((float)(new Random().NextDouble()), (float)(new Random().NextDouble()));
            var nextPosition = startPosition + walk;
            startPosition = nextPosition;
            pixels.Add(new Pixel(GraphicsDevice,nextPosition));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred);
            foreach (Pixel pixel in pixels)
            {
                spriteBatch.Draw(pixel.texture, pixel.position, Color.Black);
            }            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
