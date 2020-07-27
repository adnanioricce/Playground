using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public static class GridExtensions
    {
        public static int CountNeighboards(this int[,] grid,(int X,int Y) position,(int X,int Y) scale)
        {
            var (x, y) = (position.X, position.Y);            
            int count = 0;
            // Check borders of the cell      
            for (int i = -1; i <= 1; ++i)
            {                
                for (int j = -1; j <= 1; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    var (xPos, yPos) = (RoundCellPosition(x + i,scale, false), RoundCellPosition(y + j, scale, true));
                    if (grid[xPos, yPos] == 1)
                        count++;
                }
            }            
            return count;
        }
        public static int[,] CreateGridCopy(this int[,] lastGrid)
        {
            var (width,height) = (lastGrid.GetLength(0), lastGrid.GetLength(1));
            var nextGrid = new int[width,height];
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; j++){
                    nextGrid[i, j] = lastGrid[i, j];
                }
            return nextGrid;
        }
        public static void CopyGrid(this int[,] nextGrid,int[,] frontGrid,(int X,int Y) scales)
        {
            var (scaleX, scaleY) = ((int)scales.X, (int)scales.Y);
            for (int i = 0; i < scaleX; i++){
                for (int j = 0; j < scaleY; j++){
                    frontGrid[i, j] = nextGrid[i, j];
                }
            }            
        }
        public static void SetCell(this int[,] grid,(float X,float Y) position,bool delete)
        {
            grid[(int)Math.Abs(position.X), (int)Math.Abs(position.Y)] = delete ? 0 : 1;
        }        
        private static int RoundCellPosition(int value,(int X,int Y) scale, bool isYaxis)
        {
            if (value < 0 && !isYaxis) return scale.X - 1;
            if (value < 0 && isYaxis) return scale.Y - 1;
            if (value == scale.X || value == scale.Y) return 0;
            return value;            
        }
    }
}
