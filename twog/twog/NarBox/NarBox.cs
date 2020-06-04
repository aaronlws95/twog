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
        private static int screenWidth = Engine.ViewWidth;
        private static int screenHeight = Engine.ViewHeight;
        private static int narboxWidth = screenWidth / 4 + 20;
        private static int narboxHeight = screenHeight * 9 / 10;
        private static int narboxX = screenWidth - narboxWidth - 30;
        private static int narboxY = (screenHeight - narboxHeight) / 2;

        public bool Open;

        private bool canOpen;
        private List<Line> drawCommands;

        public NarBox()
        {
            drawCommands = new List<Line>();
            Log("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Turpis egestas integer eget aliquet nibh praesent. Montes nascetur ridiculus mus mauris. Enim praesent elementum facilisis leo vel fringilla est ullamcorper eget. Eget lorem dolor sed viverra ipsum. Lectus proin nibh nisl condimentum id venenatis a condimentum vitae. At auctor urna nunc id. Ornare arcu odio ut sem nulla pharetra diam sit amet. Non odio euismod lacinia at quis risus sed vulputate. Augue ut lectus arcu bibendum. Urna molestie at elementum eu facilisis. Tortor at auctor urna nunc id cursus metus. Vel facilisis volutpat est velit egestas dui. Rhoncus dolor purus non enim. Auctor eu augue ut lectus arcu bibendum.", Color.Orange);
            Log("Hey don't mess with me, I got your money in a bag. A bag you know?", Color.DarkOrange);
        }

        internal void UpdateClosed()
        {
            if (!canOpen)
                canOpen = true;
            else if (MInput.Keyboard.Pressed(Keys.D1))
            {
                Open = true;
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
            Draw.Rect(narboxX, narboxY, narboxWidth, narboxHeight, Color.DarkBlue * OPACITY);

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
