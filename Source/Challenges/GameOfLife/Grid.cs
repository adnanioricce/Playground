using System;

namespace GameOfLife
{    
    public class Grid
    {
        public readonly int Scale;
        public int[,] TileCell { get { return _tileCell;} private set {_tileCell = value;} }
        private int[,] _tileCell;
        public int Width { get; }
        public int Height { get; }        
        public Grid(int width,int height)
        {
            Width = width;
            Height = height;
	        Scale = width / 10; 
            _tileCell = new int[width, height];            
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    _tileCell[i,j] = GridHelper.GetRandomState();
        }
	    public Grid(int[,] initialState)
	    {            
            Width = initialState.GetUpperBound(0);
            Height = initialState.GetUpperBound(1);
            var length = initialState.GetUpperBound(0);
            if (length <= 10)
                Scale = 1;
            if (length >= 10)
                Scale = length / 10;
            SetNextGeneration(initialState);
        }
	    //TODO:Method to swap cells
        //its better to find cells and store your positions  
        public void CreateNextGeneration(int[,] lastState)
        {            
            for (int x = 0; x <= Width; ++x)
                for (int y = 0; y <= Height; ++y)
                {
                    int neighbors = CountNeighboards(_tileCell, x, y, Scale);

                    var cellState = _tileCell[x, y];
                    int result = 0;
                    //subpopulation
                    if (cellState == 0 && neighbors < 2)
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
                    lastState[x, y] = result;
                }

            SetNextGeneration(lastState);
        }
	    public void SetNextGeneration(int[,] nextGrid)
	    {
            _tileCell = nextGrid;
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; j++)            
		            _tileCell[i,j] = nextGrid[i,j];
	    }
        private int CountNeighboards(int[,] grid,int x,int y,int _scale)
        {
            int count = 0;
            // Check cell on the right.
            if (x != Width)
                if (grid[x + 1, y] == 1)
                    count++;

            // Check cell on the bottomw right.
            if (x != Width && y != Height)
                if (grid[x + 1, y + 1] == 1)
                    count++;

            // Check cell on the bottom.
            if (y != Height )
                if (grid[x, y + 1] == 1)
                    count++;

            // Check cell on the bottom left.
            if (x != 0 && y != Height)
                if (grid[x - 1, y + 1] == 1)
                    count++;

            // Check cell on the left.
            if (x != 0)
                if (grid[x - 1, y] == 1)
                    count++;

            // Check cell on the top left.
            if (x != 0 && y != 0)
                if (grid[x - 1, y - 1] == 1)
                    count++;

            // Check cell on the top.
            if (y != 0)
                if (grid[x, y - 1] == 1)
                    count++;

            // Check cell on the top right.
            if (x != Width && y != 0)
                if (grid[x + 1, y - 1] == 1)
                    count++;

            return count;
            //return (
            //+ grid[GridHelper.Floor(x + 1, _scale), y]
            //+ grid[x, GridHelper.Floor(y - 1, _scale)]
            //+ grid[x, GridHelper.Floor(y + 1, _scale)]
            //+ grid[GridHelper.Floor(x - 1, _scale), GridHelper.Floor(y - 1, _scale)]
            //+ grid[GridHelper.Floor(x + 1, _scale), GridHelper.Floor(y + 1, _scale)]
            //+ grid[GridHelper.Floor(x - 1, _scale), GridHelper.Floor(y + 1, _scale)]
            //+ grid[GridHelper.Floor(x - 1, _scale), GridHelper.Floor(y + 1, _scale)]);
        }
    }
}
