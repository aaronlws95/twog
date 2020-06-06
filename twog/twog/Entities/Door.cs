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
        public Scene NextScene;

        public Door(Vector2 pos, int width, int height, Scene nextScene) : base(pos)
        {
            NextScene = nextScene;
            Collider = new Hitbox(width, height);
            Tag = GAccess.DoorTag;
        }

    }
}
