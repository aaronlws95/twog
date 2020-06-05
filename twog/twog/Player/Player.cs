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
        public Sprite Sprite;

        public Player(Vector2 pos) : base(pos)
        {
            Sprite = GFX.SpriteBank.Create("player");
            Collider = new Hitbox(16, 16, -8, -8);
            Add(Sprite);
        }

        public void Move(Vector2 add, Vector2 minClamp, Vector2 maxClamp)
        {
            if (add.X != 0 || add.Y != 0)
            {
                if (CollideCheck(GAccess.HouseTag, new Vector2(X + add.X, Y)))
                {
                    while (!CollideCheck(GAccess.HouseTag, new Vector2(X + Calc.Sign(add).X, Y)))
                        X += Calc.Sign(add).X;
                }
                else
                    X += add.X;

                if (CollideCheck(GAccess.HouseTag, new Vector2(X, Y + add.Y)))
                {
                    while (!CollideCheck(GAccess.HouseTag, new Vector2(X, Y + Calc.Sign(add).Y)))
                        Y += Calc.Sign(add).Y;
                }
                else
                    Y += add.Y;
            }

            X = Math.Min(maxClamp.X - 8, Math.Max(minClamp.X + 8, X));
            Y = Math.Min(maxClamp.Y - 8, Math.Max(minClamp.Y + 8, Y));
        }
    }
}
