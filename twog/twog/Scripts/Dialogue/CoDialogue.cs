using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace twog
{
    public class CoDialogue : Component
    {
        public string DialogueState { get; set; }
        public bool Activated { get; private set; }
        public bool Running { get; private set; }

        private Coroutine dialogueCoroutine;
        private int optionChoice;
        private int leaveChoiceNum;
        private Action OnComplete;

        public CoDialogue() : base(true, false)
        {
            Running = false;
        }

        public CoDialogue(string dialogueState) : base(true, false)
        {
            DialogueState = dialogueState;
            Running = false;
        }

        public CoDialogue(string dialogueState, Action onComplete) : base(true, false)
        {
            OnComplete = onComplete;
            DialogueState = dialogueState;
            Running = false;
        }

        public override void Update()
        {
            base.Update();
            if (Scene != null && !Activated)
            {
                PlayerInteractor playerInteractor = Scene.Tracker.GetComponent<PlayerInteractor>();
                if (playerInteractor != null)
                {
                    if (playerInteractor.Check(Entity))
                        StartDialogue();
                }
            }

            if (Activated)
                dialogueCoroutine.Update();
        }

        public void StartDialogue()
        {
            Game1.NarBox.Open = true;
            Activated = true;
            dialogueCoroutine = new Coroutine(DisplayDialogue(DialogueState));
            Game1.Player.EnterDialogue();
        }

        public void StopDisplayDialogue()
        {
            Running = false;
            Activated = false;
            Game1.NarBox.Open = false;
            Game1.Player.LeaveDialogue();
            Game1.NarBox.ClearLog();
        }

        private IEnumerator DisplayDialogue(string key)
        {
            if (Running)
                yield break;
            else
                Running = true;
                
            DialogueInfo dialogueInfo = DialogueData.GetDialogueInfo(key);

            while (Running)
            {
                string character = dialogueInfo.Character;
                string text = dialogueInfo.Text;
                string[] options = dialogueInfo.Options;

                // log character dialogue
                Game1.NarBox.Log(character.ToUpper() + " -- \"" + text + "\"", Color.Red);
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    Game1.NarBox.Log((i + 1) + ". " + options[i], Color.Yellow);
                }
                if (dialogueInfo.CanEnd)
                {
                    Game1.NarBox.Log(options.GetLength(0) + 1 + ". End", Color.Yellow);
                    leaveChoiceNum = options.GetLength(0);
                }
                Game1.NarBox.Log("");

                // wait for key input
                int canEnd = dialogueInfo.CanEnd ? 1 : 0;
                yield return WaitForKeyPressed(options.GetLength(0) + canEnd);

                // fade all previous text
                Game1.NarBox.FadeAllLines();

                // resolve key input
                if (dialogueInfo.CanEnd && optionChoice == leaveChoiceNum)
                {
                    StopDisplayDialogue();
                    OnComplete?.Invoke();
                }
                else
                {
                    if (options[optionChoice] != "next")
                    {
                        Game1.NarBox.Log("You " + " -- \"" + options[optionChoice] + "\"", Color.Gray, true);
                    }
                    DialogueState = dialogueInfo.Paths[optionChoice];
                    dialogueInfo = DialogueData.GetDialogueInfo(DialogueState);
                }
            }
        }

        private IEnumerator WaitForKeyPressed(int optionCount)
        {
            bool pressed = false;

            while (!pressed)
            {
                for (int i = 0; i < optionCount; i++)
                {
                    if (MInput.Keyboard.Pressed((Keys)(49 + i)))
                    {
                        pressed = true;
                        optionChoice = i;
                        break;
                    }
                }
                yield return null;
            }
        }
    }
}
