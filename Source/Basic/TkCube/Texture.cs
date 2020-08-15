﻿using OpenTK.Graphics;
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
        public static Texture LoadTexture(string textureFilepath)
        {                        
            var image = Image.Load<Rgba32>(textureFilepath);
            var pixels = image.GetPixelsBytesFromImage();
            return LoadTexture(pixels, (image.Width, image.Height));            
        }
        public static Texture LoadTexture(string textureFilepath, TextureTarget target, PixelInternalFormat pixelInternalFormat, PixelFormat pixelFormat, PixelType pixelType)
        {
            var image = Image.Load<Rgba32>(textureFilepath);
            return LoadTexture(image.GetPixelsBytesFromImage(), (image.Width, image.Height), target, pixelInternalFormat, pixelFormat, pixelType);
        }
        public static Texture LoadTexture(byte[] pixels,(int Width,int Height) textureSize , TextureTarget target = TextureTarget.Texture2D, PixelInternalFormat pixelInternalFormat = PixelInternalFormat.Rgba, PixelFormat pixelFormat = PixelFormat.Rgba, PixelType pixelType = PixelType.Byte)
        {
            var textureId = GL.GenTexture();
            GL.BindTexture(target, textureId);
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(target, 0, pixelInternalFormat, textureSize.Width, textureSize.Height, 0, pixelFormat, pixelType, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            return new Texture(textureId, textureSize.Width, textureSize.Height, pixels);
        }
        public void Bind(int slot = 0)
        {
            var unit = TextureUnit.Texture0 + slot;
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, this.Id);
            Logger.Log(nameof(Texture), nameof(Bind));
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
