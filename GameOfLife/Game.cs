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

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];

            

            for (int row = 0; row < boardLabels.GetLength(0); row++)
            {
                for (int col = 0; col < boardLabels.GetLength(1); col++)
                {
                    boardLabels[row, col] = new BoardLabel(row + 2, col );
                }
            }
        }

        public Game(GameLogic pLogic)
        {
            lastLogicUpdate = DateTime.Now;
            logic = pLogic;
            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];

            for (int row = 0; row < boardLabels.GetLength(0); row++)
            {
                for (int col = 0; col < boardLabels.GetLength(1); col++)
                {
                    boardLabels[row, col] = new BoardLabel(row, col );
                }
            }
        }

        public Game(string pFileName)
        {
            lastLogicUpdate = DateTime.Now;
            
            logic = new GameLogic(GameLogic.size);

            logic.LoadGame(pFileName);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];


            for (int row = 0; row < boardLabels.GetLength(0); row++)
            {
                for (int col = 0; col < boardLabels.GetLength(1); col++)
                {
                    boardLabels[row, col] = new BoardLabel(row, col );
                }
            }
        }

        

        public override void Update()
        {
            bool[,] arrayToDraw = logic.Field;

            for (int row = 0; row < arrayToDraw.GetLength(0); row++)
            {
                for (int col = 0; col < arrayToDraw.GetLength(1); col++)
                {
                    boardLabels[row, col].Alive = arrayToDraw[row, col];
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
            foreach (var item in boardLabels)
            {
                Program.NeedsRedraw.Add(item);
            }
        }
    }
}
