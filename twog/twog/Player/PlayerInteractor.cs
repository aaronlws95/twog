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
    class PlayerInteractor : Entity
    {
        public PlayerInteractor(Vector2 pos) : base(pos)
        {
            Collider = new Hitbox(4, 4, -2, -2);
        }

        public override void Update()
        {
            if (MInput.Keyboard.Pressed(Keys.F))
            {
                Door door = CollideFirst<Door>();
                if (door != null)
                {
                    Engine.Scene = door.NextScene;
                }

                NPC npc = CollideFirst<NPC>();
                if (npc != null)
                {
                    Console.WriteLine("hi");
                }
            }
        }

        public void Move(Vector2 pos, Vector2 add)
        {
            if(add.X != 0 || add.Y != 0)
            {
                Position = new Vector2(pos.X + add.X * 16, pos.Y + add.Y * 16);
            }
            
        }
    }
}
