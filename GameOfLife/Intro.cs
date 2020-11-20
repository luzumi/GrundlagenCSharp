using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Intro : Scene
    {
        public static (int x, int y) size = (20,20);

        public override void Update()
        {

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.SetCursorPosition(20, 5);
            Console.WriteLine("Game of Life");

            Console.SetCursorPosition(20, 10);
            Console.WriteLine("Press 'ESC' to abort the Program");

            Console.SetCursorPosition(20, 12);
            Console.WriteLine("Wie Gross soll das Spielfeld denn sein?");
            
            ////ArrayGröße abfragen
            //if (Console.KeyAvailable)
            //{

                //Console.SetCursorPosition(28, size.x == 0? 13 : 14);
                
                //int number = (int)Console.ReadKey().Key;
                
                //if ((int)Console.ReadKey().Key >= 30 && (int)Console.ReadKey().Key <= 39)
                //{
                    //if (size.x == 0)
                    //{
                        //size.x = number;
                    //}
                    //else if (size.y == 0)
                    //{
                        //size.y = number;
                    //}
                //}
            //}

            Console.SetCursorPosition(20, 17);
            Console.WriteLine("Press Spacebar to Continue");

            //TODO: animations

            if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                Program.Scenes.Pop();
                Program.Scenes.Push(new MainMenue());
                Console.WriteLine("intro");
            }

            
        }
    }
}
