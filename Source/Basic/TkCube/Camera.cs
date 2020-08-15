using OpenTK;

namespace TkCube
{
    public class Camera
    {
        public Matrix4 Model { get; set; }
        public Matrix4 View { get; set; }
        public Matrix4 Projection { get; set; }        
        protected Camera(int width, int height)
        {
            Model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));
            View = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);
        }
        public static Camera CreateCamera(int width,int height)
        {
            return new Camera(width, height);
        }
    }
}
