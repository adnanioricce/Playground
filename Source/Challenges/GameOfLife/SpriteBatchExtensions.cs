using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class SpriteBatchExtensions
    {
        public static void DrawGrid(this SpriteBatch spriteBatch, Grid grid, int tileSize, Color color)
        {
            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    int textureId = grid.TileCell[x, y];
                    if (textureId != 0)
                    {
                        var texturePosition = new Vector2(x * tileSize, y * tileSize) + Vector2.Zero;
                        spriteBatch.Draw(Game1.whiteTexture, texturePosition, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    }
                }
            }
        }
        public static void DrawGridLines(this SpriteBatch spriteBatch, (int Width, int Height) gridSize, (int X, int Y) centers, int tileSize, Texture2D pointTexture)
        {
            for (float x = -gridSize.Width; x < gridSize.Width; ++x)
            {
                Rectangle rectangle = new Rectangle((int)(centers.X + x * tileSize), 0, 1, gridSize.Height);
                spriteBatch.Draw(pointTexture, rectangle, Color.White);
            }
            for (float y = -gridSize.Height; y < gridSize.Height; ++y)
            {
                Rectangle rectangle = new Rectangle(0, (int)(centers.Y + y * tileSize), gridSize.Width, 1);
                spriteBatch.Draw(pointTexture, rectangle, Color.White);
            }
        }
    }
}
