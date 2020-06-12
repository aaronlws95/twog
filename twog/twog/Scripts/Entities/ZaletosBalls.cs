using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class ZaletosBalls : Entity
    {
        // constant
        public const float INIT_SPEED = 100f;
        public const float SHOOT_SPEED = 500f;
        public const float ANGULAR_SPEED = 1f;
        public const int NUM_BALLS = 8;
        public const float RADIUS = 50f;
        public const float BULLET_LIFESPAN = 1f;

        public Vector2 SpriteOffset = new Vector2(18, 14);
        private Zaletos Zaletos;
        public Sprite Sprite;
        public bool Init = true;

        public List<Bullet> Balls = new List<Bullet>();

        public ZaletosBalls(Zaletos zaletos)
        {
            Zaletos = zaletos;
        }

        public void Recharge()
        {
            for (int i = 0; i < NUM_BALLS; i++)
            {
                Bullet ball = new Bullet(Zaletos.Position + SpriteOffset, Vector2.Zero, 1, false);
                Balls.Add(ball);
                Scene.Add(ball);
            }
        }

        public void Clear()
        {
            for (int i = 0; i<Balls.Count; i++)
            {
                Scene.Remove(Balls[i]);
                Balls.RemoveAt(i);
            }
        }

        public void Shoot(Vector2 target)
        {
            for (int i =0; i< Balls.Count;i++)
            {
                if (!Balls[i].Shooting)
                {
                    Balls[i].Shooting = true;
                    Vector2 targetDirection = Vector2.Normalize(target - Balls[i].Position);
                    Balls[i].SetVelocity(SHOOT_SPEED, targetDirection);
                    Balls[i].SetAlarm(BULLET_LIFESPAN);
                    Balls.RemoveAt(i);
                    break;
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (Balls.Count == 0)
                Init = true;

            for (int i = 0; i < Balls.Count; i++)
            {
                // initialize balls around Zaletos
                if (Init)
                {
                    // if the ball isn't in position
                    if (!(Vector2.Distance(Balls[i].Position, Zaletos.Position + SpriteOffset) >= RADIUS))
                    {
                        float angle = (float)(2 * Math.PI * i / Balls.Count);
                        Vector2 direction = Vector2.Normalize(Calc.Rotate(new Vector2(0, -1f), angle));
                        Vector2 velocity = new Vector2(INIT_SPEED * direction.X, INIT_SPEED * direction.Y);
                        Balls[i].X += velocity.X * Engine.DeltaTime;
                        Balls[i].Y += velocity.Y * Engine.DeltaTime;
                    }
                    else
                    {
                        // if one balls is in position then all of them are, stop init 
                        Init = false;
                    }
                }
                else
                {
                    // if the ball is not being shot, it is rotating around zaletos
                    if (!Balls[i].Shooting)
                    {
                        Vector2 refPosition = Zaletos.Position + SpriteOffset;
                        Vector2 offset = Balls[i].Position - refPosition;
                        float angle = offset.Angle() + ANGULAR_SPEED * Engine.DeltaTime;

                        Balls[i].X = RADIUS * (float)Math.Cos(angle) + refPosition.X;
                        Balls[i].Y = RADIUS * (float)Math.Sin(angle) + refPosition.Y;
                    }
                }
            }
        }            
    }
}
