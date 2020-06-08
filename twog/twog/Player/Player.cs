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
        private const float SHOOT_DELAY = 0.25f;
        private float lastShotTime = 0;

        public Sprite Sprite;
        public StateMachine StateMachine;

        public const int StNormal = 0;
        public const int StStationary = 1;

        public PlayerInteractor PlayerInteractor;

        public Vector2 AimDirection;

        public Player(Vector2 pos) : base(pos)
        {
            Sprite = GFX.SpriteBank.Create("player");
            Collider = new Hitbox(16, 16, -8, -8);
            Add(Sprite);

            // State Machine
            StateMachine = new StateMachine(2);
            StateMachine.SetCallbacks(StNormal, null, null, null, null);
            StateMachine.SetCallbacks(StStationary, null, null, null, null);

            StateMachine.State = StNormal;

            PlayerInteractor = new PlayerInteractor(new Vector2(Position.X, Position.Y + 16));
            Add(PlayerInteractor);
        }

        public void EnterDialogue()
        {
            StateMachine.State = StStationary;
            PlayerInteractor.CanUse = false;
        }

        public void LeaveDialogue()
        {
            StateMachine.State = StNormal;
            PlayerInteractor.CanUse = true;
        }

        public override void Update()
        {
            base.Update();

            if (StateMachine.State != StStationary)
            {
                int move_x = MInput.Keyboard.AxisCheck(Keys.Left, Keys.Right);
                int move_y = MInput.Keyboard.AxisCheck(Keys.Up, Keys.Down);

                Level level = SceneAs<Level>();
                if (level != null)
                {
                    Move(new Vector2(move_x, move_y), new Vector2(0, 0), new Vector2(level.Background.GridWidth, level.Background.GridHeight));
                    if (level.BoundCamera)
                        level.Camera.Move(new Vector2(move_x, move_y), Position, new Vector2(0, 0), new Vector2(level.Background.GridWidth, level.Background.GridHeight));
                    else
                        level.Camera.Move(new Vector2(move_x, move_y), Position);
                }

                if (move_x != 0 || move_y != 0)
                    AimDirection = new Vector2(move_x, move_y);
                
                // shooting
                if (Engine.TotalTime - lastShotTime > SHOOT_DELAY && MInput.Keyboard.Check(Keys.C))
                {
                    lastShotTime = Engine.TotalTime;
                    Shoot(AimDirection);
                }
            }
        }

        public void Shoot(Vector2 direction)
        {
            if (direction != Vector2.Zero)
                Scene.Add(new Bullet(Position, direction));
        }

        public void Move(Vector2 add, Vector2 minClamp, Vector2 maxClamp)
        {
            if (add.X != 0 || add.Y != 0)
            {
                if (CollideCheck(GAccess.SolidTag, new Vector2(X + add.X, Y)))
                {
                    while (!CollideCheck(GAccess.SolidTag, new Vector2(X + Calc.Sign(add).X, Y)))
                        X += Calc.Sign(add).X;
                }
                else
                    X += add.X;

                if (CollideCheck(GAccess.SolidTag, new Vector2(X, Y + add.Y)))
                {
                    while (!CollideCheck(GAccess.SolidTag, new Vector2(X, Y + Calc.Sign(add).Y)))
                        Y += Calc.Sign(add).Y;
                }
                else
                    Y += add.Y;
            }

            X = Math.Min(maxClamp.X - 8, Math.Max(minClamp.X + 8, X));
            Y = Math.Min(maxClamp.Y - 8, Math.Max(minClamp.Y + 8, Y));

            PlayerInteractor.Move(new Vector2(Position.X, Position.Y), new Vector2(add.X, add.Y));
        }
        
    }
}
