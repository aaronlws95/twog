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
    public class MainMenu : Scene
    {
        public int Selection;

        public MenuButton StartButton;
        public MenuButton OptionsButton;
        public MenuButton ExitButton;

        public List<MenuButton> Buttons;

        public const int StStart = 0;
        public const int StOptions = 1;
        public const int StExit = 2;

        public MainMenu() : base()
        {
            Buttons = new List<MenuButton>();
            Selection = 0;
        }


        public override void Begin()
        {
            base.Begin();
            StartButton = new MenuButton(new Vector2(230, 30), "start", true);
            Add(StartButton);

            OptionsButton = new MenuButton(new Vector2(230, 90), "options", true);
            Add(OptionsButton);

            ExitButton = new MenuButton(new Vector2(230, 150), "exit", true);
            Add(ExitButton);

            Buttons.Add(StartButton);
            Buttons.Add(OptionsButton);
            Buttons.Add(ExitButton);
            StartButton.Sprite.Play("selected");

            EverythingRenderer er = new EverythingRenderer();
            Add(er);
        }

        public void ToggleSelected()
        {
            for (int i = 0; i<Buttons.Count; i++)
            {
                if (Selection == i)
                    Buttons[i].Sprite.Play("selected");
                else
                    Buttons[i].Sprite.Play("default");
            }
        }

        public override void Update()
        {
            base.Update();

            if (MInput.Keyboard.Pressed(Keys.Up))
            {
                Selection -= 1;
                if (Selection < 0)
                    Selection = Buttons.Count - 1;
                ToggleSelected();
            }

            else if (MInput.Keyboard.Pressed(Keys.Down))
            {
                Selection += 1;
                if (Selection >= Buttons.Count)
                    Selection = 0;
                ToggleSelected();
            }

            if(MInput.Keyboard.Pressed(Keys.Enter))
            {
                switch (Selection)
                {
                    case StStart:
                        Engine.Scene = new Scene0();
                        break;
                    case StOptions:
                        break;
                    case StExit:
                        Engine.Instance.Exit();
                        break;
                }
            }
        }

    }
}
