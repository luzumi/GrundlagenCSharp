using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Intro : Scene
    {
        public static (int x, int y) size;

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 3);
            Console.WriteLine(Program.Logo());

            Console.SetCursorPosition(33, 28);
            Console.WriteLine("Press 'ESC' to abort the Program");

            Console.SetCursorPosition(27, 15);
            Console.WriteLine("Wie Gross soll das Spielfeld denn sein?");

            Console.SetCursorPosition(size.x == 0 ? 31 : 51,  17);
            Console.Write(size.x == 0 ? "Horizontal: " : "Vertikal: ");

            //ArrayGröße abfragen
            if (Console.KeyAvailable && size.y == 0)
            {
                if (size.x == 0)
                {
                    Int32.TryParse(Console.ReadLine(), out int resultx);
                    {
                        size.x = resultx;
                    }
                }
                else
                {
                    Int32.TryParse(Console.ReadLine(), out int resulty);
                    {
                        size.y = resulty;

                        Console.SetCursorPosition(37, 23);
                        Console.WriteLine("Press [Enter] to Continue");
                    }
                }
            }

            if (Console.KeyAvailable && size.y != 0)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Program.Scenes.Pop();
                    Program.Scenes.Push(new MainMenue());
                    //Program.Scenes.Push(new Game());
                    Console.Clear();
                }
            }
            //TODO: animations

        }

    }
}
