﻿using System;
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
        private const float LIFESPAN = 1f;

        public Sprite Sprite;
        public Vector2 Velocity;
        public Alarm Alarm;

        public Bullet(Vector2 pos, Vector2 direction)
        {
            Sprite = GFX.SpriteBank.Create("bullet_0");
            Add(Sprite);
            Position = new Vector2(pos.X - 4, pos.Y - 4);
            Collider = new Hitbox(7, 7, 0, 0);
            float speed = 2f;
            Velocity = new Vector2(speed * direction.X, speed * direction.Y);
            Alarm = Alarm.Set(this, LIFESPAN, OnComplete);
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
        }
    }
}
