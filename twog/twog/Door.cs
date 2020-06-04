using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class Door : Entity
    {
        public Door(Vector2 pos, int width, int height) : base(pos)
        {
            Collider = new Hitbox(width, height);
            Tag = GAccess.DoorTag;
        }

    }
}
