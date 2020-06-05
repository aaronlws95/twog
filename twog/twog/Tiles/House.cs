using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class House : Entity
    {
        public Sprite Sprite;

        public House(Vector2 pos) : base(pos)
        {
            Sprite = GFX.SpriteBank.Create("house_1");
            Add(Sprite);
            Collider = new Hitbox(80, 80);
            Tag = GAccess.HouseTag;
        }



    }
}
