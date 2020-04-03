using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Starfield
{
    public class Star
    {
        public VertexPositionColor StartPosition;
        public VertexPositionColor EndPosition;        
        public Star()
        {            
            GetInitialPositon();
        }
        private void GetInitialPositon()
        {
            var yseed = (int)(Utils.Seeder.NextDouble() * 100);
            var xseed = (int)(Utils.Seeder.NextDouble() * 100);
            StartPosition = new VertexPositionColor(new Vector3(xseed, yseed, 0), Color.White);
            EndPosition = new VertexPositionColor(new Vector3(xseed, yseed, 0), Color.White);
        }        
        public void Update()
        {
            var seed = (int)(Utils.Seeder.NextDouble() * 100);            
            if (StartPosition.Position.Z <= 200)
            {
                StartPosition.Position.Z += 0.2f;
                EndPosition.Position.Z += 1f;
            }
            else if (StartPosition.Position.Z >= 50)
            {
                StartPosition.Position.Z += 0.9f;
                EndPosition.Position.Z += 2f;
            }
            else if (EndPosition.Position.Z >= 150)
            {
                StartPosition.Position.Z += 1f;
                EndPosition.Position.Z += 0.2f;
            }
            else if (EndPosition.Position.Z >= 200)
            {
                StartPosition.Position.Z += 1.6f;
                EndPosition.Position.Z += 0.4f;
            }
            else
            {
                StartPosition.Position.Z = 0;
                EndPosition.Position.Z = 1;
            }
        }        
    }
}
