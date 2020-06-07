using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace twog
{
    public class NarBox
    {
        private const float OPACITY = 0.9f;
        private const float REPEAT_DELAY = .1f;
        private const float REPEAT_EVERY = 1 / 30f;
        private const int LINE_SPACE_Y = 30;
        private const int MARGIN_X = 10;
        private const int MARGIN_Y = 10;

        public bool Open;

        private int screenWidth;
        private int screenHeight;
        private int narboxWidth;
        private int narboxHeight;
        private int narboxX;
        private int narboxY;
        private int maxLines;
        private KeyboardState oldState;
        private KeyboardState currentState;
        private int scrollOffset = 0;
        private bool canOpen;
        private List<Line> drawCommands;
        private Keys? repeatKey = null;
        private float repeatCounter = 0;
        private Color Color = Color.Black;

        public NarBox()
        {
            UpdateDimensions();
            drawCommands = new List<Line>();
            Log("Welcome to Nazarek. I hope you enjoy your stay.", Color.White, true);
        }

        internal void UpdateClosed()
        {
            if (!canOpen)
                canOpen = true;
            else if (MInput.Keyboard.Pressed(Keys.OemTilde))
            {
                Open = true;
                currentState = Keyboard.GetState();
            }
        }

        private void UpdateDimensions()
        {
            screenWidth = Engine.ViewWidth;
            screenHeight = Engine.ViewHeight;
            narboxWidth = screenWidth / 4 + 20;
            narboxHeight = screenHeight * 9 / 10;
            narboxX = screenWidth - narboxWidth - 30;
            narboxY = (screenHeight - narboxHeight) / 2;
            maxLines = (narboxHeight - MARGIN_Y) / LINE_SPACE_Y;
        }

        internal void UpdateOpen()
        {
            UpdateDimensions();

            oldState = currentState;
            currentState = Keyboard.GetState();

            if (repeatKey.HasValue)
            {
                if (currentState[repeatKey.Value] == KeyState.Down)
                {
                    repeatCounter += Engine.DeltaTime;

                    while (repeatCounter >= REPEAT_DELAY)
                    {
                        HandleKey(repeatKey.Value);
                        repeatCounter -= REPEAT_EVERY;
                    }
                }
                else
                    repeatKey = null;
            }

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
            if (key == Keys.Q || key == Keys.W)
            {
                repeatKey = key;
                repeatCounter = 0;
            }

            switch (key)
            {
                default:
                    break;
                case Keys.OemTilde:
                    Open = canOpen = false;
                    break;
                case Keys.Q:
                    ScrollUp();
                    break;
                case Keys.W:
                    ScrollDown();
                    break;
            }
        }

        private void ScrollUp()
        {
            if (scrollOffset != 0)
            {
                scrollOffset -= 1;
            }
        }

        private void ScrollDown()
        {
            
            if (scrollOffset < Math.Min(drawCommands.Count - maxLines, maxLines))
            {
                scrollOffset += 1;
            }
        }

        public void Log(object obj, Color color, bool logBreak)
        {
            Log(obj, color);
            if (logBreak)
                Log("");
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
            int maxWidth = narboxWidth - MARGIN_X;
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

            //Don't overflow bottom of window
            int drawCommandsCount = drawCommands.Count;
            scrollOffset = 0;
            while (drawCommandsCount > maxLines)
            {
                drawCommandsCount -= 1;
                scrollOffset += 1;
            }
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
                int lineNum = 0;
                int lastLineNum = Math.Min(scrollOffset + maxLines, drawCommands.Count);
                for (int i = scrollOffset; i < lastLineNum; i++)
                {
                    Draw.SpriteBatch.DrawString(Draw.DefaultFont, drawCommands[i].Text, new Vector2(narboxX + MARGIN_X, narboxY + MARGIN_Y + (LINE_SPACE_Y * lineNum)), drawCommands[i].Color,
                            0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                    lineNum += 1;
                }

            }

            Draw.SpriteBatch.End();
        }

        public void FadeAllLines()
        {
            for (int i = 0; i < drawCommands.Count; i++)
            {
                if (!drawCommands[i].Faded)
                {
                    drawCommands[i] = new Line(drawCommands[i].Text, drawCommands[i].Color * 0.4f, true);
                }

            }
        }

        public void ClearLog()
        {
            drawCommands = new List<Line>();
        }

        private struct Line
        {
            public string Text;
            public Color Color;
            public bool Faded;

            public Line(string text)
            {
                Text = text;
                Color = Color.White;
                Faded = false;
            }

            public Line(string text, Color color)
            {
                Text = text;
                Color = color;
                Faded = false;
            }

            public Line(string text, Color color, bool faded)
            {
                Text = text;
                Color = color;
                Faded = faded;
            }
        }
    }
}
