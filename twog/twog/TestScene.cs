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
    class TestScene : Scene
    {
        Camera camera;
        Player player;
        Background background;

        public TestScene(): base()
        {

        }

        public override void Begin()
        {
            base.Begin();
            camera = new Camera(640, 360);

            EverythingRenderer er = new EverythingRenderer();
            er.Camera = camera;
            Add(er);

            player = new Player(new Vector2(Engine.Width/2, Engine.Height/2));
            Add(player);

            background = new Background("Maps/test_map_1");
            Add(background);
        }

        public override void Update()
        {
            base.Update();

            int nx = MInput.Keyboard.AxisCheck(Keys.Left, Keys.Right);
            int ny = MInput.Keyboard.AxisCheck(Keys.Up, Keys.Down);

            player.Move(new Vector2(nx, ny));

            // This is a direct transition to test scene
            if (MInput.Keyboard.Check(Keys.LeftShift, Keys.RightShift) && MInput.Keyboard.Check(Keys.Add))
                Engine.Scene = new TestScene();
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
            Vector2 npos = new Vector2(player.X - Engine.ViewWidth / 4, player.Y - Engine.ViewHeight / 4);

            camera.Position = npos;
        }
    }
}
