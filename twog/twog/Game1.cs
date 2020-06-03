using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace twog
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Engine
    {
        private static int width = 320;
        private static int height = 180;
        private static int windowWidth = 640;
        private static int windowHeight = 360;

        public Game1() : base(width, height, windowWidth, windowHeight, "twog", false)
        {

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            GFX graphicsInit = GFX.Instance;
            TestScene testScene = new TestScene();
            Scene = testScene;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }

    //GraphicsDeviceManager graphics;
    //SpriteBatch spriteBatch;

    //int virtualWidth = 320;
    //int virtualHeight = 180;
    //int actualWidth = 640;
    //int actualHeight = 360;

    //RenderTarget2D renderTarget;

    //Texture2D player;
    //List<Texture2D> tileList = new List<Texture2D>();

    //List<int> mapTiles;

    //Camera camera;

    //public Game1()
    //{
    //    graphics = new GraphicsDeviceManager(this);
    //    Content.RootDirectory = "Content";
    //}

    //protected override void Initialize()
    //{   
    //    base.Initialize();
    //    renderTarget = new RenderTarget2D(GraphicsDevice, virtualWidth, virtualHeight);

    //    camera = new Camera(GraphicsDevice.Viewport);

    //    graphics.PreferredBackBufferWidth = actualWidth;  // set this value to the desired width of your window
    //    graphics.PreferredBackBufferHeight = actualHeight;   // set this value to the desired height of your window
    //    graphics.ApplyChanges();
    //}

    //protected override void LoadContent()
    //{
    //    // Create a new SpriteBatch, which can be used to draw textures.
    //    spriteBatch = new SpriteBatch(GraphicsDevice);

    //    player = Content.Load<Texture2D>("Assets/player-idle");

    //    string[] tileNames  = { "tile-1", "tile-2", "tile-3", "tile-4"};
    //    foreach (var s in tileNames)
    //    {
    //        tileList.Add(Content.Load<Texture2D>("Assets/Tiles/" + s));
    //    }

    //    Map map = new Map("Maps/test_map.json");
    //    mapTiles = map.mapjson.Tiles;
    //}

    //protected override void UnloadContent()
    //{
    //}

    //protected override void Update(GameTime gameTime)
    //{
    //    var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    //    var keyboardState = Keyboard.GetState();

    //    var camSpeed = 2;

    //    // movement
    //    if (keyboardState.IsKeyDown(Keys.Up))
    //        camera.Position -= new Vector2(0, camSpeed);

    //    if (keyboardState.IsKeyDown(Keys.Down))
    //        camera.Position += new Vector2(0, camSpeed);

    //    if (keyboardState.IsKeyDown(Keys.Left))
    //        camera.Position -= new Vector2(camSpeed, 0);

    //    if (keyboardState.IsKeyDown(Keys.Right))
    //        camera.Position += new Vector2(camSpeed, 0);

    //    base.Update(gameTime);
    //}

    //protected override void Draw(GameTime gameTime)
    //{
    //    GraphicsDevice.SetRenderTarget(renderTarget);
    //    GraphicsDevice.Clear(Color.Black);
    //    Matrix viewMatrix = camera.GetViewMatrix();
    //    spriteBatch.Begin(transformMatrix: viewMatrix);

    //    var numTiles = 20;
    //    for (int i = 0; i < numTiles; ++i)
    //    {
    //        for (int j = 0; j < numTiles; ++j)
    //        {
    //            int lin_idx = j * numTiles + i;
    //            spriteBatch.Draw(tileList[mapTiles[lin_idx]], new Vector2(16 * i, 16 * j), Color.White);
    //        }
    //    }

    //    spriteBatch.Draw(player, new Vector2(16 * 10, 16 * 6), Color.White);
    //    spriteBatch.End();

    //    GraphicsDevice.SetRenderTarget(null);

    //    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
    //    spriteBatch.Draw(renderTarget, new Rectangle(0, 0, actualWidth, actualHeight), Color.White);
    //    spriteBatch.End();

    //    base.Draw(gameTime);
    //}
}

