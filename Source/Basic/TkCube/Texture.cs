using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;

namespace TkCube
{
    public class Texture : IDisposable
    {
        public int Id { get; protected set; }        
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public byte[] Data { get; protected set; }
        protected Texture(int id,int width,int height,byte[] data)
        {
            Id = id;
            Width = width;
            Height = height;
            Data = data;
        }
        public static Texture LoadTexture(string textureFilepath,TextureTarget target = TextureTarget.Texture2D, PixelInternalFormat pixelInternalFormat = PixelInternalFormat.Rgba, PixelFormat pixelFormat = PixelFormat.Rgba,PixelType pixelType = PixelType.Byte)
        {            
            
            var image = Image.Load<Rgba32>(textureFilepath);
            image.Mutate(x => x.Flip(FlipMode.Vertical));
            var pixels = new List<byte>();
            for (int i = 0; i < image.Width; ++i){
                for (int j = 0; j < image.Height; ++j){
                    //pixels.Add(new Color4(image[i, j].R, image[i, j].G, image[i, j].B, image[i, j].A));                    
                    pixels.Add(image[i, j].R);
                    pixels.Add(image[i, j].G);
                    pixels.Add(image[i, j].B);
                    pixels.Add(image[i, j].A);
                }
            }
            var textureId = GL.GenTexture();
            GL.BindTexture(target, textureId);
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(target, 0, pixelInternalFormat, image.Width, image.Height, 0, pixelFormat, pixelType, pixels.ToArray());
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            return new Texture(textureId, image.Width, image.Height,pixels.ToArray());
        }
        public void Bind(int slot = 0)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + slot);
            GL.BindTexture(TextureTarget.Texture2D, this.Id);            
        }
        public void UnBind()
        {            
            //TODO:Add error handling
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(this.Id);
        }
    }
}
