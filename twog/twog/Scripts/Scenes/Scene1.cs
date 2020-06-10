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
    class Scene1 : Level
    {
        public Scene1()
        {
            Name = "Scene1";
            BoundCamera = false;
        }

        public override void Begin()
        {
            base.Begin();

            Background = new Background("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/bg_map_scene1");
            Add(Background);

            ObjectMap objectMap = new ObjectMap("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/object_map_scene1");
            Add(objectMap);

            Door door = new Door(new Vector2(5 * 16, 11 * 16), 16, 16, new Scene0());
            Add(door);

            Add(Game1.Player);

            EverythingRenderer er = new EverythingRenderer();
            Camera = new PlayerCamera(640, 360, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            Camera.Position = new Vector2(Game1.Player.Position.X - Engine.Width / 2, Game1.Player.Position.Y - Engine.Height / 2);
            er.Camera = Camera;
            Add(er);

            Vector2 newScreenCenter = Camera.ScreenToCamera(new Vector2(Engine.Width / 2, Engine.Height / 2));
            PlayerCameraStaticBound camDebug = new PlayerCameraStaticBound(newScreenCenter, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            Add(camDebug);

            NPC aisya = Game1.NPCDict["Aisya"];
            aisya.Position = new Vector2(5 * 16, 3 * 16);
            Add(aisya);
        }
    }
}
