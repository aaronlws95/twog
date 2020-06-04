using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class PlayerCamera : Camera
    {
        private int boundWidth;
        private int boundHeight;
        
        public PlayerCamera(int width, int height, int bWidth, int bHeight) : base(width, height)
        {
            boundWidth = bWidth;
            boundHeight = bHeight;
        }

        public void Move(Vector2 add, Vector2 playerPosition)
        {
            Vector2 playerPosRelativeToScreen = CameraToScreen(playerPosition);
            if (playerPosRelativeToScreen.X > Engine.Width / 2 + boundWidth / 2 ||
                playerPosRelativeToScreen.X < Engine.Width / 2 - boundWidth / 2 )
            {
                if (add.X != 0)
                {
                    X += add.X;
                }
            }

            if (playerPosRelativeToScreen.Y > Engine.Height / 2 + boundHeight / 2 ||
                playerPosRelativeToScreen.Y < Engine.Height / 2 - boundHeight / 2)
            {
                if (add.Y != 0)
                {
                    Y += add.Y;
                }
            }
        }
    }
}
