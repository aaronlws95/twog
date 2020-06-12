using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class NazarekSouth : Level
    {
        public NazarekSouth()
        {
            Name = "Nazarek South";
            BoundCamera = true;
        }

        public void OnComplete()
        {
            Game1.GameDialogue = new CoDialogue("NAZSOUTH001TUTORIAL");
            Game1.GameDialogue.StartDialogue();
            Game1.NazSouthIntro = true;
        }

        public override void Begin()
        {
            base.Begin();

            if (!Game1.NazSouthIntro)
            {
                Game1.Player.Position = new Vector2(9 * 16, 12 * 16);
                Game1.GameDialogue = new CoDialogue("NAZSOUTH001INTRO", OnComplete);
                Game1.GameDialogue.StartDialogue();                
            }

            // background
            Background = new Background("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/nazarek_south_map");
            Add(Background);
            ObjectMap objectMap = new ObjectMap("Sprites/Spritesheets/tile_spritesheet_0.png", "Maps/nazarek_south_object_map");
            Add(objectMap);

            // player
            Add(Game1.Player);

            // renderer
            EverythingRenderer er = new EverythingRenderer();
            Camera = new PlayerCamera(640, 360, Engine.Width * 1 / 3, Engine.Height * 1 / 3);
            Camera.Position = new Vector2(Game1.Player.Position.X - Engine.Width / 2, Game1.Player.Position.Y - Engine.Height / 2);
            er.Camera = Camera;
            Add(er);

            // objects
            Entity sign = new Entity(new Vector2(6 * 16, 2 * 16));
            sign.Collider = new Hitbox(16, 16);
            sign.Add(new CoDialogue("NAZSOUTH001SIGN"));
            Add(sign);

            // exits
            Door northExit = new Door(new Vector2(5 * 16, -8), 128, 16, new Nazarek());
            Add(northExit);
        }
    }
}
