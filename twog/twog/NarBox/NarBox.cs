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
    public class NarBox
    {
        private const float OPACITY = 0.9f;
        private Color Color = Color.Black;

        private int screenWidth;
        private int screenHeight;
        private int narboxWidth;
        private int narboxHeight;
        private int narboxX;
        private int narboxY;
        private KeyboardState oldState;
        private KeyboardState currentState;
        
        public bool Open;

        private bool canOpen;
        private List<Line> drawCommands;

        public NarBox()
        {
            screenWidth = Engine.ViewWidth;
            screenHeight = Engine.ViewHeight;
            narboxWidth = screenWidth / 4 + 20;
            narboxHeight = screenHeight * 9 / 10;
            narboxX = screenWidth - narboxWidth - 30;
            narboxY = (screenHeight - narboxHeight) / 2;
            drawCommands = new List<Line>();
            Log("Thou lettest man flow on like a river, and Thy years know no end. As for man, his days are like grass as a flower on the field, so he blossoms. For the wind passeth over it, and it is gone, and the place thereof shall know it no more.", Color.White);
            Log("Perucho, don't you think the cannon might be a little bit rusty?", Color.Red);
        }

        internal void UpdateClosed()
        {
            if (!canOpen)
                canOpen = true;
            else if (MInput.Keyboard.Pressed(Keys.D1))
            {
                Open = true;
                currentState = Keyboard.GetState();
            }
        }

        internal void UpdateOpen()
        {
            screenWidth = Engine.ViewWidth;
            screenHeight = Engine.ViewHeight;
            narboxWidth = screenWidth / 4 + 20;
            narboxHeight = screenHeight * 9 / 10;
            narboxX = screenWidth - narboxWidth - 30;
            narboxY = (screenHeight - narboxHeight) / 2;

            oldState = currentState;
            currentState = Keyboard.GetState();

            foreach (Keys key in currentState.GetPressedKeys())
            {
                if (oldState[key] == KeyState.Up)
                {
                    HandleKey(key);
                    break;
                }
            }
        }

        private void HandleKey(Keys key)
        {
            switch (key)
            {
                default:
                    break;
                case Keys.D1:
                    Open = canOpen = false;
                    break;
            }
        }

        public void Log(object obj, Color color)
        {
            string str = obj.ToString();

            //Newline splits
            if (str.Contains("\n"))
            {
                var all = str.Split('\n');
                foreach (var line in all)
                    Log(line, color);
                return;
            }

            //Split the string if you overflow horizontally
            int maxWidth = narboxWidth - 10;
            while (Draw.DefaultFont.MeasureString(str).X > maxWidth)
            {
                int split = -1;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        if (Draw.DefaultFont.MeasureString(str.Substring(0, i)).X <= maxWidth)
                            split = i;
                        else
                            break;
                    }
                }

                if (split == -1)
                    break;

                drawCommands.Add(new Line(str.Substring(0, split), color));
                str = str.Substring(split + 1);
            }

            drawCommands.Add(new Line(str, color));

            //Don't overflow top of window
            int maxCommands = narboxHeight / 30;
            while (drawCommands.Count > maxCommands)
                drawCommands.RemoveAt(0);
        }

        public void Log(object obj)
        {
            Log(obj, Color.White);
        }


        internal void Render()
        {
            Draw.SpriteBatch.Begin();
            Draw.Rect(narboxX, narboxY, narboxWidth, narboxHeight, Color * OPACITY);

            if (drawCommands.Count > 0)
            {
                for (int i = 0; i < drawCommands.Count; i++)
                    Draw.SpriteBatch.DrawString(Draw.DefaultFont, drawCommands[i].Text, new Vector2(narboxX + 10, narboxY + 10 + (30 * i)), drawCommands[i].Color);
            }

            Draw.SpriteBatch.End();
        }

        private struct Line
        {
            public string Text;
            public Color Color;

            public Line(string text)
            {
                Text = text;
                Color = Color.White;
            }

            public Line(string text, Color color)
            {
                Text = text;
                Color = color;
            }
        }
    }
}
