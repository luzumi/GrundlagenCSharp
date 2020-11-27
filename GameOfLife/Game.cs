using System;

namespace GameOfLife
{
    class Game : Scene
    {
        readonly GameLogic logic;
        DateTime lastLogicUpdate;
        readonly BoardLabel[,] boardLabels;

        public Game(int template)
        {
            lastLogicUpdate = DateTime.Now;

            logic = new GameLogic(template);

            Console.WindowWidth = logic.FieldFalse.GetLength(1) * 2;

            Console.WindowHeight = logic.FieldFalse.GetLength(0);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];
            
            BoardLabelsFill();
        }


        public Game(GameLogic pLogic)
        {
            lastLogicUpdate = DateTime.Now;

            logic = pLogic;

            Console.WindowWidth = logic.FieldFalse.GetLength(1) * 2;

            Console.WindowHeight = logic.FieldFalse.GetLength(0);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];

            BoardLabelsFill();
        }

        public Game(string pFileName)
        {
            lastLogicUpdate = DateTime.Now;
            
            logic = new GameLogic(0);

            logic.LoadGame(pFileName, SaveGameVariante.Text);

            Console.WindowWidth = logic.FieldFalse.GetLength(1) * 2;

            Console.WindowHeight = logic.FieldFalse.GetLength(0);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];
            
            BoardLabelsFill();
        }


        private void BoardLabelsFill()
        {
            offset = Console.WindowWidth / 2 - GameLogic.size.col;

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
                            logic.SaveGameTxt("Snapshot"+DateTime.Now.ToString("hh-mm-ss"));
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
