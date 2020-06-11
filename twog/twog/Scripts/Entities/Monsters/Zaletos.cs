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
        public const int StJump = 3;
        public const int StEnergyAtk = 4;

        // Constants
        private const float JUMP_DURATION = 1f;
        private const float SHOOT_DELAY = 0.1f;
        private const float ENERGY_ATK_RECHARGE = 2.5f;

        // Jumping
        public float VerticalVelocity = 0;
        public float Z = 0;
        public bool Jumping = false;
        public Vector2 JumpTarget;
        public Vector2 JumpOrigin;
        public Vector2 HorizontalDir;

        // Energy Attack
        public ZaletosBalls ZaletosBalls;
        private float lastShotTime;
        private float lastEnergyAtkTime;

        public Zaletos(Vector2 pos) : base("zaletos", pos)
        {
            StateMachine = new StateMachine(10);
            StateMachine.State = StIdle;
            Health = 10;
            ZaletosBalls = new ZaletosBalls(this);
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);
            Scene.Add(ZaletosBalls);
        }

        public override void Removed(Scene scene)
        {
            base.Added(scene);
            Scene.Remove(ZaletosBalls);
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

                    if (!CollideCheck(GAccess.SolidTag, new Vector2(X + Velocity.X, Y)))
                        X += Velocity.X;

                    if (!CollideCheck(GAccess.SolidTag, new Vector2(X, Y + Velocity.Y)))
                        Y += Velocity.Y;
                    //X += Velocity.X;
                    //Y += Velocity.Y;

                    // update vertical velocity
                    VerticalVelocity += 9.8f * Engine.DeltaTime;

                    // update vertical position
                    Z += VerticalVelocity;

                    // if landed then change state
                    if (Z > 0)
                    {
                        Z = 0;
                        
                        Jumping = false;
                        Velocity = Vector2.Zero;

                        if (Engine.TotalTime - lastEnergyAtkTime > ENERGY_ATK_RECHARGE)
                            StateMachine.State = StEnergyAtk;
                        else
                            StateMachine.State = StJump;
                    }

                    // project vertical Z to 2D 
                    if (Z != 0)
                        if (!CollideCheck(GAccess.SolidTag, new Vector2(X, Y + VerticalVelocity)))
                            Y += VerticalVelocity;

                    break;

                case StEnergyAtk:
                    if (ZaletosBalls.Balls.Count == 0 && ZaletosBalls.Init)
                    {
                        ZaletosBalls.Recharge();
                    }
                    else if (!ZaletosBalls.Init)
                    {
                        if (!ZaletosBalls.Init && Engine.TotalTime - lastShotTime > SHOOT_DELAY)
                        {
                            lastShotTime = Engine.TotalTime;
                            ZaletosBalls.Shoot(Game1.Player.Position);
                        }

                        if (ZaletosBalls.Balls.Count == 0)
                        {
                            lastEnergyAtkTime = Engine.TotalTime;
                            StateMachine.State = StJump;
                        }
                    }
                    break;

                case StDead:
                    Sprite.Play("dead_0");
                    break;
            }
        }
    }
}
