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
        public int Health = 3;
        public float KnockbackDistance = 5f;

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

            // detect area
            DetectAreaCollider = new CollidableComponent(true, false, true);
            float detectAreaColliderWidth = Sprite.Width * 3;
            float detectAreaColliderHeight = Sprite.Width * 3;
            DetectAreaCollider.Collider = new Hitbox(detectAreaColliderWidth, detectAreaColliderHeight, 
                                                    -detectAreaColliderWidth / 2 + Sprite.Width / 2, 
                                                    -detectAreaColliderHeight / 2 + Sprite.Height / 2);
            Add(DetectAreaCollider);

            Depth = 1;
        }

        public void Hurt(int dmg)
        {
            Sprite.Play("hurt_0");
            Health -= dmg;
        }

        public override void Update()
        {
            base.Update();

            if (StateMachine.State != StDead)
            {
                // dealing with hits
                foreach (Bullet bullet in Scene.Tracker.GetEntities<Bullet>())
                {
                    if (bullet.CollideCheck(this) && bullet.TagCheck(GAccess.PlayerBullet))
                    {
                        Hurt(bullet.Damage);
                        Scene.Remove(bullet);
                    }
                }

                if (Health == 0)
                {
                    StateMachine.State = StDead;
                }
                    

                // knocking into player
                if (CollideCheck(Game1.Player))
                {
                    // only knockback if not currently being knockbacked
                    if (Game1.Player.StateMachine.State != Player.StKnockback)
                    {
                        Game1.Player.Velocity = Calc.Sign(Center - Game1.Player.Position) * -KnockbackDistance;
                        Game1.Player.StateMachine.State = Player.StKnockback;
                        Game1.Player.Hurt(TOUCH_DMG);
                    }
                }
            }
        }
    }
}
