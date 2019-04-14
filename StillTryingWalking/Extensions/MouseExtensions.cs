
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StillTryingWalking.Extensions
{
    public static class MouseExtensions
    {
        public static Vector2 GetMouseVector2()
        {
            return new Vector2(Mouse.GetState().X,Mouse.GetState().Y);
        }
    }
}