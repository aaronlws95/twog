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
        public int Id { get; private set; }

        public House(int id, Vector2 pos) : base(pos)
        {
            Id = id;
            switch (id)
            {
                default:
                    Collider = new Hitbox(80, 80, 0, 0);
                    break;
                case 1:
                    Collider = new Hitbox(80, 74, 0, 6);
                    break;
            }

            Sprite = GFX.SpriteBank.Create("house_" + id);
            Add(Sprite);

            Tag = GAccess.HouseTag;
            AddTag(GAccess.CollideTag);
        }


    }
}
