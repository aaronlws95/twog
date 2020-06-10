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
        private const float SHOOT_DELAY = 0.45f;
        private const float MAX_SPEED = 1.0f;
        private const float MIN_SPEED = 1.0f;
        private const float ACCELERATION = 30f;
        private const float FRICTION = 20f;

        public Sprite Sprite;
        public PlayerInteractor PlayerInteractor;

        private float lastShotTime = 0;
        private Level level;

        // state machine
        public StateMachine StateMachine;
        public const int StNormal = 0;
        public const int StStationary = 1;
        public const int StDead = 2;
        public const int StKnockback = 3;

        // movement
        public Vector2 Velocity;
        public Vector2 Acceleration;

        // player stats
        public int Health;

        public Player(Vector2 pos) : base(pos)
        {
            Sprite = GFX.SpriteBank.Create("player");
            Collider = new Hitbox(Sprite.Width, Sprite.Height, -Sprite.Width / 2, -Sprite.Height / 2);
            Add(Sprite);

            // State Machine
            StateMachine = new StateMachine(4);
            StateMachine.SetCallbacks(StNormal, null, null, null, null);
            StateMachine.SetCallbacks(StStationary, null, null, null, null);

            StateMachine.State = StNormal;

            Health = 30;

            PlayerInteractor = new PlayerInteractor(new Vector2(Position.X, Position.Y + 16));
            Add(PlayerInteractor);

            Acceleration = new Vector2(ACCELERATION, ACCELERATION);

        }

        public override void Added(Scene scene)
        {
            base.Added(scene);
            level = SceneAs<Level>();
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
                        Shoot(new Vector2(shoot_x, shoot_y));
                    }

                    break;

                case StKnockback:
                    UpdateVelocityFriction();
                    
                    if (Velocity == Vector2.Zero)
                        StateMachine.State = StNormal;

                    break;
            }

            UpdatePosition();
            if (level.BoundCamera)
                level.Camera.MoveBound(new Vector2(0, 0), new Vector2(level.Background.GridWidth, level.Background.GridHeight));
            else
                level.Camera.Move();
        }

        public void Hurt(int dmg)
        {
            Sprite.Play("hurt_0");
            Health -= dmg;
        }

        #region Attack

        public void Shoot(Vector2 direction)
        {
            if (direction != Vector2.Zero)
                Scene.Add(new Bullet(Position, direction));
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
                Velocity.X = add.X * Acceleration.X * Engine.DeltaTime;
                Velocity.Y = add.Y * Acceleration.Y * Engine.DeltaTime;

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
