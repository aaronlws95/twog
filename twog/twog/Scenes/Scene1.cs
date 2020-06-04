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
        PlayerCamera camera;
        Player player;
        Background background;
        PlayerCameraStaticBound camDebug;
        PlayerInteractor playerInteractor;

        public Scene1()
        {

        }

        public override void Begin()
        {
            base.Begin();

            player = new Player(new Vector2(Engine.Width / 2, Engine.Height / 2));
            Add(player);

            playerInteractor = new PlayerInteractor(new Vector2(player.Position.X, player.Position.Y + 16));
            Add(playerInteractor);

            background = new Background("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/test_map_1");
            Add(background);

            EverythingRenderer er = new EverythingRenderer();
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

            player.Move(new Vector2(move_x, move_y));
            playerInteractor.Move(new Vector2(player.Position.X, player.Position.Y), new Vector2(move_x, move_y));
            camera.Move(new Vector2(move_x, move_y), player.Position);
            Vector2 newScreenCenter = camera.ScreenToCamera(new Vector2(Engine.Width / 2, Engine.Height / 2));
            camDebug.Update(newScreenCenter);
        }
    }
}
