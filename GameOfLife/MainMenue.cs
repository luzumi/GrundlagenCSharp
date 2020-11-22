using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class MainMenue : Scene
    {
        List<FieldButton> fieldButtons;
        byte activeButtonID = 0;
        private StringBuilder logo = new StringBuilder(Program.Logo());
        private Random rand = new Random();

        public MainMenue()
        {
            Console.CursorVisible = false;
            fieldButtons = new List<FieldButton>(4);
            fieldButtons.Add(new FieldButton("Play Level"));
            fieldButtons.Add(new FieldButton("Create new Level"));
            fieldButtons.Add(new FieldButton("Edit existing Level"));
            fieldButtons.Add(new FieldButton("Exit"));
        }

        public override void Update()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(Program.Logo());

            drawButtons(activeButtonID, fieldButtons);
            if (Console.KeyAvailable) GetInput(Console.ReadKey(true).Key);

            //if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
            //{
            //    Program.Scenes.Pop();
            //    Program.Scenes.Push(new Game());
            //    Console.WriteLine("Game");
            //}
        }


        static void drawButtons(byte IdActiveButton, List<FieldButton> buttons)
        {
            for (int counter = 0; counter < buttons.Count; counter++)
            {
                Console.SetCursorPosition(37, 16 + 2 * counter);

                if (counter == IdActiveButton)
                {
                    // ausgewählter button
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" -> " + buttons[counter].MenueText);
                    Console.ResetColor();
                }
                else
                {
                    // nicht ausgewählt
                    Console.Write("    " + buttons[counter].MenueText);
                }
            }
        }


        public void GetInput(ConsoleKey pConsoleKey)
        {
            switch (pConsoleKey)
            {
                case ConsoleKey.UpArrow:
                    if (activeButtonID > 0)
                        activeButtonID--;
                    break;
                case ConsoleKey.DownArrow:
                    if (activeButtonID < fieldButtons.Count - 1)
                        activeButtonID++;
                    break;
                case ConsoleKey.Enter:
                    SelectScene();
                    break;
            }
        }

        private void SelectScene()
        {
            switch (activeButtonID)
            {
                case 0:
                    ClearScreen();
                    Program.Scenes.Pop();
                    Program.Scenes.Push(new Game());
                    Program.Scenes.Peek().Update();
                    Thread.Sleep(300); //TODO: sleep ersetzen
                    break;
                case 1:
                    Console.WriteLine("create");
                    break;
                case 2:
                    Console.WriteLine("edit");
                    break;
                case 3:
                    Console.WriteLine("exit");
                    break;
            }
        }

        private void ClearScreen()
        {
            bool ready = false;
            while (!ready)
            {
                Console.SetCursorPosition(0, 3);

                for (int i = 0; i < logo.Length; i += rand.Next(0, 5))
                {
                    if (logo[i] != ' ' && logo[i] != '\n')
                    {
                        logo[i] = ' ';
                    }
                }

                Console.WriteLine(logo);

                Thread.Sleep(100); //TODO: Sleep ersetzen

                for (int i = 0; i < logo.Length; i++)
                {
                    if (logo[i] == ' ' || logo[i] == '\n')
                    {
                        ready = true;
                    }
                    else
                    {
                        ready = false;
                        break;
                    }
                }
            }

            Console.Clear();
        }
    }
}
