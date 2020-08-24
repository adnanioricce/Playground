using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace GameOfLife
{    
    public class Grid
    {
        public readonly (int X, int Y) Scales;        
        public int[,] TileCell { get { return _tileCell;}  }
        private int[,] _tileCell;
        private int[,] _nextState;
        public int Width { get; }
        public int Height { get; }        
        public Grid(int width,int height)
        {
            (Width, Height) = (width, height);
            (Scales.X, Scales.Y) = (width / 16, height / 16);            
            _tileCell = new int[width, height];            
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    _tileCell[i,j] = GridHelper.GetRandomState();
            _nextState = new int[width, height];
            _nextState.Initialize();
        }
	    public Grid(int[,] initialState)
	    {            
            (Width,Height) = (initialState.GetUpperBound(0),initialState.GetUpperBound(1));             
            (Scales.X,Scales.Y) = (initialState.GetLength(0),initialState.GetLength(1));             
            _nextState = new int[Scales.X, Scales.Y];
            _nextState.Initialize();
            _tileCell = initialState;            
            
        }
	    //TODO:Method to swap cells
        //its better to find cells and store your positions  
        public void CreateNextGeneration()
        {            
            GenerationEvolve(EvolveCell, _tileCell,Scales).CopyGrid(_tileCell, Scales);                           
        }
	    public void SetNextGeneration(int[,] lastGrid)
	    {
            lastGrid.CopyGrid(_tileCell, (Scales.X, Scales.Y));                  
	    }        
        protected int[,] GenerationEvolve(Func<int[,], (int X,int Y), (int X,int Y), int> evolveMethod, int[,] lastGeneration,(int X,int Y) scales)
        {            
            var resultGrid = new int[scales.X, scales.Y];            
            for (int x = 0; x < Scales.X; ++x) { 
                for (int y = 0; y < Scales.Y; ++y) {
                    resultGrid[x, y] = evolveMethod(lastGeneration, (x,y), scales);                   
                } 
            }                    
            return resultGrid;
        }
        protected int EvolveCell(int[,] grid,(int X,int Y) position, (int X,int Y) scales)
        {            
            int neighbors = grid.CountNeighboards(position, scales);
            var cellState = grid[position.X, position.Y];
            int result = IsCellAlive(cellState,neighbors);            
            return result;
        }
        protected int IsCellAlive(int cellState,int neighbors)
        {
            int result = 0;            
            //Subpopulation
            if (cellState == 1 && neighbors <= 1)
                result = 0;
            //Alive
            if (cellState == 1 && (neighbors == 2 || neighbors == 3))
                result = 1;
            //overpopulation
            if (cellState == 1 && neighbors > 3)
                result = 0;
            //Reproduction
            if (cellState == 0 && neighbors == 3)
                result = 1;
            return result;
        }
    }
}
