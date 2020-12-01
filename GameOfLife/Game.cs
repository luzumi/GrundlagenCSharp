using System;
using GameOfLifeLogic;

namespace GameOfLife
{
    class Game : Scene
    {
        DateTime lastLogicUpdate;
        readonly BoardLabel[,] boardLabels;

        /// <summary>
        /// Konstruktor für leeres Spielfeld [0] oder mit vorgefertigtem Spielfeld [1],[2]
        /// </summary>
        /// <param name="template">[0] leeres Spielfeld, [1]/[2] vorgefertigte Spielfelder</param>
        public Game(int template)
        {
            lastLogicUpdate = DateTime.Now;

            _saveGameLogic = new GameLogic(template);

            Console.WindowWidth = _saveGameLogic.FieldFalse.GetLength(1) * 2;

            Console.WindowHeight = _saveGameLogic.FieldFalse.GetLength(0);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];
            
            BoardLabelsFill();
        }

        /// <summary>
        /// Konstruktor übernimmt entworfenes Spielfeld im Editor
        /// </summary>
        /// <param name="pLogic"></param>
        public Game(GameLogic pLogic)
        {
            lastLogicUpdate = DateTime.Now;

            _saveGameLogic = pLogic;

            Console.WindowWidth = _saveGameLogic.FieldFalse.GetLength(1) * 2;

            Console.WindowHeight = _saveGameLogic.FieldFalse.GetLength(0);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];

            BoardLabelsFill();
        }


        /// <summary>
        /// Konstruktor für Aufruf mit Dateinamen eines zu ladenden Spielstandes
        /// </summary>
        /// <param name="pFileName"></param>
        public Game(string pFileName)
        {
            lastLogicUpdate = DateTime.Now;

            _saveGameLogic = new GameLogic(0);

            _saveGameLogic.LoadGame(pFileName);

            Console.WindowWidth = _saveGameLogic.FieldFalse.GetLength(1) * 2;

            Console.WindowHeight = _saveGameLogic.FieldFalse.GetLength(0);

            boardLabels = new BoardLabel[GameLogic.size.row, GameLogic.size.col];
            
            BoardLabelsFill();
        }


        /// <summary>
        /// füllt BoardLabels mit den jeweiligen ´x,y coordinaten
        /// </summary>
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
            bool[,] arrayToDraw = _saveGameLogic.Field;

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
                            _saveGameLogic.SaveGameTxt("Snapshot"+DateTime.Now.ToString("hh-mm-ss"));
                            break;
                    }
                }
            }

            if ((DateTime.Now - lastLogicUpdate).TotalMilliseconds > 30)
            {
                lastLogicUpdate = DateTime.Now;
                _saveGameLogic.Update();
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
