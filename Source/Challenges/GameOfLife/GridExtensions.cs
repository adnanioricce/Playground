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
            var (scaleX, scaleY) = ((int)scale.X, (int)scale.Y);
            int count = 0;
            // Check cell on the right.            
            if (grid[ScalePosition(x + 1, scaleX, scaleY, false), y] == 1)
                count++;

            // Check cell on the bottomw right.
            //if (x != Width && y != Height)            
            if (grid[ScalePosition(x + 1, scaleX, scaleY, false), ScalePosition(y + 1, scaleX, scaleY, true)] == 1)
                count++;

            // Check cell on the bottom.            
            if (grid[ScalePosition(x + 1, scaleX, scaleY, false), ScalePosition(y - 1, scaleX, scaleY, true)] == 1)
                count++;

            // Check cell on the bottom left.                        
            if (grid[ScalePosition(x - 1, scaleX, scaleY, false), ScalePosition(y + 1, scaleX, scaleY, true)] == 1)
                count++;

            // Check cell on the left.            
            if (grid[ScalePosition(x - 1, scaleX, scaleY, false), y] == 1)
                count++;
            // Check cell on the top left.            
            if (grid[ScalePosition(x - 1, scaleX, scaleY, false), ScalePosition(y - 1, scaleX, scaleY, true)] == 1)
                count++;

            // Check cell on the top.            
            if (grid[x, ScalePosition(y - 1, scaleX, scaleY, true)] == 1)
                count++;
            if (grid[x, ScalePosition(y + 1, scaleX, scaleY, true)] == 1)
                count++;
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
        private static int ScalePosition(int value,int scaleX,int scaleY, bool isYaxis)
        {
            if (value < 0 && !isYaxis) return scaleX - 1;
            if (value < 0 && isYaxis) return scaleY - 1;
            if (value == scaleX || value == scaleY) return 0;
            return value;            
        }
    }
}
