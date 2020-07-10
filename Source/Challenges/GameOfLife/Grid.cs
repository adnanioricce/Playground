using Microsoft.Xna.Framework;
using System;
using System.Drawing;

namespace GameOfLife
{    
    public class Grid
    {
        public readonly int ScaleX;
        public readonly int ScaleY;
        public int[,] TileCell { get { return _tileCell;}  }
        private int[,] _tileCell;
        private int[,] _nextState;
        public int Width { get; }
        public int Height { get; }        
        public Grid(int width,int height)
        {
            Width = width;
            Height = height;
	        ScaleX = width / 16;
            ScaleY = height / 16; 
            _tileCell = new int[width, height];            
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    _tileCell[i,j] = GridHelper.GetRandomState();
            _nextState = new int[width, height];
            _nextState.Initialize();
        }
	    public Grid(int[,] initialState)
	    {            
            Width = initialState.GetUpperBound(0);
            Height = initialState.GetUpperBound(1);            
            ScaleX = initialState.GetLength(0);
            ScaleY = initialState.GetLength(1);
            _nextState = new int[ScaleX, ScaleY];
            _nextState.Initialize();
            _tileCell = initialState;            
            
        }
	    //TODO:Method to swap cells
        //its better to find cells and store your positions  
        public void CreateNextGeneration(int[,] lastState)
        {
            var resultGrid = new int[ScaleX, ScaleY];
            for (int x = 0; x < ScaleX; ++x)
                for (int y = 0; y < ScaleY; ++y){
                    
                    int neighbors = CountNeighboards(_tileCell, x, y, ScaleX);
                    
                    var cellState = _tileCell[x, y];
                    int result = 0;
                    //subpopulation
                    if (cellState == 1 && neighbors <= 1)
                        result = 0;
                    //Alive
                    if(cellState == 1 && (neighbors == 2 || neighbors == 3))
                        result = 1;
                    //overpopulation
                    if (cellState == 1 && neighbors > 3)
                        result = 0;
                    //Reproduction
                    if (cellState == 0 && neighbors == 3)
                        result = 1;
                    resultGrid[x, y] = result;
                }
            for (int i = 0; i < ScaleX; i++){
                for (int j = 0; j < ScaleY; j++){
                    _tileCell[i, j] = resultGrid[i, j];
                }
            }            
        }
	    public void SetNextGeneration(int[,] lastGrid,int[,] nextGrid)
	    {                        
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; j++) 
                {
                    nextGrid[i, j] = lastGrid[i, j];                    
                }
	    }
        private int CountNeighboards(int[,] grid,int x,int y,int _scale)
        {                        
            int count = 0;
            // Check cell on the right.            
            if (grid[ScalePosition(x + 1,false), y] == 1)
                count++;

            // Check cell on the bottomw right.
            //if (x != Width && y != Height)            
            if (grid[ScalePosition(x + 1,false), ScalePosition(y + 1,true)] == 1)
                count++;

            // Check cell on the bottom.            
            if (grid[ScalePosition(x + 1,false), ScalePosition(y - 1,true)] == 1)
                count++;

            // Check cell on the bottom left.                        
            if (grid[ScalePosition(x - 1,false), ScalePosition(y + 1,true)] == 1)
                count++;

            // Check cell on the left.            
            if (grid[ScalePosition(x - 1,false),y] == 1)
                count++;
            // Check cell on the top left.            
            if (grid[ScalePosition(x - 1,false), ScalePosition(y - 1,true)] == 1)
                count++;

            // Check cell on the top.            
            if (grid[x, ScalePosition(y - 1,true)] == 1)
                count++;
            if (grid[x, ScalePosition(y + 1,true)] == 1)
                count++;            
            return count;            
        }
        private int ScalePosition(int value,bool isYaxis)
        {
            if (value < 0 && !isYaxis) return ScaleX - 1;
            if (value < 0 && isYaxis) return ScaleY - 1;
            if (value == ScaleX || value == ScaleY) return 0;            
            return value;
            //return value < 0 || value == Scale ? Math.Abs(value) % Scale : value;
        }
    }
}
