using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class Monsters : Entity
    {
        public Sprite Sprite;

        public Monsters(string name, Vector2 pos) : base(pos)
        {
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(Sprite.Width, Sprite.Height, 0, 0);
            Add(Sprite);
            Tag = GAccess.HittableTag;
        }

        public override void Update()
        {
            base.Update();

            foreach (var bullet in Scene.Tracker.GetEntities<Bullet>())
            {
                if (bullet.CollideCheck(this))
                {
                    Sprite.Play("hurt_0");
                    Scene.Remove(bullet);
                }
            }
        }
    }
}
