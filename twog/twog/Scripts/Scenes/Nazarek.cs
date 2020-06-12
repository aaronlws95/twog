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
    class Nazarek : Level
    {
        public Nazarek() : base()
        {
            Name = "Nazarek";
            BoundCamera = true;
        }

        public override void Begin()
        {
            base.Begin();

            Background = new Background("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/nazarek_map");
            Add(Background);

            House house1 = new House(1, new Vector2(4 * 16, 2 * 16));
            Add(house1);

            House house0 = new House(0, new Vector2(21 * 16, 2 * 16));
            Add(house0);

            Door door0 = new Door(new Vector2(house0.X + 16, house0.Y + 16 * 4), 16, 16);
            door0.Add(new CoDialogue("LOCKED001DOOR"));
            door0.Locked = true;
            Add(door0);

            Door door1 = new Door(new Vector2(house1.X + 30, house1.Y + 62), 21, 17, new Scene1());
            Add(door1);

            Door southExit = new Door(new Vector2(13 * 16, 29 * 16), 4 * 16, 2 * 16, new NazarekSouth());
            Add(southExit);

            Add(Game1.Player);

            EverythingRenderer er = new EverythingRenderer();
            Camera = new PlayerCamera(640, 360, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            Camera.Position = new Vector2(Game1.Player.Position.X - Engine.Width / 2, Game1.Player.Position.Y - Engine.Height / 2);
            er.Camera = Camera;
            Add(er);

            Vector2 newScreenCenter = Camera.ScreenToCamera(new Vector2(Engine.Width / 2, Engine.Height / 2));
            PlayerCameraStaticBound camDebug = new PlayerCameraStaticBound(newScreenCenter, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            Add(camDebug);

            Monsters monster = new Zaletos(new Vector2(14 * 16 - 8, 14 * 16 - 8));
            Add(monster);
        }
    }
}
