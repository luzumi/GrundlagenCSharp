using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Game : Scene
    {
        readonly GameLogic logic;
        DateTime lastLogicUpdate;
        readonly BoardLabel[,] boardLabels;

        public Game()
        {
            lastLogicUpdate = DateTime.Now;
            logic = new GameLogic(GameLogic.size);

            boardLabels = new BoardLabel[GameLogic.size.x, GameLogic.size.y];

            int offset = Console.WindowWidth / 2 - boardLabels.GetLength(1);

            for (int row = 0; row < boardLabels.GetLength(0); row++)
            {
                for (int col = 0; col < boardLabels.GetLength(1); col++)
                {
                    boardLabels[row, col] = new BoardLabel(row, offset + col * 2);
                }
            }
        }

        public Game(GameLogic pLogic)
        {
            lastLogicUpdate = DateTime.Now;
            logic = pLogic;
            boardLabels = new BoardLabel[GameLogic.size.x, GameLogic.size.y];
            int offset = Console.WindowWidth / 2 - boardLabels.GetLength(1);

            for (int row = 0; row < boardLabels.GetLength(0); row++)
            {
                for (int col = 0; col < boardLabels.GetLength(1); col++)
                {
                    boardLabels[row, col] = new BoardLabel(row, offset + col * 2);
                }
            }
        }

        public Game(string pFileName)
        {
            lastLogicUpdate = DateTime.Now;
            
            logic = new GameLogic(GameLogic.size);

            logic.LoadGame(pFileName);

            boardLabels = new BoardLabel[GameLogic.size.x, GameLogic.size.y];

            int offset = Console.WindowWidth / 2 - boardLabels.GetLength(1);

            for (int row = 0; row < boardLabels.GetLength(0); row++)
            {
                for (int col = 0; col < boardLabels.GetLength(1); col++)
                {
                    boardLabels[row, col] = new BoardLabel(row, offset + col * 2);
                }
            }
        }

        

        public override void Update()
        {
            Console.SetCursorPosition(0, 3);
            bool[,] arrayToDraw = logic.Field;

            for (int row = 0; row < arrayToDraw.GetLength(0); row++)
            {
                for (int column = 0; column < arrayToDraw.GetLength(1); column++)
                {
                    boardLabels[row, column].Alive = arrayToDraw[row, column];
                }
            }

            if (Console.KeyAvailable)
            {
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape:
                            Program.SceneRemove();
                            return;
                        case ConsoleKey.S: // spiel speichern
                            logic.SaveGame("GameA.xml");
                            break;
                    }
                }
            }

            if ((DateTime.Now - lastLogicUpdate).TotalMilliseconds > 500)
            {
                lastLogicUpdate = DateTime.Now;
                logic.Update();
            }
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            Console.Clear();
            foreach (var item in boardLabels)
            {
                Program.NeedsRedraw.Add(item);
            }
        }
    }
}
