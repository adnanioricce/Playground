using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace StillTryingWalking
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Pixel pixel;
        //turn off the redraw of the back buffer
        bool drawOff = true;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }        
        protected override void Initialize()
        {                        
            pixel = new Pixel(GraphicsDevice, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            pixel.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            pixel.Draw(spriteBatch);
            base.Draw(gameTime);
        }
        protected override bool BeginDraw()
        {
            //this question and answer saved my sanity
            //https://stackoverflow.com/questions/4054936/manual-control-over-when-to-redraw-the-screen/4057180#4057180
            if (drawOff) {                
                return base.BeginDraw();
            }
            else
            {
                return false;
            }
        }
        
    }
}
