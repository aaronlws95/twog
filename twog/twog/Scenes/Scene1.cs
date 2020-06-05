using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace twog
{
    class Scene1 : Scene
    {

        private PlayerCamera camera;
        private Player player;
        private Background background;
        private PlayerCameraStaticBound camDebug;
        private PlayerInteractor playerInteractor;
        private ObjectMap objectMap;
        private Door door;
        EverythingRenderer er;

        public Scene1()
        {
            Name = "Scene1";
        }

        public override void Begin()
        {
            base.Begin();

            background = new Background("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/bg_map_scene1");
            Add(background);

            objectMap = new ObjectMap("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/object_map_scene1");
            Add(objectMap);

            door = new Door(new Vector2(5 * 16, 11 * 16), 16, 16, new Scene0());
            Add(door);

            player = Game1.Player;
            Add(player);

            playerInteractor = new PlayerInteractor(new Vector2(player.Position.X, player.Position.Y + 16));
            Add(playerInteractor);

            er = new EverythingRenderer();
            camera = new PlayerCamera(640, 360, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            camera.Position = new Vector2(player.Position.X - Engine.Width / 2, player.Position.Y - Engine.Height / 2);
            er.Camera = camera;
            Add(er);

            Vector2 newScreenCenter = camera.ScreenToCamera(new Vector2(Engine.Width / 2, Engine.Height / 2));
            camDebug = new PlayerCameraStaticBound(newScreenCenter, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            Add(camDebug);
        }

        public override void Update()
        {
            base.Update();
            int move_x = MInput.Keyboard.AxisCheck(Keys.Left, Keys.Right);
            int move_y = MInput.Keyboard.AxisCheck(Keys.Up, Keys.Down);

            player.Move(new Vector2(move_x, move_y), new Vector2(0, 0), new Vector2(background.GridWidth, background.GridHeight));
            playerInteractor.Move(new Vector2(player.Position.X, player.Position.Y), new Vector2(move_x, move_y));
            camera.Move(new Vector2(move_x, move_y), player.Position);
            Vector2 newScreenCenter = camera.ScreenToCamera(new Vector2(Engine.Width / 2, Engine.Height / 2));
            camDebug.Update(newScreenCenter);
        }
    }
}
