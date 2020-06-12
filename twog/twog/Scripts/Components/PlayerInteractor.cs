using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace twog
{
    [Tracked]
    public class PlayerInteractor : CollidableComponent
    {
        public Vector2 Position { get; set; }
        private readonly int width = 4;
        private readonly int height = 4;

        public PlayerInteractor(Vector2 pos) : base(true, false, true)
        {
            Position = pos;
        }

        public bool Check(Entity other)
        {
            if (MInput.Keyboard.Pressed(Keys.Space))
                return Collide.Check(other, this);
            else
                return false;
        }

        public void Move()
        {
            // if velocity is not zero to save last direction
            if (Game1.Player.Velocity != Vector2.Zero)
                Collider = new Hitbox(width, height, Calc.Sign(Game1.Player.Velocity).X * 16 - 2, Calc.Sign(Game1.Player.Velocity).Y * 16 - 2);
        }


           
    }
}
