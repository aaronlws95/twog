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

        // 4:3
        //private static int windowWidth = 640;
        //private static int windowHeight = 480;

        // 16:9
        //private static int windowWidth = 640;
        //private static int windowHeight = 360;
        //private static int windowWidth = 1024;
        //private static int windowHeight = 576;
        //private static int windowWidth = 1280;
        //private static int windowHeight = 720;
        private static int windowWidth = 1600;
        private static int windowHeight = 900;
        //private static int windowWidth = 1920;
        //private static int windowHeight = 1080;

        public static List<Scene> Scenes;
        public static Player Player;

        public static NarBox NarBox { get; private set; }
        public static Dictionary<string, NPC> NPCDict { get; private set; }
        public static CoDialogue GameDialogue { get; set; }

        public Game1() : base(width, height, windowWidth, windowHeight, "twog", false)
        {
            Window.AllowUserResizing = false;
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
            Commands.Enabled = false;

            int initGAccess = GAccess.SolidTag.ID;
            GFX graphicsInit = GFX.Instance;
            DialogueData dialogueInit = DialogueData.Instance;
            NarBox = new NarBox();

            // create NPCs
            NPCDict = new Dictionary<string, NPC>();
            NPC aisya = new NPC("Aisya");
            aisya.Add(new CoDialogue("SCENE1001AISYA"));
            NPCDict.Add("Aisya", aisya);

            // initialize player and scene
            Player = new Player(new Vector2(10 * 16, 10 * 16));
            Scene = new MainMenu();

            // intro dialogue
            GameDialogue = new CoDialogue("SCENE0001INTRO");
            //GameDialogue.StartDialogue();
        }

        protected override void OnSceneTransition(Scene from, Scene to)
        {
            base.OnSceneTransition(from, to);

            // determine player position from scene transition
            if (from != null && to != null)
            {
                if (from.Name == "Scene0" && to.Name == "Scene1")
                {
                    Player.Position = new Vector2(6 * 16 - 8, 11 * 16 - 8);
                }

                else if (from.Name == "Scene1" && to.Name == "Scene0")
                {
                    Player.Position = new Vector2(9 * 16 - 8, 8 * 16 - 8);
                }
            }
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
            //Debug Console
            if (NarBox.Open)
                NarBox.UpdateOpen();
            //else
            //    NarBox.UpdateClosed();

            if (GameDialogue.Activated)
                GameDialogue.Update();
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

