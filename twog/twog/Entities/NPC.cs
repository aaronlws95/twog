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
    public class NPC : Entity
    {
        public Sprite Sprite;
        public string Name { get; private set; }

        public NPC(string name, Vector2 pos) : base(pos)
        {
            Name = name;
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(16, 16, 0, 0);
            Add(Sprite);
            Tag = GAccess.NPCTag;
            AddTag(GAccess.CollideTag);
        }
    }
}
