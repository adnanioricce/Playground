using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;

namespace GameOfLife
{
    //!Try to put all rendering code here, and all computation on "Library" project
    public class Game1 : Game
    {
        public const int UPS = 20;
        public const int FPS = 60;
        int counter = 1;
        int patternCount = 0;
        int limit = 50;
        float countDuration = 2f; 
        float currentTime = 0f;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
	    LifeGrid _lifeGrid;
        private string[] patterns;
        private static int[,] currentPattern;

        public static Texture2D whiteTexture;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPS);
        }

        protected override void Initialize()
        {                       
            Console.WriteLine(Directory.GetCurrentDirectory()); 
            currentPattern = LifeReader.GetGridFromPlaintextFile("./Patterns/somePatternThatIDontKnowTheName.txt");
            whiteTexture = CreateWhiteTexture(new Texture2D(GraphicsDevice, 4, 4));
	        _lifeGrid = new LifeGrid(currentPattern);            
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
            _lifeGrid.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            _lifeGrid.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private Texture2D CreateWhiteTexture(Texture2D _whiteTexture)
        {
            Color[] color = CreateColor(Color.White, 4, 4);
            _whiteTexture.SetData(color);
            return _whiteTexture;
        }
        private Color[] CreateColor(Color color, int width, int height)
        {
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = color;

            return colors;
        }        
    }
}
