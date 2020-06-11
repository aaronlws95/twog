using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class Zaletos : Monsters
    {
        // State Machine
        public const int StWander = 3;
        public const int StJump = 4;
        public const int StEnergyAtk = 5;

        // Constants
        private const float JUMP_DURATION = 1.5f;
        private const float SHOOT_DELAY = 0.1f;

        // Jumping
        public float VerticalVelocity = 0;
        public float Z = 0;
        public bool Jumping = false;
        public Vector2 JumpTarget;
        public Vector2 JumpOrigin;
        public Vector2 HorizontalDir;

        public ZaletosBalls ZaletosBalls;
        public float lastShotTime;

        public Zaletos(Vector2 pos) : base("zaletos", pos)
        {
            StateMachine = new StateMachine(10);
            StateMachine.State = StIdle;
            Health = 5;
        }

        public override void Update()
        {
            base.Update();

            switch (StateMachine.State)
            {
                default:
                    break;

                case StIdle:
                    if (Collide.Check(Game1.Player, DetectAreaCollider))
                    {
                        StateMachine.State = StJump;
                    }
                    break;
                case StJump:
                    // init jump 
                    if (!Jumping)
                    {
                        JumpTarget = Game1.Player.Position;
                        JumpOrigin = Center;

                        float distance = Vector2.Distance(JumpTarget, JumpOrigin);
                        HorizontalDir = Vector2.Normalize(JumpTarget - JumpOrigin);
                        Acceleration = Vector2.One * (distance / JUMP_DURATION);
                        VerticalVelocity = -9.8f * JUMP_DURATION / 2; // vy = -aT/2

                        Jumping = true;
                    }


                    // update horizontal velocity
                    Velocity = HorizontalDir * Acceleration * Engine.DeltaTime;

                    // update horizontal position
                    X += Velocity.X;
                    Y += Velocity.Y;

                    // update vertical velocity
                    VerticalVelocity += 9.8f * Engine.DeltaTime;

                    // update vertical position
                    Z += VerticalVelocity;

                    // if landed then change state
                    if (Z > 0)
                    {
                        Z = 0;
                        StateMachine.State = StEnergyAtk;
                        Jumping = false;
                        Velocity = Vector2.Zero;
                    }

                    // project vertical Z to 2D 
                    if (Z != 0)
                        Y += VerticalVelocity;

                    break;
                case StEnergyAtk:
                    if (ZaletosBalls == null)
                    {
                        ZaletosBalls = new ZaletosBalls(this);
                        Scene.Add(ZaletosBalls);
                    }
                    else
                    {
                        if (!ZaletosBalls.Init && Engine.TotalTime - lastShotTime > SHOOT_DELAY)
                        {
                            lastShotTime = Engine.TotalTime;
                            ZaletosBalls.Shoot(Game1.Player.Position);
                        }

                        if (ZaletosBalls.Balls.Count == 0)
                        {
                            Scene.Remove(ZaletosBalls);
                            ZaletosBalls = null;
                            StateMachine.State = StJump;
                        }
                    }

                        
                    break;
                case StDead:
                    if (ZaletosBalls != null)
                    {
                        Scene.Remove(ZaletosBalls);
                        ZaletosBalls = null;
                    }
                    Sprite.Play("dead_0");
                    AddTag(GAccess.SolidTag);
                    break;
            }
        }
    }
}
