using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class MainMenue : Scene
    {

        public override void Update()
        {
            //Dastellung mainMenue

            if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Program.Scenes.Pop();
                Program.Scenes.Push(new Game());
                Console.WriteLine("Game");
            }
        }
    }
}
