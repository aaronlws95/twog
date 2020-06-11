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
        public int Damage;
        public bool Shooting;

        public Sprite Sprite;
        public Vector2 Velocity;


        public Bullet(Vector2 pos, Vector2 direction, int id, bool player=true)
        {
            if (player)
                Tag = GAccess.PlayerBullet;
            else
                Tag = GAccess.MonsterBullet;

            float speed;

            switch (id)
            {
                default:
                    Damage = 1;
                    speed = 2f;
                    SetAlarm(0.4f);
                    Shooting = true;
                    break;
                case 0:
                    Damage = 1;
                    speed = 100f;
                    SetAlarm(0.4f);
                    Shooting = true;
                    break;
                case 1:
                    Damage = 2;
                    speed = 200f;
                    Shooting = false;
                    break;
            }

            Sprite = GFX.SpriteBank.Create("bullet_" + id);
            Add(Sprite);
            Position = new Vector2(pos.X, pos.Y);
            Collider = new Hitbox(Sprite.Width, Sprite.Height);
            Velocity = speed * Vector2.Normalize(direction);
        }

        public void SetVelocity(float speed, Vector2 direction)
        {
            Velocity = speed * Vector2.Normalize(direction);
        }

        public void SetAlarm(float lifespan)
        {
            Alarm Alarm = Alarm.Set(this, lifespan, OnComplete);
        }

        public void OnComplete()
        {
            Scene.Remove(this);
        }

        public override void Update()
        {
            base.Update();            

            if (Shooting)
            {
                X += Velocity.X * Engine.DeltaTime;
                Y += Velocity.Y * Engine.DeltaTime;

                if (CollideCheck(GAccess.SolidTag))
                {
                    Scene.Remove(this);
                }
            }

        }
    }
}
