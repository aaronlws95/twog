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

        public override void Update()
        {
            base.Update();
            Level level = SceneAs<Level>();
            Vector2 newScreenCenter = level.Camera.ScreenToCamera(new Vector2(Engine.Width / 2, Engine.Height / 2));
            Position = new Vector2(newScreenCenter.X - BoundWidth / 2 - 8, newScreenCenter.Y - BoundHeight / 2 - 8);
        }

        public override void DebugRender(Camera camera)
        {
            Draw.HollowRect(Position.X, Position.Y, BoundWidth + 16, BoundHeight + 16, Color.Blue);
        }


    }
}
