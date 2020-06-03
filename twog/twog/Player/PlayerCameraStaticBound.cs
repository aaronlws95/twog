using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class PlayerCameraStaticBound : Entity
    {
        public int BoundWidth;
        public int BoundHeight;

        public PlayerCameraStaticBound(Vector2 pos, int width, int height)
        {
            Position = new Vector2(pos.X - width / 2, pos.Y - height / 2);
            BoundWidth = width;
            BoundHeight = height;
        }

        public void Update(Vector2 pos)
        {
            Position = new Vector2(pos.X - BoundWidth / 2 - 8, pos.Y - BoundHeight / 2 - 8);
        }

        public override void DebugRender(Camera camera)
        {
            Draw.HollowRect(Position.X, Position.Y, BoundWidth + 16, BoundHeight + 16, Color.Blue);
        }


    }
}
