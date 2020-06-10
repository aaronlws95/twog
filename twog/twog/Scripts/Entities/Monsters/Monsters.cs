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
    public class Monsters : Entity
    {
        public const int TOUCH_DMG = 1;

        public Sprite Sprite;
        public int Health;

        public StateMachine StateMachine;
        public const int StIdle = 0;
        public const int StDead = 1;

        public Vector2 Velocity;
        public Vector2 Acceleration;

        public CollidableComponent DetectAreaCollider;

        public Monsters(string name, Vector2 pos) : base(pos)
        {
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(Sprite.Width, Sprite.Height);
            Add(Sprite);
            Tag = GAccess.HittableTag;
            AddTag(GAccess.MonsterTag);
            StateMachine = new StateMachine(2);
            StateMachine.State = StIdle;
            Health = 3;

            // detect area
            DetectAreaCollider = new CollidableComponent(true, false, true);
            float detectAreaColliderWidth = Sprite.Width * 3;
            float detectAreaColliderHeight = Sprite.Width * 3;
            DetectAreaCollider.Collider = new Hitbox(detectAreaColliderWidth, detectAreaColliderHeight, 
                                                    -detectAreaColliderWidth / 2 + Sprite.Width / 2, 
                                                    -detectAreaColliderHeight / 2 + Sprite.Height / 2);
            Add(DetectAreaCollider);
        }

        public override void Update()
        {
            base.Update();

            if (StateMachine.State != StDead)
            {
                // dealing with hits
                foreach (var bullet in Scene.Tracker.GetEntities<Bullet>())
                {
                    if (bullet.CollideCheck(this))
                    {
                        Sprite.Play("hurt_0");
                        Scene.Remove(bullet);
                        Health -= 1;
                        if (Health == 0)
                        {
                            StateMachine.State = StDead;
                        }
                    }
                }

                if (CollideCheck(Game1.Player))
                {
                    // only knockback if not currently being knockbacked
                    if (Game1.Player.StateMachine.State != Player.StKnockback)
                    {
                        Game1.Player.Velocity = Calc.Sign(Center - Game1.Player.Position) * -5f;
                        Game1.Player.StateMachine.State = Player.StKnockback;
                        Game1.Player.Hurt(TOUCH_DMG);
                    }
                }
            }
        }
    }
}
