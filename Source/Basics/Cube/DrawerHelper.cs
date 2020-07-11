using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Cube
{
    public class DrawerHelper
    {
        public static VertexPositionNormalTexture[] MakeCube()
        {
            var vertexes = new VertexPositionNormalTexture[36];
            var Texcoords = new Vector2(0f,0f);
            var face = new Vector3[6];
            // TopLeft
            face[0] = new Vector3(-1f,1f,0.0f);
            // BottomLeft
            face[1] = new Vector3(-1f,-1f,0.0f);
            // TopRight
            face[2] = new Vector3(1f,1f,0.0f);
            // BottomLeft 
            face[3] = new Vector3(1f,-1f,0.0f);
            // BottomRight 
            face[4] = new Vector3(1f,-1f,0.0f);
            // TopRight
            face[5] = new Vector3(1f,1f,0.0f);
            // front face 
            for (int i = 0; i <= 2; ++i)
            {
                vertexes[i] = new VertexPositionNormalTexture(face[i] + Vector3.UnitZ,Vector3.UnitZ,Texcoords);
                vertexes[i + 3] = new VertexPositionNormalTexture(face[i + 3] + Vector3.UnitZ,Vector3.UnitZ,Texcoords);                
            }
            //Back face
            for (int i = 0; i <= 2; ++i)
            {
                vertexes[i + 6] = new VertexPositionNormalTexture(face[2 - i] - Vector3.UnitZ,-Vector3.UnitZ,Texcoords);
                vertexes[i + 6 + 3] = new VertexPositionNormalTexture(face[5 - i] - Vector3.UnitZ,-Vector3.UnitZ,Texcoords);                
            }
            // left face
            Matrix RotY90 = Matrix.CreateRotationY(-(float)Math.PI / 2f);
            for (int i = 0; i <= 2; ++i)
            {
                vertexes[i + 12] = new VertexPositionNormalTexture(Vector3.Transform(face[i],RotY90) - Vector3.UnitX,-Vector3.UnitX,Texcoords);
                vertexes[i + 12 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[i + 3],RotY90) - Vector3.UnitX,-Vector3.UnitX,Texcoords);                
            }
            // Right face
            for (int i = 0; i <= 2; ++i)
            {
                vertexes[i + 18] = new VertexPositionNormalTexture(Vector3.Transform(face[2 - i],RotY90) - Vector3.UnitX,Vector3.UnitX,Texcoords);
                vertexes[i + 18 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[5 - i],RotY90) - Vector3.UnitX,Vector3.UnitX,Texcoords);                
            }
            // Top face
            var RotX90 = Matrix.CreateRotationX(-(float)Math.PI / 2f);
            for (int i = 0; i <= 2; ++i)
            {
                vertexes[i + 24] = new VertexPositionNormalTexture(Vector3.Transform(face[i],RotX90) + Vector3.UnitY,Vector3.UnitY,Texcoords);
                vertexes[i + 24 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[i + 3],RotX90) + Vector3.UnitY,Vector3.UnitY,Texcoords);
            }
            // Bottom face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 30] = new VertexPositionNormalTexture(Vector3.Transform(face[2 - i], RotX90) - Vector3.UnitY, -Vector3.UnitY, Texcoords);
                vertexes[i + 33] = new VertexPositionNormalTexture(Vector3.Transform(face[5 - i], RotX90) - Vector3.UnitY, -Vector3.UnitY, Texcoords);
            }
            return vertexes;
        }
    }    
}