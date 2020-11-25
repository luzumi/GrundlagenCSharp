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

        void EventNewGame()
        {
            Program.SceneAdd(new Game());
        }


        byte activeButtonID = 0;

        //private StringBuilder logo = new StringBuilder(Program.Logo());
        private Random rand = new Random();

        public MainMenue()
        {
        
            byte row = 12;
            buttons = new List<Button>
            {
                new Button(row, true, "Random Game", () => Program.SceneAdd(new Game())),
                new Button(row += 2, true, "Create a Game", () => Program.SceneAdd(new Editor())),
                new Button(row += 2, true, "Load Game", () => Program.SceneAdd(new LoadGame())),
                new Button(row += 2, true, "Quit Game", () => Program.running = false)
            };

            labels = new List<Label>();

            PrintLogo();
            Activate();
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
                    LogoLines.Add(newLine);
                }

                labels.Add(new Label(1, true, LogoLines));
            }
        }


        public override void Update()
        {
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
                        Program.SceneRemove();
                        break;
                    case ConsoleKey.Enter:
                        buttons[ActiveButtonID].Execute();
                        // Todo: switch for buttonID to react to user choice or delegate!
                        break;
                }
            }
        }


        //public void UpdateAlt()
        //{
        //    foreach (var item in needsRedraw)
        //    {
        //        item.Draw();
        //    }

        //    needsRedraw.Clear();

        //    int row = 0;
        //    Console.ResetColor();
        //    for (; row < LogoLines.Count; row++)
        //    {
        //        Console.SetCursorPosition(Console.WindowWidth / 2 - LogoLines[row].Length / 2, 2 + row);
        //        Console.Write(LogoLines[row]);
        //    }

        //    if (Console.KeyAvailable)
        //    {
        //        switch (Console.ReadKey(true).Key)
        //        {
        //            case ConsoleKey.UpArrow:
        //                ActiveButtonID--;
        //                break;
        //            case ConsoleKey.DownArrow:
        //                ActiveButtonID++;
        //                break;
        //            case ConsoleKey.Escape:
        //                Program.Scenes.Pop();
        //                break;
        //            case ConsoleKey.Enter:
        //                ClearScreen();
        //                switch (ActiveButtonID)
        //                {
        //                    case 0:
        //                        Program.Scenes.Push(new Game());
        //                        break;
        //                    case 1:
        //                        Program.Scenes.Push(new Editor());
        //                        break;
        //                    case 2:
        //                        Program.Scenes.Push(new LoadGame());
        //                        break;
        //                    case 3:
        //                        Program.Scenes.Pop();
        //                        Program.running = false;
        //                        break;
        //                }
        //                break;
        //        }
        //    }
        //}

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            Program.NeedsRedraw.AddRange(buttons);
            Program.NeedsRedraw.AddRange(labels);
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
                for (int row = 0; row < LogoLines[0].Length * LogoLines.Count; row += rand.Next(0, 3))
                {
                    if (backup[row] != ' ' && backup[row] != '\n')
                    {
                        backup[row] = ' ';
                    }
                }

                for (int j = 0; j < LogoLines.Count; j++)
                {
                    temp = backup.ToString().Substring(89 * j, 89);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - LogoLines[0].Length / 2, 2 + j);
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
    }
}
