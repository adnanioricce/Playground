using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Starfield
{
    public class StarField
    {
        private Star[] stars = new Star[(int)Settings.ScreenSpace.X];
        private BasicEffect _effect;
        public StarField()
        {
            Initialize();
        }
        public void SetEffect(BasicEffect effect)
        {
            _effect = effect;
        }
        private void Initialize()
        {
            for (int i = 0; i < stars.Length; i++){                
                stars[i] = new Star();
            }
        }
        public void Update()
        {
            for (int i = 0; i < stars.Length; i++){
                stars[i].Update();
            }
        }
        public void Draw(GraphicsDevice graphics)
        {
            var vertices = new VertexPositionColor[stars.Length * 2];
            int verticeCount = 0;
            for (int i = 0; i < stars.Length; i++)
            {
                vertices[verticeCount] = stars[i].StartPosition;
                verticeCount++;
                vertices[verticeCount] = stars[i].EndPosition;
                verticeCount++;
            }
            //var vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor),vertices.Length, BufferUsage.WriteOnly);
            //vertexBuffer.SetData<VertexPositionColor>(vertices);
            //var result = graphics.GraphicsDebug.TryDequeueMessage(out var message);
            //Console.WriteLine(message.Message);            
            for (int i = 0; i < stars.Length; ++i)
            {
                _effect.CurrentTechnique.Passes[0].Apply();
                graphics.DrawUserPrimitives(PrimitiveType.LineList,new[] { stars[i].StartPosition,stars[i].EndPosition }, 0, 1);
            }
            //var newResult = graphics.GraphicsDebug.TryDequeueMessage(out var newMessage);            
            //Console.WriteLine(newMessage.Message);
        }
    }
}
