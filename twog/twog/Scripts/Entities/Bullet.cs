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
    public class Bullet : Entity
    {
        // constants
        public float Lifespan = 0.4f;
        public float Speed = 2f;

        public Sprite Sprite;
        public Vector2 Velocity;
        public Alarm Alarm;

        public Bullet(Vector2 pos, Vector2 direction, int bullet_id, bool player)
        {
            if (player)
                Tag = GAccess.PlayerBullet;
            else
                Tag = GAccess.MonsterBullet;

            Sprite = GFX.SpriteBank.Create("bullet_" + bullet_id);
            Add(Sprite);
            Position = new Vector2(pos.X, pos.Y);
            Collider = new Hitbox(Sprite.Width, Sprite.Height);
            Velocity = new Vector2(Speed * direction.X, Speed * direction.Y);
            Alarm = Alarm.Set(this, Lifespan, OnComplete);
        }

        public void OnComplete()
        {
            Scene.Remove(this);
        }

        public override void Update()
        {
            base.Update();            
            X += Velocity.X;
            Y += Velocity.Y;

            if (CollideCheck(GAccess.SolidTag))
            {
                Scene.Remove(this);
            }
        }
    }
}
