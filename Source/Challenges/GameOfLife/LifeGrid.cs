using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameOfLife
{
	public class LifeGrid
	{
		private readonly Grid _grid;				
        private readonly Vector2 position = Vector2.Zero;
        public TimeSpan updateTimer;
        private int[,] lastGeneration;        
		public LifeGrid(int width,int height)
		{
			_grid = new Grid(width,height);
            lastGeneration.Initialize();
            for (int i = 0; i < _grid.Width; i++)
                for (int j = 0; j < _grid.Height; ++j)
                    lastGeneration[i, j] = _grid.TileCell[i, j];
            updateTimer = TimeSpan.Zero;
		}
        public LifeGrid(int[,] initialState)
        {
            _grid = new Grid(initialState);
            lastGeneration = new int[_grid.Scales.X, _grid.Scales.Y];
            for (int i = 0; i < _grid.Width; i++)
                for (int j = 0; j < _grid.Height; ++j)
                    lastGeneration[i, j] = _grid.TileCell[i, j];
            updateTimer = TimeSpan.Zero;
        }
        public LifeGrid(string stateFilepath)
        {
            _grid = new Grid(LifeReader.GetGridFromPlaintextFile(stateFilepath));
            lastGeneration = _grid.TileCell;
            for (int i = 0; i < _grid.Width; i++)
                for (int j = 0; j < _grid.Height; ++j)
                    lastGeneration[i, j] = _grid.TileCell[i, j];
            updateTimer = TimeSpan.Zero;
        }
		//Change state of grid here, leave nextGrid outside of this class
		public void Draw(SpriteBatch spriteBatch)
		{                                        
            for (int x = 0; x < _grid.Width; ++x)//GetUpperBound é o tamanho do array
            {
                for (int y = 0; y < _grid.Height; ++y)
                {
                    int textureId = _grid.TileCell[x, y];
                    if (textureId != 0)
                    {
                        var texturePosition = new Vector2(x * 16, y * 16) + position;
                        spriteBatch.Draw(Game1.whiteTexture, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    }
                }
            }            
		}        
        public void Update(GameTime gameTime)
        {
            updateTimer += gameTime.ElapsedGameTime;
            if (updateTimer.TotalMilliseconds > 1000f / Game1.UPS)
            { 
                updateTimer = TimeSpan.Zero;
                _grid.CreateNextGeneration();
                for (int i = 0; i < _grid.Scales.X; i++){
                    for (int j = 0; j < _grid.Scales.Y; j++){
                        lastGeneration[i, j] = _grid.TileCell[i, j];
                    }
                }
            }
		}
		//TODO:Create or Add package for generic color buffer factory method
		
	}
}
