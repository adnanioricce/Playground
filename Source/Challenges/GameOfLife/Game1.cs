using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private readonly (int X,int Y) centers = (X:400,Y: 400);        
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
            (centers.X,centers.Y) = (gridSize.Width / 2,gridSize.Height / 2);            
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
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                isPaused = !isPaused;
            }            
            if (isPaused) 
            {
                var mouseState = Mouse.GetState();
                var mousePosition = mouseState.Position;
                var roundedPositions = (X: mousePosition.X / scale, Y: mousePosition.Y / scale);                
                if (IsMouseClicked(mouseState))
                {
                    var erase = mouseState.RightButton == ButtonState.Pressed;
                    currentPattern.SetCell(roundedPositions, erase);                    
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
            spriteBatch.DrawGridLines(gridSize, centers, totalGridSize, whitePoint);            
            _lifeGrid.Draw(spriteBatch);            
            spriteBatch.End();
            base.Draw(gameTime);
        }                
        private bool IsMouseClicked(MouseState mouseState)
        {
            return mouseState.RightButton != ButtonState.Released || mouseState.LeftButton != ButtonState.Released;
        }
    }
}
