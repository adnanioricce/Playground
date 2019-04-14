using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnotherTry
{
    public class Game1 : Game
    {
        Matrix viewMatrix;
        Matrix projectionMatrix;
        BasicEffect basicEffect;

        VertexDeclaration vertexDeclaration;
        VertexPositionNormalTexture[] pointList;
        VertexBuffer vertexBuffer;
        PrimitiveType typeToDraw = PrimitiveType.PointList;
        int points = 8;

        IndexBuffer lineListIndexBuffer;
        IndexBuffer lineStripIndexBuffer;
   
        GraphicsDeviceManager graphics;
        ContentManager content;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            InitializeTransform();
            if (true)
            {
                InitializeEffect();
                InitializePointList();
                InitializeLineList();
                InitializeLineStrip();
            }
        }

        private void InitializeTransform()
        {

            viewMatrix = Matrix.CreateLookAt(
            new Vector3(0, 0, 5),
            Vector3.Zero,
            Vector3.Up
            );

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),
                (float)graphics.GraphicsDevice.Viewport.Width /
                (float)graphics.GraphicsDevice.Viewport.Height,
                1.0f, 100.0f);

    }

    private void InitializeEffect()
    {

        vertexDeclaration = new VertexDeclaration(
            graphics.GraphicsDevice,
            VertexPositionNormalTexture.VertexElements);

        basicEffect = new BasicEffect(graphics.GraphicsDevice, null);
        basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);

        basicEffect.View = viewMatrix;
        basicEffect.Projection = projectionMatrix;

    }

    private void InitializePointList()
    {
        vertexDeclaration = new VertexDeclaration(
            graphics.GraphicsDevice,
            VertexPositionNormalTexture.VertexElements);

        double angle = MathHelper.TwoPi / points;

        pointList = new VertexPositionNormalTexture[points + 1];

        pointList[0] = new VertexPositionNormalTexture(
            Vector3.Zero, Vector3.Forward, Vector2.One);

        for (int i = 1; i <= points; i++)
        {
            pointListIdea = new VertexPositionNormalTexture(
                new Vector3(
                             (float)Math.Round(Math.Sin(angle * i), 4),
                             (float)Math.Round(Math.Cos(angle * i), 4),
                              0.0f),
                Vector3.Forward,
                new Vector2());
        }

        // Initialize the vertex buffer, allocating memory for each vertex
        vertexBuffer = new VertexBuffer(graphics.GraphicsDevice,
            VertexPositionNormalTexture.SizeInBytes * (pointList.Length),
            ResourceUsage.None,
            ResourceManagementMode.Automatic);

        // Set the vertex buffer data to the array of vertices
        vertexBuffer.SetData<VertexPositionNormalTexture>(pointList);

    }

    private void InitializeLineList()
    {
        // Initialize an array of indices of type short
        short[] lineListIndices = new short[(points * 2)];

        // Populate the array with references to indices in the vertex buffer
        for (int i = 0; i < points; i++)
        {
            lineListIndices[i * 2] = (short)(i + 1);
            lineListIndices[(i * 2) + 1] = (short)(i + 2);
        }

        lineListIndices[(points * 2) - 1] = 1;

        // Initialize the index buffer, allocating memory for each index
        lineListIndexBuffer = new IndexBuffer(
            graphics.GraphicsDevice,
            sizeof(short) * lineListIndices.Length,
            ResourceUsage.None,
            ResourceManagementMode.Automatic,
            IndexElementSize.SixteenBits
            );

        // Set the data in the index buffer to our array
        lineListIndexBuffer.SetData<short>(lineListIndices);

    }

    private void InitializeLineStrip()
    {
        // Initialize an array of indices of type short
        short[] lineStripIndices = new short[points + 1];

        // Populate the array with references to indices in the vertex buffer
        for (int i = 0; i < points; i++)
        {
            lineStripIndicesIdea = (short)(i + 1);
        }

        lineStripIndices[points] = 1;

        // Initialize the index buffer, allocating memory for each index
        lineStripIndexBuffer = new IndexBuffer(
            graphics.GraphicsDevice,
            sizeof(short) * lineStripIndices.Length,
            ResourceUsage.None,
            ResourceManagementMode.Automatic,
            IndexElementSize.SixteenBits
            );

        // Set the data in the index buffer to our array
        lineStripIndexBuffer.SetData<short>(lineStripIndices);

    }

    protected override void UnloadContent()
    {
        if (true)
        {
            content.Unload();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        // Allows the default game to exit on Xbox 360 and Windows
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            this.Exit();

        CheckInput();
        base.Update(gameTime);
    }

    private void CheckInput()
    {
        KeyboardState newState = Keyboard.GetState();

        if (newState.IsKeyDown(Keys.NumPad1))
        {
            typeToDraw = PrimitiveType.LineList;
        }
        else if (newState.IsKeyDown(Keys.NumPad2))
        {
            typeToDraw = PrimitiveType.LineStrip;
        }
    }

    protected override void Draw(GameTime gameTime)
    {

        graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

        graphics.GraphicsDevice.VertexDeclaration = vertexDeclaration;

        // effect is a compiled effect created and compiled elsewhere
        // in the application
        basicEffect.Begin();
        foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
        {
            pass.Begin();

            switch (typeToDraw)
            {
                case PrimitiveType.LineList:
                    DrawLineList();
                    break;
                case PrimitiveType.LineStrip:
                    DrawLineStrip();
                    break;
            }


            pass.End();
        }
        basicEffect.End();

        base.Draw(gameTime);

    }

    private void DrawLineList()
    {

        graphics.GraphicsDevice.Vertices[0].SetSource(
            vertexBuffer, 0,
            VertexPositionNormalTexture.SizeInBytes);

        graphics.GraphicsDevice.Indices = lineListIndexBuffer;

        graphics.GraphicsDevice.DrawIndexedPrimitives(
            PrimitiveType.LineList,
            0,  // vertex buffer offset to add to each element of the index buffer
            0,  // minimum vertex index
            16, // number of vertices
            0,  // first index element to read
            8); // number of primitives to draw

    }

    private void DrawLineStrip()
    {
        graphics.GraphicsDevice.Vertices[0].SetSource(
            vertexBuffer, 0,
            VertexPositionNormalTexture.SizeInBytes);

        graphics.GraphicsDevice.Indices = lineStripIndexBuffer;

        graphics.GraphicsDevice.DrawIndexedPrimitives(
            PrimitiveType.LineStrip,
            0, // vertex buffer offset to add to each element of the index buffer
            0, // minimum vertex index
            9, // number of vertices
            0, // first index element to read
            8);// number of primitives to draw

    }
    }
}
