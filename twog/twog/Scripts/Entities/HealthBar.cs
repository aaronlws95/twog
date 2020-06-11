using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    public class HealthBar : Entity
    {
        public MTexture Holder;
        public MTexture FullBar;
        public MTexture CurBar;
        public Vector2 Offset;
        public Vector2 HolderPosition;
        public Vector2 BarOffset;

        public HealthBar()
        {
            HolderPosition = Vector2.Zero;
            BarOffset = new Vector2(10, 2);
            Offset = new Vector2(4, 4);
            Holder = GFX.SpriteBank.Create("healthbar_holder").Texture;
            FullBar = GFX.SpriteBank.Create("healthbar_bar").Texture;
            CurBar = FullBar;
            //Add(Holder);
            //Add(Bar);
        }

        public override void Update()
        {
            base.Update();
            Level level = SceneAs<Level>();
            Vector2 newScreenCenter = level.Camera.ScreenToCamera(new Vector2(0, 0));
            HolderPosition = newScreenCenter + Offset;
            Rectangle clipBar = new Rectangle(0, 0, FullBar.Width * Game1.Player.CurHealth / Game1.Player.BaseHealth, FullBar.Height);
            CurBar = new MTexture(FullBar, clipBar);
        }

        public override void Render()
        {
            base.Render();

            Holder.Draw(HolderPosition);
            CurBar.Draw(HolderPosition + BarOffset);
        }
    }
}
