using System;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace GameOfLife
{
    public static class LifeReader
    {
        //private static readonly string RootFolder = Directory.GetParent(Environment.CurrentDirectory);
        private static readonly Vector2 MinSize = new Vector2(320, 320); 
        public static int[,] GetGridFromPlaintextFile(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                throw new ArgumentNullException("filepath is null or empty");
            if (!File.Exists(filepath))
                throw new FileNotFoundException("file don't exist");            
            //TODO:Check if is valid content            
            string[] lines = File.ReadAllLines(filepath);
            string line = lines[0];
            int[,] grid = new int[32,32];
            grid.Initialize();            
            for (int i = 0; i < lines.Length; ++i)
            {                
                for (int j = 0; j < line.Length; ++j)
                {
                    grid[i, j] = int.Parse(line[j].ToString());
                }
                line = lines[i];
            }            
            return grid;
        }
        public static int[,] GetGridFromPlainTextCellFile(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                throw new ArgumentNullException("filepath is null or empty");
            if (!File.Exists(filepath))
                throw new FileNotFoundException("file don't exist");

            string[] lines = File.ReadAllLines(filepath);
            string line = lines[0];
            int[,] grid = new int[lines.Length, line.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (grid[i, j] == '.')
                        grid[i, j] = 0;
                    if (grid[i, j] == 'O')
                        grid[i, j] = 1;
                }
                line = lines[i];
            }
            return grid;
        }
    }
}
