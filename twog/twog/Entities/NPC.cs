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

        public NPC(string name) :base()
        {
            Name = name;
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(16, 16, 0, 0);
            Add(Sprite);
            Tag = GAccess.NPCTag;
            AddTag(GAccess.SolidTag);
        }

        public NPC(string name, Vector2 pos) : base(pos)
        {
            Name = name;
            Sprite = GFX.SpriteBank.Create(name);
            Collider = new Hitbox(16, 16, 0, 0);
            Add(Sprite);
            Tag = GAccess.NPCTag;
            AddTag(GAccess.SolidTag);
        }

        public void StartDialogue()
        {
            CoDialogue.StartDialogue();
        }

        public override void Update()
        {
            base.Update();

            if (CoDialogue != null)
            {
                PlayerInteractor playerInteractor = Scene.Tracker.GetComponent<PlayerInteractor>();
                if (playerInteractor.Check(this) && !CoDialogue.Activated)
                {
                    StartDialogue();
                }

                if (CoDialogue.Activated)
                {
                    CoDialogue.Update();
                }
            }
        }
    }
}
