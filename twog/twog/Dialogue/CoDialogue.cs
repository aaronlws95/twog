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
    public class CoDialogue
    {
        public string DialogueState { get; set; }
        private Coroutine dialogueCoroutine;
        public bool Running { get; private set; }
        private int optionChoice;
        private int leaveChoiceNum;

        public CoDialogue(string dialogueState)
        {
            DialogueState = dialogueState;
            Running = false;
        }

        public void StartDialogue()
        {
            dialogueCoroutine = new Coroutine(DisplayDialogue(DialogueState));
        }

        public void Update()
        {
            dialogueCoroutine.Update();
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

                // fade all previous text
                Game1.NarBox.FadeAllLines();

                // log character dialogue
                Game1.NarBox.Log(character.ToUpper() + " -- \"" + text + "\"", Color.Red);
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    Game1.NarBox.Log((i + 1) + ". " + options[i], Color.Yellow);
                }
                if (dialogueInfo.CanEnd)
                {
                    Game1.NarBox.Log(options.GetLength(0) + 1 + ". Leave", Color.Yellow);
                    leaveChoiceNum = options.GetLength(0);
                }
                Game1.NarBox.Log("");

                // wait for key input
                int canEnd = dialogueInfo.CanEnd ? 1 : 0;
                yield return WaitForKeyPressed(options.GetLength(0) + canEnd);

                // resolve key input
                if (dialogueInfo.CanEnd && optionChoice == leaveChoiceNum)
                {
                    StopDisplayDialogue();
                    Game1.NarBox.Open = false;
                    Game1.Player.StateMachine.State = twog.Player.StNormal;
                    Game1.NarBox.ClearLog();
                }
                else
                {
                    Game1.NarBox.Log("You " + " -- \"" + options[optionChoice] + "\"", Color.Gray, true);
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

        public void StopDisplayDialogue()
        {
            Running = false;
        }
    }
}
