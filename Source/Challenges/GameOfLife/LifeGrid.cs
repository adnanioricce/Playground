using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameOfLife
{
	public class LifeGrid
	{
		private readonly Grid _grid;				
        private readonly Vector2 position = Vector2.Zero;
        public TimeSpan updateTimer = TimeSpan.Zero;
        private int[,] lastGeneration;
        private readonly int _tileSize = 16;
		public LifeGrid(int width,int height)
		{
			_grid = new Grid(width,height);            
            _grid.TileCell.CopyGrid(lastGeneration, _grid.Scales);                    
		}
        public LifeGrid(int[,] initialState)
        {
            _grid = new Grid(initialState);
            lastGeneration = new int[_grid.Scales.X, _grid.Scales.Y];
            _grid.TileCell.CopyGrid(lastGeneration, _grid.Scales);                        
        }
        public LifeGrid(string stateFilepath)
        {
            _grid = new Grid(LifeReader.GetGridFromPlaintextFile(stateFilepath));
            lastGeneration = _grid.TileCell;
            _grid.TileCell.CopyGrid(lastGeneration, _grid.Scales);                        
        }
		//Change state of grid here, leave nextGrid outside of this class
		public void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.DrawGrid(_grid, _tileSize, Color.White);                      
		}        
        public void Update(GameTime gameTime)
        {
            updateTimer += gameTime.ElapsedGameTime;
            if (updateTimer.TotalMilliseconds > 1000f / Game1.UPS)
            { 
                updateTimer = TimeSpan.Zero;
                _grid.CreateNextGeneration();
                _grid.TileCell.CopyGrid(lastGeneration, _grid.Scales);
                //for (int i = 0; i < _grid.Scales.X; i++){
                //    for (int j = 0; j < _grid.Scales.Y; j++){
                //        lastGeneration[i, j] = _grid.TileCell[i, j];
                //    }
                //}
            }
		}
		//TODO:Create or Add package for generic color buffer factory method		
	}
}
