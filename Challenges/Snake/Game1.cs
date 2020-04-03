using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        private Vector2 screen;
        private SnakeBody snake;
        private Food food;        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureHelper.SetGraphicsDevice(GraphicsDevice);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            this.screen = new Vector2(graphics.PreferredBackBufferWidth/2,graphics.PreferredBackBufferHeight/2);            
            snake = new SnakeBody(new Vector2(this.screen.X,this.screen.Y),screen,GraphicsDevice);
            food = new Food();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            snake.Update();
            food.Update(snake);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            //spriteBatch.Draw(snake._texture, snake._position, Color.White);
            food.Draw(spriteBatch);
            snake.Draw(spriteBatch);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
