using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class MainMenue : Scene
    {
        public List<string> LogoLines { get; set; }
        readonly List<Label> labels;
        readonly List<IDrawable> needsRedraw;
        readonly List<Button> inactiveButtons;
        private sbyte activeButton;

        List<Button> menuButtons;

        byte activeButtonID = 0;

        //private StringBuilder logo = new StringBuilder(Program.Logo());
        private Random rand = new Random();

        public MainMenue()
        {
            Console.ResetColor();
            Console.Clear();
            
            menuButtons = new List<Button>
            {
                new Button(10, true, "Random Game"),
                new Button(12, true, "Predefined Game"),
                new Button(14, true, "Load Game"),
                new Button(16, true, "Quit Game")
            };

            menuButtons[2].State = ButtonStates.Inactive;

            needsRedraw = new List<IDrawable>(menuButtons);
            labels = new List<Label>();

            Console.SetCursorPosition(0, 2);

            PrintLogo();

            ActiveButtonID = 0;
        }

        public void PrintLogo()
        {
            Console.SetCursorPosition(0, 2);
            using (StreamReader reader = new StreamReader("LogoSmall.txt"))
            {
                LogoLines = new List<string>();
                string newLine;
                while ((newLine = reader.ReadLine()) != null)
                {
                    LogoLines.Add(newLine + "\n");
                }
                labels.Add(new Label(1, true, LogoLines));
            }
        }


        public override void Update()
        {
            foreach (var item in needsRedraw)
            {
                item.Draw();
            }

            needsRedraw.Clear();

            int row = 0;
            Console.ResetColor();
            for (; row < LogoLines.Count; row++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - LogoLines[row].Length / 2, 2 + row);
                Console.Write(LogoLines[row]);
            }

            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        ActiveButtonID--;
                        break;
                    case ConsoleKey.DownArrow:
                        ActiveButtonID++;
                        break;
                    case ConsoleKey.Escape:
                        Program.Scenes.Pop();
                        break;
                    case ConsoleKey.Enter:
                        ClearScreen();
                        Program.Scenes.Push(new Game());
                        // Todo: switch for buttonID to react to user choice
                        break;
                }
            }
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            needsRedraw.AddRange(inactiveButtons);
            needsRedraw.AddRange(menuButtons);
            needsRedraw.AddRange(labels);
        }

        public static void drawButtons(byte IdActiveButton, List<MenuButton> buttons)
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
                    if (activeButtonID < menuButtons.Count - 1)
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
                    Program.Scenes.Push(new Game());
                    Program.Scenes.Peek().Update();
                    Thread.Sleep(300); //TODO: sleep ersetzen
                    break;
                case 1: //Editor
                    ClearScreen();
                    Program.Scenes.Push(new Editor());
                    break;
                case 2:
                    ClearScreen();
                    Console.ResetColor();
                    Program.Scenes.Push(new LoadGame());
                    Console.WriteLine("Load Game");
                    break;
                case 3:
                    Console.WriteLine("exit");
                    break;
            }
        }

        private void ClearScreen()
        {
            bool ready = false;
            StringBuilder backup = new StringBuilder();
            foreach (string line in LogoLines)
            {
                backup.Append(line);
            }

            while (!ready)
            {
                string temp = "";
                for (int row = 0; row < LogoLines[0].Length*LogoLines.Count; row += rand.Next(0, 3))
                {
                    if (backup[row] != ' ' && backup[row] != '\n')
                    {
                        backup[row] = ' ';
                    }
                }

                for (int j = 0; j < LogoLines.Count; j++)
                {
                    temp = backup.ToString().Substring(89 * j, 89);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - LogoLines[0].Length / 2, 2+j);
                    Console.Write(temp);
                }

                Thread.Sleep(100); //TODO: Sleep ersetzen

                for (int i = 0; i < backup.Length; i++)
                {
                    if (backup[i] == ' ' || backup[i] == '\n')
                    {
                        ready = true;
                    }
                    else
                    {
                        ready = false;
                        break;
                    }
                }

                Thread.Sleep(100); //TODO: Sleep ersetzen
            }

            Console.Clear();
        }


        public sbyte ActiveButtonID
        {
            get { return activeButton; }
            set
            {
                menuButtons[activeButton].State = ButtonStates.Available;
                needsRedraw.Add(menuButtons[activeButton]);
                activeButton = value;
                if (activeButton < 0)
                {
                    activeButton = (sbyte)(menuButtons.Count - 1);
                }
                else if (activeButton == menuButtons.Count)
                {
                    activeButton = 0;
                }

                menuButtons[activeButton].State = ButtonStates.Selected;
                needsRedraw.Add(menuButtons[activeButton]);
            }
        }
    }
}
