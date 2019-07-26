namespace PerlinNoise
{
    public static class ColorExtensions
    {
        public static System.Drawing.Color ConvertToDrawingColor(this Microsoft.Xna.Framework.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        
    }
}
