using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameOfLife
{
    //!Try to put all rendering code here, and all computation on "Library" project
    public class Game1 : Game
    {
        public const int UPS = 30;
        public const int FPS = 60;        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
	    LifeGrid _lifeGrid;
        private int[,] currentPattern;
        private bool isPaused = true;
        public static Texture2D whiteTexture;
        public static Texture2D whitePoint;
        private readonly (int Width, int Height) gridSize = (Width:1280,Height: 960);
        private readonly float scale = 16;
        private readonly int totalGridSize = 16;
        private readonly int gridCenterX = 400;
        private readonly int gridCenterY = 400;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = gridSize.Width;
            graphics.PreferredBackBufferHeight = gridSize.Height;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPS);            
            gridCenterX = gridSize.Width / 2;
            gridCenterY = gridSize.Height / 2;
            currentPattern = new int[80, 60];
        }

        protected override void Initialize()
        {            
            whiteTexture = GraphicsDevice.CreateTexture(Color.White,totalGridSize,totalGridSize);            
            whitePoint = GraphicsDevice.CreateTexture(Color.White,1,1);
            currentPattern.Initialize();
            _lifeGrid = new LifeGrid(currentPattern);            
            base.Initialize();
        }

	    protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);                                  
        }        
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isPaused = !isPaused;
            }            
            if (isPaused) 
            {
                var mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {                    
                    var mousePosition = mouseState.Position;
                    var roudedPositions = (X: mousePosition.X / scale, Y: mousePosition.Y / scale);
                    currentPattern[(int)Math.Abs(roudedPositions.X), (int)Math.Abs(roudedPositions.Y)] = 1;
                }                
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    var mousePosition = mouseState.Position;
                    var roudedPositions = (X: mousePosition.X / scale, Y: mousePosition.Y / scale);
                    currentPattern[(int)Math.Abs(roudedPositions.X), (int)Math.Abs(roudedPositions.Y)] = 0;
                }
                return;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _lifeGrid.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {            
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();            
            for (float x = -gridSize.Width; x < gridSize.Width; ++x)
            {
                Rectangle rectangle = new Rectangle((int)(gridCenterX + x * totalGridSize), 0, 1, gridSize.Height);
                spriteBatch.Draw(whitePoint, rectangle, Color.White);
            }
            for (float y = -gridSize.Height; y < gridSize.Height; ++y)
            {
                Rectangle rectangle = new Rectangle(0, (int)(gridCenterY + y * totalGridSize), gridSize.Width, 1);
                spriteBatch.Draw(whitePoint, rectangle, Color.White);
            }
            for (int x = 0; x < currentPattern.GetUpperBound(0); ++x)//GetUpperBound é o tamanho do array
            {
                for (int y = 0; y < currentPattern.GetUpperBound(1); ++y)
                {
                    int textureId = currentPattern[x, y];
                    if (textureId != 0)
                    {
                        var texturePosition = new Vector2(x * 16, y * 16);
                        spriteBatch.Draw(Game1.whiteTexture, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    }
                }
            }            
            if(!isPaused)
            {
                _lifeGrid.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }        
        private Vector2 ScalePosition(Point point)
        {
            var position = new Vector2();
            position.X = scale * point.X;
            position.Y = scale * point.Y;
            return position;
        }
    }
}
