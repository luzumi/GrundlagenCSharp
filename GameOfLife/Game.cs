using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Game : Scene
    {
        readonly GameLogic logic;

        public Game()
        {
            logic = new GameLogic((Intro.size));
        }

        public Game(GameLogic pLogic)
        {
            logic = pLogic;
        }

        public override void Update()
        {
            Console.SetCursorPosition(0, 3);
            bool[,] arrayToDraw = logic.Field;

            for (int row = 0; row < arrayToDraw.GetLength(1); row++)
            {
                for (int column = 0; column < arrayToDraw.GetLength(0); column++)
                {
                    Console.Write("{0}", (arrayToDraw[column, row] ? "▒" : " "));
                }

                Console.WriteLine();
            }
            logic.Update();
        }
    }
}
