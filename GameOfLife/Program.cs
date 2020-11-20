using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        public static Stack<Scene> Scenes = new Stack<Scene>(4);

        public static bool running = true;

        static void Main()
        {
            //Scene intro = new Intro();
            Scene game = new Game();

            Console.CursorVisible = !running;
            do
            {
                Scenes.Peek().Update();
                //Thread.Sleep(300);

                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    running = false;
                    Console.Clear();
                    Console.WriteLine(running);
                }
            } while (running);
        }
    }
}
