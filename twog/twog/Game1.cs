using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace twog
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player;

        int virtualWidth = 320;
        int virtualHeight = 180;
        int actualWidth = 640;
        int actualHeight = 360;

        RenderTarget2D renderTarget;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {   
            base.Initialize();
            renderTarget = new RenderTarget2D(GraphicsDevice, virtualWidth, virtualHeight);

            graphics.PreferredBackBufferWidth = actualWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = actualHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = Content.Load<Texture2D>("Assets/player-idle");
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(player, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(player, new Vector2(virtualWidth/2, virtualHeight/2), Color.White);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
           
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, actualWidth, actualHeight), Color.White);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
