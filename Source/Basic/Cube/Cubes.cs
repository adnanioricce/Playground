using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Cube
{
    public static class Cubes
    {
        public static VertexPositionNormalTexture[] CreateSimpleCubeVertexes()
        {
            var vertexes = new VertexPositionNormalTexture[36];
            var texcoords = new Vector2(0f,0f);
            var face = new Vector3[6];
            //TopLeft
            face[0] = new Vector3(-1f, 1f, 0.0f);
            //BottomLeft
            face[1] = new Vector3(-1f, -1f, 0.0f);
            //TopRight
            face[2] = new Vector3(1f, 1f, 0.0f);
            //BottomLeft
            face[3] = new Vector3(-1f, -1f, 0.0f);
            //BottomRight
            face[4] = new Vector3(1f, -1f, 0.0f);
            //TopRight
            face[5] = new Vector3(1f, 1f, 0.0f);
            for (int i = 0; i <= 2; ++i){
                vertexes[i] = new VertexPositionNormalTexture(face[i] + Vector3.UnitZ, Vector3.UnitZ, texcoords);
                vertexes[i + 3] = new VertexPositionNormalTexture(face[i + 3] + Vector3.UnitZ, Vector3.UnitZ, texcoords);
            }
            //Back face
            for (int i = 0; i <= 2; ++i){
                vertexes[i + 6] = new VertexPositionNormalTexture(face[2 - i] - Vector3.UnitZ, -Vector3.UnitZ, texcoords);
                vertexes[i + 6 + 3] = new VertexPositionNormalTexture(face[5 - i] - Vector3.UnitZ, -Vector3.UnitZ, texcoords);
            }
            //left face
            Matrix RotY90 = Matrix.CreateRotationY(-(float)Math.PI / 2f);
            for (int i = 0; i <= 2; ++i){
                vertexes[i + 12] = new VertexPositionNormalTexture(Vector3.Transform(face[i], RotY90) - Vector3.UnitX,-Vector3.UnitX, texcoords);
                vertexes[i + 12 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[i + 3], RotY90) - Vector3.UnitX, -Vector3.UnitX, texcoords);
            }
            //Right face
            for (int i = 0; i <= 2; ++i){
                vertexes[i + 18] = new VertexPositionNormalTexture(Vector3.Transform(face[2 - i], RotY90) - Vector3.UnitX, Vector3.UnitX, texcoords);
                vertexes[i + 18 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[5 - i], RotY90) - Vector3.UnitX, Vector3.UnitX, texcoords);
            }

            //Top face
            Matrix RotX90 = Matrix.CreateRotationX(-(float)Math.PI / 2f);
            for (int i = 0; i <= 2; ++i){
                vertexes[i + 24] = new VertexPositionNormalTexture(Vector3.Transform(face[i], RotX90)+ Vector3.UnitY, Vector3.UnitY, texcoords);
                vertexes[i + 24 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[i + 3], RotX90)+ Vector3.UnitY, Vector3.UnitY, texcoords);
            }
            //Bottom face

            for (int i = 0; i <= 2; ++i){
                vertexes[i + 30] =new VertexPositionNormalTexture(Vector3.Transform(face[2 - i], RotX90) - Vector3.UnitY, -Vector3.UnitY, texcoords);
                vertexes[i + 30 + 3] =new VertexPositionNormalTexture(Vector3.Transform(face[5 - i], RotX90) - Vector3.UnitY, -Vector3.UnitY, texcoords);
            }
            return vertexes;
        }
        public static VertexPositionTexture[] GetSimpleCubeData()
        {
            var floorVerts = new VertexPositionTexture[8];
            //Front Bottom left
            floorVerts[0].Position = new Vector3(-1, -1, 0);
            //Front Up left
            floorVerts[1].Position = new Vector3(-1, 1, 0);
            //Front Up Right
            floorVerts[2].Position = new Vector3(1, 1, 0);
            //Front Bottom Right
            floorVerts[3].Position = new Vector3(1, -1, 0);
            //Back Bottom Left
            floorVerts[4].Position = new Vector3(-1, -1, -1);
            //Back Up Left
            floorVerts[5].Position = new Vector3(-1, 1, -1);
            //Back Bottom Right
            floorVerts[6].Position = new Vector3(1, -1, -1);
            //Back Up Right
            floorVerts[7].Position = new Vector3(1, 1, -1);            
            return floorVerts;
        }
    }
}