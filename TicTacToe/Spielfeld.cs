
namespace TicTacToe
{
    /// <summary>
    /// Ein TicTacToe Spielfeld 3x3
    /// </summary>
    class Spielfeld
    {
        private FieldState[,] board = new FieldState[3, 3];
        private bool currentPlayerID;
        public string[] PlayerNames = new string[2];
        static public Button[,] buttons = new Button[3, 3];
        private int round;

        public FieldState[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        public Spielfeld()
        {
            round = 1;
            for (byte column = 0; column < 3; column++)
            {
                for (byte row = 0; row < 3; row++)
                {
                    buttons[column, row] = new Button(new Point((byte)(column * 2), (byte)(row * 2)));
                    buttons[column, row].FieldState = FieldState.Empty;
                }
            }
        }


        public FieldState[,] GetBoard()
        {
            return board;
        }


        public bool GetPlayerID()
        {
            return currentPlayerID;
        }


        /// <summary>
        /// Prüft aktuellen Zug auf Gültigkeit und Spielende - Sieg/Tie
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <returns></returns>
        public TurnResult Turn(Point pCoordinates)
        {   //Haben aktuelle Positionen bereits X oder O?
            if (buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.X ||
                buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.O)
            {
                return TurnResult.Invalid;
            }

            //Auswahl des zu schreibenden Spielsteins
            if (GetPlayerID())
            {
                buttons[pCoordinates.X, pCoordinates.Y].FieldState = FieldState.X;
            }
            else
            {
                buttons[pCoordinates.X, pCoordinates.Y].FieldState = FieldState.O;
            }

            //prüfen ob Ein Siegzug gemacht wurde
            if (round > 1 &&
                //Prüfe vertikal
                ((buttons[pCoordinates.X, 0].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 0].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 2].FieldState) ||

                 //Prüfe Vertikal
                 (buttons[0, pCoordinates.Y].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[0, pCoordinates.Y].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[1, pCoordinates.Y].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[2, pCoordinates.Y].FieldState) ||

                 //Prüfe Diagonal
                 (buttons[1, 1].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[0, 0].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[1, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[2, 2].FieldState) ||
                 (buttons[1, 1].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[0, 2].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[1, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[2, 0].FieldState)))
            {
                return TurnResult.Win;
            }

            //Letzte Runde (9) ergibt das Unentschieden
            if (round == 9)
            {
                return TurnResult.Tie;
            }

            round++;
            currentPlayerID = !currentPlayerID; 
            return TurnResult.Valid;
        }

        public TurnResult Turn(byte pFieldNumber)
        {
            return TurnResult.Invalid;
        }

        public void DrawHint(bool pPlayerID)
        {
        }

        public byte GetHint(bool pPlayerID)
        {
            return new byte();
        }

        
    }
}
