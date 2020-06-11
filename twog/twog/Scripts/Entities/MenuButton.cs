using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    

    public class MenuButton : Entity
    {
        public Sprite Sprite;
        public bool Selected;

        public MenuButton(Vector2 pos, string id, bool selected=false) : base(pos)
        {
            Selected = selected;
            Sprite = GFX.SpriteBank.Create(id + "_button");
            Add(Sprite);
        }
    }
}
