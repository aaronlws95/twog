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
    [Tracked]
    public class PlayerInteractor : CollidableComponent
    {
        public bool CanUse { get; set; }
        public Vector2 Position { get; set; }
        private int width = 4;
        private int height = 4;

        public PlayerInteractor(Vector2 pos) : base(true, false, true)
        {
            Position = pos;
            CanUse = true;
        }

        public override void Update()
        {
            if (CanUse)
            {
                //if (MInput.Keyboard.Pressed(Keys.F))
                //{
                //    Door door = CollideFirst<Door>();
                //    if (door != null)
                //    {
                //        Engine.Scene = door.NextScene;
                //    }

                //    NPC npc = CollideFirst<NPC>();
                //    if (npc != null)
                //    {
                //        npc.StartDialogue();
                //        Game1.Player.StateMachine.State = twog.Player.StStationary;
                //        CanUse = false;
                //    }
                //}
            }
        }

        public bool Check(Entity other)
        {
            if (MInput.Keyboard.Pressed(Keys.F))
                return Collide.Check(other, this);
            else
                return false;
        }

        public void Move(Vector2 pos, Vector2 add)
        {
            if (CanUse)
            {
                if (add.X != 0 || add.Y != 0)
                {
                    Collider = new Hitbox(width, height, add.X * 16 - 2, add.Y * 16 - 2);
                }
            }            
        }

        public override void DebugRender(Camera camera)
        {
            base.DebugRender(camera);
            if (Collider != null)
                Collider.Render(camera, Collidable ? Color.Red : Color.DarkRed);
        }
           
    }
}
