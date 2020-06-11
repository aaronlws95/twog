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
        
        public const float SPEED = 100f;
        public const float SHOOT_SPEED = 500f;
        public const float ANGULAR_SPEED = 1f;
        public const int NUM_BALLS = 8;
        public const float RADIUS = 50f;
        public const float BULLET_LIFESPAN = 1f;

        public Vector2 SpriteOffset = new Vector2(18, 14);
        private Zaletos Zaletos;
        public List<Entity> Balls;
        public List<bool> Shooting;
        public List<Vector2> Target;
        public List<Vector2> TargetDirection;
        public List<float> BulletStartTime;
        public Sprite Sprite;

        public bool Init = true;

        public ZaletosBalls(Zaletos zaletos)
        {
            Zaletos = zaletos;

            Balls = new List<Entity>();
            Shooting = new List<bool>();
            Target = new List<Vector2>();
            TargetDirection = new List<Vector2>();
            BulletStartTime = new List<float>();

            for (int i = 0; i < NUM_BALLS; i++)
            {
                Entity ball = new Entity(Zaletos.Position + SpriteOffset);
                Sprite = GFX.SpriteBank.Create("bullet_1");
                ball.Add(Sprite);
                ball.Collider = new Hitbox(Sprite.Width, Sprite.Height);
                ball.Tag = GAccess.MonsterBullet;
                Balls.Add(ball);

                Shooting.Add(false);
                Target.Add(Vector2.Zero);
                TargetDirection.Add(Vector2.Zero);
                BulletStartTime.Add(0);
            }
            
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);

            for (int i = 0; i < Balls.Count; i++)
            {
                scene.Add(Balls[i]);
            }
        }

        public void Shoot(Vector2 target)
        {
            for (int i =0; i< Balls.Count;i++)
            {
                if (!Shooting[i])
                {
                    Shooting[i] = true;
                    Target[i] = target;
                    break;
                }
            }
        }

        public override void Update()
        {
            base.Update();


            for (int i = 0; i < Balls.Count; i++)
            {
                if (Init)
                {
                    if (!(Vector2.Distance(Balls[i].Position, Zaletos.Position + SpriteOffset) >= RADIUS))
                    {
                        float angle = (float)(2 * Math.PI * i / Balls.Count);
                        Vector2 direction = Vector2.Normalize(Calc.Rotate(new Vector2(0, -1f), angle));
                        Vector2 velocity = new Vector2(SPEED * direction.X * Engine.DeltaTime, SPEED * direction.Y * Engine.DeltaTime);
                        Balls[i].X += velocity.X;
                        Balls[i].Y += velocity.Y;
                    }
                    else
                    {
                        Init = false;
                    }
                }
                else
                {
                    if (!Shooting[i])
                    {
                        Vector2 refPosition = Zaletos.Position + SpriteOffset;
                        Vector2 offset = Balls[i].Position - refPosition;
                        float angle = offset.Angle() + ANGULAR_SPEED * Engine.DeltaTime;

                        Balls[i].X = RADIUS * (float)Math.Cos(angle) + refPosition.X;
                        Balls[i].Y = RADIUS * (float)Math.Sin(angle) + refPosition.Y;
                    }
                    else
                    {
                        if (TargetDirection[i] == Vector2.Zero)
                        {
                            TargetDirection[i] = Vector2.Normalize(Target[i] - Balls[i].Position);
                            BulletStartTime[i] = Engine.TotalTime;
                        }

                        if (Engine.TotalTime - BulletStartTime[i] < BULLET_LIFESPAN)
                        {
                            Vector2 velocity = new Vector2(SHOOT_SPEED * TargetDirection[i].X * Engine.DeltaTime, SHOOT_SPEED * TargetDirection[i].Y * Engine.DeltaTime);
                            Balls[i].X += velocity.X;
                            Balls[i].Y += velocity.Y;
                        }
                        else
                        {
                            Scene.Remove(Balls[i]);
                            Balls.RemoveAt(i);
                            Target.RemoveAt(i);
                            Shooting.RemoveAt(i);
                            TargetDirection.RemoveAt(i);
                            BulletStartTime.RemoveAt(i);
                        }
                    
                    }

                }

            }
        }
            
    }
}
