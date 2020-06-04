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
        private static int windowWidth = 1280;
        private static int windowHeight = 720;

        public static NarBox NarBox { get; private set; }

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

            // use tilde to Open
            //Commands.Open = true;

            int initGAccess = GAccess.HouseTag.ID;
            initGAccess = GAccess.DoorTag.ID;
            GFX graphicsInit = GFX.Instance;

            NarBox = new NarBox();

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
            NarBox.UpdateClosed();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (NarBox.Open)
                NarBox.Render();
        }

    }
}

