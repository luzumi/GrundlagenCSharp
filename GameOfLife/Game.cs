using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Game : Scene
    {
        readonly Spielfeld logic;

        public Game()
        {
            Program.Scenes.Push(this);

            logic ??= new Spielfeld((80, 30));
        }

        public override void Update()
        {
            logic.Update();
            Console.SetCursorPosition(0, 0);
            bool[,] arrayToDraw = logic.Field;

            for (int row = 0; row < arrayToDraw.GetLength(1); row++)
            {
                for (int column = 0; column < arrayToDraw.GetLength(0); column++)
                {
                    Console.Write("{0}", (arrayToDraw[column, row] ? "▒" : " "));
                }

                Console.WriteLine();
            }

            logic.FieldToRead = !logic.FieldToRead;
        }
    }
}
