using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    [Tracked]
    public class Door : Entity
    {
        public Scene NextScene { get; private set; }
        public bool Locked { get; set; }

        public Door(Vector2 pos, int width, int height) : base(pos)
        {
            Collider = new Hitbox(width, height);
            Tag = GAccess.DoorTag;
            Locked = true;
        }

        public Door(Vector2 pos, int width, int height, Scene nextScene) : base(pos)
        {
            NextScene = nextScene;
            Collider = new Hitbox(width, height);
            Locked = false;
        }

        public override void Update()
        {
            base.Update();

            if (!Locked)
            {
                PlayerInteractor playerInteractor = Scene.Tracker.GetComponent<PlayerInteractor>();
                if (playerInteractor.Check(this))
                    Engine.Scene = NextScene;
            }
        }
    }
}
