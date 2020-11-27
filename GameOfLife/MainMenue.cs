using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class MainMenue : Scene
    {
        public List<string> LogoLines { get; set; }
        readonly List<Label> labels;
        List<(int y, int x)> ClearCoordinates = new List<(int y, int x)>();
        private DateTime AnimationStart;
        private int _MaxFillCoordinates;
        private Random rand = new Random();
        private bool animationRunning;
        

        public MainMenue()
        {
            byte row = 12;
            uiElements = new List<UiElement>
            {
                new Button(row, true, "Random Game", () => Program.SceneAdd(new Game(rand.Next(1, 3))))
                {
                    State = ButtonStates.Available
                },
                new Button(row += 2, true, "Create a Game", () => Program.SceneAdd(new Editor()))
                {
                    State = ButtonStates.Available
                },
                new Button(row += 2, true, "Load Game", () => Program.SceneAdd(new LoadGame()))
                {
                    State = ButtonStates.Available
                },
                // ReSharper disable once RedundantAssignment
                new Button(row += 2, true, "Quit Game", () => Program.running = false)
                {
                    State = ButtonStates.Available
                }
            };

            labels = new List<Label>();

            PrintLogo();
            Activate();
        }


        /// <summary>
        /// liest Logo Aus txt-Datei ein und erzeugt je zeile einen Eintrag in die LabelListe
        /// </summary>
        public void PrintLogo()
        {
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

        /// <summary>
        /// fängt Usereingaben ab und verarbeitet sie 
        /// </summary>
        public override void Update()
        {
            if (animationRunning)
            {
                ClearScreen();
                return;
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
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
                        animationRunning = true;
                        AnimationStart = DateTime.Now;
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
            Program.NeedsRedraw.AddRange(uiElements);
            Program.NeedsRedraw.AddRange(labels);
            animationRunning = false;
            FillClearCoordiantes();
        }

        private void FillClearCoordiantes()
        {

            for ( int row = 0; row < LogoLines.Count; row++)
            {
                for (int col = 0; col < LogoLines[0].Length; col++)
                {
                    ClearCoordinates.Add((row + 1, col + Console.WindowWidth / 2 - LogoLines[0].Length/2));
                }
            }

            _MaxFillCoordinates = ClearCoordinates.Count;
        }


        private void ClearScreen()
        {
            double percentToFinish = 1/1000d * (DateTime.Now - AnimationStart).TotalMilliseconds;

            percentToFinish = (percentToFinish > 1 ? 1 : percentToFinish);

            int numberToFinish =  (int)(_MaxFillCoordinates * percentToFinish)- 
                                 (_MaxFillCoordinates - ClearCoordinates.Count);


            for (int i = 0; i < numberToFinish; i++)
            {
                int elementToDelete = rand.Next(ClearCoordinates.Count);

                Console.SetCursorPosition(ClearCoordinates[elementToDelete].x, ClearCoordinates[elementToDelete].y);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
                ClearCoordinates.RemoveAt(elementToDelete);
            }

            if (ClearCoordinates.Count == 0)
            {
                animationRunning = false;

                ConsoleKeyInfo enter = new ConsoleKeyInfo( ' ', ConsoleKey.Enter, false, false, false);
                
                uiElements[ActiveButtonID].ProcessKey(enter);
            }
        }
    }
}
