using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using Snake;
using System;
using System.IO;

namespace SnakeGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        Vector2 screen;
        Snake snake;
        Food food;
        bool drawOff;
        bool isRunning;
        Project _project;
        Desktop _desktop;
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
            drawOff = true;
            isRunning = false;
            _desktop = new Desktop();
            MyraEnvironment.Game = this;
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
            // UI
            string data = File.ReadAllText("Content/UI/Menu.xml");
            _project = Project.LoadFromXml(data);
            _desktop.Root = _project.Root;
            var button = (TextButton)_desktop.GetWidgetByID("_startButton");
            button.TouchDown += (s, a) =>
            {
                isRunning = !isRunning;
            };
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();            
            snake.Update();
            food.Update(snake);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (!isRunning)
            {
                _desktop.Render();
            }
            else
            {
                food.Draw(spriteBatch);
                snake.Draw(spriteBatch);
            }
            base.Draw(gameTime);
        }
        protected override bool BeginDraw()
        {
            return drawOff ? base.BeginDraw() : false;            
        }        
    }
}
