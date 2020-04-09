using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake;
using System;

namespace SnakeGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        private Vector2 screen;
        private Snake snake;
        private Food food;
        private bool drawOff = true;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d); 
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureHelper.SetGraphicsDevice(GraphicsDevice);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Utils.Width = graphics.PreferredBackBufferWidth;
            Utils.Height = graphics.PreferredBackBufferHeight;
            graphics.ApplyChanges();
            this.screen = new Vector2(graphics.PreferredBackBufferWidth/2,graphics.PreferredBackBufferHeight/2);            
            snake = new Snake(new Vector2(this.screen.X,this.screen.Y),GraphicsDevice);
            food = new Food();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            HandleInput();
            snake.Update();
            food.Update(snake);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            //spriteBatch.Draw(snake._texture, snake._position, Color.White);
            food.Draw(spriteBatch);
            snake.Draw(spriteBatch);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
        protected override bool BeginDraw()
        {
            if (drawOff)
            {
                return base.BeginDraw();
            }else
            {
                return false;
            }
        }
        private void HandleInput()
        {
            var input = Keyboard.GetState();
            if (input.IsKeyDown(Keys.Left))
            {
                snake._direction = Keys.Left;                
            }
            if (input.IsKeyDown(Keys.Right))
            {                
                snake._direction = Keys.Right;
            }
            if (input.IsKeyDown(Keys.Up))
            {                
                snake._direction = Keys.Up;
            }
            if (input.IsKeyDown(Keys.Down))
            {                
                snake._direction = Keys.Down;
            }            
        }
    }
}
