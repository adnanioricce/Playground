using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cube
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        Matrix projection;
        VertexPositionNormalTexture[] cube;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {                        
            base.Initialize();
            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.AmbientLightColor = Vector3.One;
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.DirectionalLight0.DiffuseColor = Vector3.One;
            basicEffect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);            
            basicEffect.LightingEnabled = true;
            projection = Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f,(float)this.graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight,1f,10f);
            basicEffect.Projection = projection;
            basicEffect.View = Matrix.CreateTranslation(0f,0f,-10f);            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            cube = DrawerHelper.MakeCube();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList,cube,0,12);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
