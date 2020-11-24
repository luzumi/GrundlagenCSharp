using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    class Intro : Scene
    {
        
        private List<string> LogoLines { get; set; }

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorVisible = false;

            LogoLines = new List<string>();
            LogoLines = PrintLogo();

            Console.SetCursorPosition(33, 28);
            Console.WriteLine("Press 'ESC' to abort the Program");

            Console.SetCursorPosition(27, 15);
            Console.WriteLine("Wie Gross soll das Spielfeld denn sein?");

            Console.SetCursorPosition(GameLogic.size.x == 0 ? 31 : 51,  17);
            Console.Write(GameLogic.size.x == 0 ? "Horizontal: " : "Vertikal: ");

            //ArrayGröße abfragen
            if (Console.KeyAvailable && GameLogic.size.y == 0)
            {
                if (GameLogic.size.x == 0)
                {
                    Int32.TryParse(Console.ReadLine(), out int resultx);
                    {
                        GameLogic.size.x = resultx;
                    }
                }
                else
                {
                    Int32.TryParse(Console.ReadLine(), out int resulty);
                    {
                        GameLogic.size.y = resulty;

                        Console.SetCursorPosition(37, 23);
                        Console.WriteLine("Press [Enter] to Continue");
                    }
                }
            }

            if (Console.KeyAvailable && GameLogic.size.y != 0)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Program.Scenes.Pop();
                    Program.Scenes.Push(new MainMenue());
                    
                    Console.Clear();
                }
            }
            //TODO: animations

        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }


        public List<string> PrintLogo()
        {
            Console.SetCursorPosition(0, 2);
            using (StreamReader reader = new StreamReader("LogoSmall.txt"))
            {
                string newLine;
                while ((newLine = reader.ReadLine()) != null)
                {
                    LogoLines.Add(newLine + "\n");
                }
                for (int row = 0; row < LogoLines.Count; row++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - LogoLines[row].Length / 2, 2 + row);
                    Console.Write(LogoLines[row]);
                }
            }

            return LogoLines;
        }
    }
}
