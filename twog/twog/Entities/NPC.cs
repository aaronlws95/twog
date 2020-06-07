using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace twog
{
    [Tracked]
    public class NPC : Entity
    {
        public Sprite Sprite;
        public string Name { get; private set; }
        public CoDialogue CoDialogue { get; set; }
        private bool dialogueRunning = false;

        public NPC(string name) :base()
        {
            Name = name;
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(16, 16, 0, 0);
            Add(Sprite);
            Tag = GAccess.NPCTag;
            AddTag(GAccess.CollideTag);
        }

        public NPC(string name, Vector2 pos) : base(pos)
        {
            Name = name;
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(16, 16, 0, 0);
            Add(Sprite);
            Tag = GAccess.NPCTag;
            AddTag(GAccess.CollideTag);
        }

        public void StartDialogue()
        {
            if (!dialogueRunning)
            {
                Game1.NarBox.Open = true;
                CoDialogue.StartDialogue();
                dialogueRunning = true;
            }
        }

        public override void Update()
        {
            base.Update();
            if (dialogueRunning)
            {
                CoDialogue.Update();
                dialogueRunning = CoDialogue.Running;
            }
        }
    }
}
