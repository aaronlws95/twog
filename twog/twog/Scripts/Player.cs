using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace twog
{
    public class Player : Entity
    {
        // constants
        private const float SHOOT_DELAY = 0.6f;
        private const float MAX_SPEED = 1.0f;
        private const float MIN_SPEED = 1.0f;
        private const float SPEED = 30f;
        private const float FRICTION = 20f;

        public Sprite Sprite = GFX.SpriteBank.Create("player");
        public PlayerInteractor PlayerInteractor;

        private float lastShotTime = 0;
        private Level level;

        // state machine
        public StateMachine StateMachine = new StateMachine(4);
        public const int StNormal = 0;
        public const int StStationary = 1;
        public const int StDead = 2;
        public const int StKnockback = 3;

        // movement
        public Vector2 Velocity;

        // player stats
        public int BaseHealth = 30;
        public int CurHealth;

        public Player(Vector2 pos) : base(pos)
        {
            Collider = new Hitbox(Sprite.Width, Sprite.Height, -Sprite.Width / 2, -Sprite.Height / 2);
            PlayerInteractor = new PlayerInteractor(new Vector2(Position.X, Position.Y + 16));
                    
            // State Machine
            StateMachine.State = StNormal;

            CurHealth = BaseHealth;

            Add(Sprite);
            Add(PlayerInteractor);

            Depth = 0;
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);
            level = SceneAs<Level>();
            if (level != null)
            {
                level.Add(new HealthBar());
            }
        }

        public override void Update()
        {
            base.Update();

            switch (StateMachine.State)
            {
                default:
                    int move_x = MInput.Keyboard.AxisCheck(Keys.A, Keys.D);
                    int move_y = MInput.Keyboard.AxisCheck(Keys.W, Keys.S);

                    if (level != null)
                    {
                        UpdateVelocityInput(new Vector2(move_x, move_y));
                    }

                    // shooting
                    int shoot_x = MInput.Keyboard.AxisCheck(Keys.Left, Keys.Right);
                    int shoot_y = MInput.Keyboard.AxisCheck(Keys.Up, Keys.Down);

                    if (Engine.TotalTime - lastShotTime > SHOOT_DELAY && (shoot_x != 0 ^ shoot_y != 0))
                    {
                        lastShotTime = Engine.TotalTime;
                        Shoot(new Vector2(shoot_x, shoot_y), 0);
                    }

                    break;
                case StStationary:
                    break;
                case StKnockback:
                    UpdateVelocityFriction();
                    
                    if (Velocity == Vector2.Zero)
                        StateMachine.State = StNormal;

                    break;
                case StDead:
                    if (!Game1.GameDialogue.Activated)
                    {
                        Game1.GameDialogue = new CoDialogue("DEAD001PLAYER");
                        Game1.GameDialogue.StartDialogue();
                    }
    
                    break;
            }

            if (StateMachine.State != StDead)
            {
                // update position
                UpdatePosition();

                // update camera
                if (level.BoundCamera)
                    level.Camera.MoveBound(new Vector2(0, 0), new Vector2(level.Background.GridWidth, level.Background.GridHeight));
                else
                    level.Camera.Move();

                // dealing with hits
                foreach (Bullet bullet in Scene.Tracker.GetEntities<Bullet>())
                {
                    if (bullet.CollideCheck(this) && bullet.TagCheck(GAccess.MonsterBullet))
                    {
                        Hurt(bullet.Damage);
                        Scene.Remove(bullet);
                    }
                }

                if (CurHealth <= 0)
                {
                    StateMachine.State = StDead;
                }
            }
        }


        #region Combat

        public void Shoot(Vector2 direction, int bullet_id)
        {
            if (direction != Vector2.Zero)
                Scene.Add(new Bullet(Position - Vector2.One * 4, direction, bullet_id, true));
        }

        public void Hurt(int dmg)
        {
            Sprite.Play("hurt_0");
            CurHealth -= dmg;
        }

        #endregion

        #region Dialogue

        public void EnterDialogue()
        {
            StateMachine.State = StStationary;
        }

        public void LeaveDialogue()
        {
            StateMachine.State = StNormal;
        }

        #endregion

        #region Movement

        public void UpdateVelocityInput(Vector2 add)
        {
            // update velocity        
            if (add != Vector2.Zero)
            {
                Velocity.X = add.X * SPEED * Engine.DeltaTime;
                Velocity.Y = add.Y * SPEED * Engine.DeltaTime;

                // clamp velocity
                Velocity.X = Calc.Sign(Velocity).X * Math.Max(Math.Abs(Velocity.X), MIN_SPEED);
                Velocity.Y = Calc.Sign(Velocity).Y * Math.Max(Math.Abs(Velocity.Y), MIN_SPEED);

                Velocity.X = Calc.Sign(Velocity).X * Math.Min(Math.Abs(Velocity.X), MAX_SPEED);
                Velocity.Y = Calc.Sign(Velocity).Y * Math.Min(Math.Abs(Velocity.Y), MAX_SPEED);
            }
            else
            {
                UpdateVelocityFriction();
            }
        }

        public void UpdateVelocityFriction()
        {
            if (Velocity.X > 0)
            {
                Velocity.X -= FRICTION * Engine.DeltaTime;
                if (Velocity.X < 0)
                    Velocity.X = 0;
            }
            else
            {
                Velocity.X += FRICTION * Engine.DeltaTime;
                if (Velocity.X > 0)
                    Velocity.X = 0;
            }

            if (Velocity.Y > 0)
            {
                Velocity.Y -= FRICTION * Engine.DeltaTime;
                if (Velocity.Y < 0)
                    Velocity.Y = 0;
            }
            else
            {
                Velocity.Y += FRICTION * Engine.DeltaTime;
                if (Velocity.Y > 0)
                    Velocity.Y = 0;
            }
        }

        public void UpdatePosition()
        {
            // update position
            if (!CollideCheck(GAccess.SolidTag, new Vector2(X + Velocity.X, Y)))
                X += Velocity.X;

            if (!CollideCheck(GAccess.SolidTag, new Vector2(X, Y + Velocity.Y)))
                Y += Velocity.Y;

            // ensure position is within map bounds
            X = Math.Min(level.Background.GridWidth - 8, Math.Max(8, X));
            Y = Math.Min(level.Background.GridHeight - 8, Math.Max(8, Y));

            // update interactor
            PlayerInteractor.Move();
        }

        #endregion
    }
}
