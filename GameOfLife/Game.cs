using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Game : Scene
    {
        readonly GameLogic logic;
        private string item;

        public Game()
        {
            logic = new GameLogic((GameLogic.size));
        }

        public Game(GameLogic pLogic)
        {
            logic = pLogic;
        }

        public Game(string item)
        {
            this.item = item;
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

        public override void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
