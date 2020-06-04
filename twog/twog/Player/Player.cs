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
            Add(Sprite);
        }

        public void Move(Vector2 add)
        {
            
            if (add.X != 0 || add.Y != 0)
            {
                X += add.X;
                Y += add.Y;
            }
        }
    }
}
