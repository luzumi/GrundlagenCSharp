namespace TicTacToe
{
    /// <summary>
    /// Ein TicTacToe Spielfeld 3x3
    /// </summary>
    class Spielfeld
    {
        private FieldState[,] board = new FieldState[3, 3];
        private bool currentPlayerID;
        public readonly string[] PlayerNames = new string[2];
        public static readonly Button[,] buttons = new Button[3, 3];
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
                    buttons[column, row] =
                        new Button(new Point((byte)(column * 2), (byte)(row * 2))) {FieldState = FieldState.Empty};
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
        {
            //Haben aktuelle Positionen bereits X oder O?
            if (buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.X ||
                buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.O)
            {
                return TurnResult.Invalid;
            }

            //Auswahl des zu schreibenden Spielsteins
            buttons[pCoordinates.X, pCoordinates.Y].FieldState = GetPlayerID() ? FieldState.X : FieldState.O;

            //prüfen ob Ein Siegzug gemacht wurde
            if (round > 1 &&
                //Prüfe vertikal
                ((buttons[pCoordinates.X, pCoordinates.Y].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 0].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 2].FieldState) ||

                 //Prüfe Vertikal
                 (buttons[pCoordinates.X, pCoordinates.Y].FieldState != FieldState.Empty &&
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



        /// <summary>
        /// Ermittlung der möglichen Löungsvorschläge
        /// 
        /// <code>+-----+-----+-----+</code>
        /// <code>| 0,0 | 1,0 | 2,0 |</code>
        /// <code>+-----+-----+-----+</code>
        /// <code>| 0,1 | 1,1 | 2,1 |</code>
        /// <code>+-----+-----+-----+</code>
        /// <code>| 0,2 | 1,2 | 2,2 |</code>
        /// <code>+-----+-----+-----+</code>
        /// </summary>
        /// <param name="pPlayerID"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void DrawHint(int row, int column)
        {
            var notTheActualPlayer = !GetPlayerID() ? FieldState.X : FieldState.O;
            var activePlayerColor = buttons[row, column].FieldState;

            //if(buttons[row, column].FieldState == activePlayerColor)

            #region case 0,0

           
            if (row == 0 && column == 0)
            {   //  == 0,0 -> 1,0 -> 2,0
                if (buttons[0, 0].FieldState == buttons[1, 0].FieldState ||
                    buttons[1, 0].FieldState != notTheActualPlayer) // O != X        E != X
                {   
                    if (buttons[0, 0].FieldState == buttons[2, 0].FieldState ||
                        buttons[2, 0].FieldState != notTheActualPlayer)
                    {
                        //IsEmptySetHint(0,0, activePlayerColor);
                        IsEmptySetHint(1,0, activePlayerColor);
                        IsEmptySetHint(2, 0, activePlayerColor);
                    }
                }
                //  == 0,0 -> 0,1 -> 0,2
                if (buttons[0, 0].FieldState == buttons[0, 1].FieldState ||
                    buttons[0, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[0, 0].FieldState == buttons[1, 2].FieldState ||
                        buttons[1, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 1, activePlayerColor);
                        IsEmptySetHint(0, 2, activePlayerColor);
                    }
                }
                //  == 0,0 -> 1,1 -> 2,2
                if (buttons[0, 0].FieldState == buttons[1, 1].FieldState )
                    // buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[0, 0].FieldState == buttons[2, 2].FieldState ||
                        buttons[2, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 0, activePlayerColor);
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(2, 2, activePlayerColor);
                    }
                }
            }
            #endregion

            #region case 0,1

            //case 0,1
            if (row == 0 && column == 1)
            {
                if (buttons[0, 1].FieldState == buttons[0, 0].FieldState ||
                    buttons[0, 0].FieldState != notTheActualPlayer)
                {   //  == 0,1 -> 0,0 -> 0,2
                    if (buttons[0, 1].FieldState == buttons[0, 2].FieldState ||
                        buttons[0, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 0, activePlayerColor);
                        IsEmptySetHint(0, 2, activePlayerColor);
                    }
                }
                //  == 0,1 -> 1,1 -> 2,1                            
                if (buttons[0, 1].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[0, 1].FieldState == buttons[2, 1].FieldState ||
                        buttons[2, 1].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 1, activePlayerColor);
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(2, 1, activePlayerColor);
                    }
                }
            }
            #endregion

            #region case 0,1

            //case 0,2                          
            if (row == 0 && column == 2)
            {   //  == 0,2 -> 0,1 -> 0,0  
                if (buttons[0, 2].FieldState == buttons[0, 1].FieldState ||
                    buttons[0, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[0, 2].FieldState == buttons[0, 0].FieldState ||
                        buttons[0, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 1, activePlayerColor);
                        IsEmptySetHint(0, 0, activePlayerColor);
                    }
                }
                //  == 0,2 -> 1,2 -> 2,2
                if (buttons[0, 2].FieldState == buttons[1, 2].FieldState ||
                    buttons[1, 2].FieldState != notTheActualPlayer)
                {
                    if (buttons[0, 2].FieldState == buttons[2, 2].FieldState ||
                        buttons[2, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 2, activePlayerColor);
                        IsEmptySetHint(2, 2, activePlayerColor);
                    }
                }
                //  == 0,2 -> 1,1 -> 2.0
                if (buttons[0, 2].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[0, 2].FieldState == buttons[2, 0].FieldState ||
                        buttons[2, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 2, activePlayerColor);
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(2, 0, activePlayerColor);
                    }
                }
            }

            #endregion

            #region case 1,0

            // case 1,0
            if (row == 1 && column == 0)
            {   //  == 1,0 -> 0,0 -> 2,0
                if (buttons[1, 0].FieldState == buttons[0, 0].FieldState ||
                    buttons[0, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 0].FieldState == buttons[2, 0].FieldState ||
                        buttons[2, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 0, activePlayerColor);
                        IsEmptySetHint(2, 0, activePlayerColor);
                    }
                }
                //  == 1,0 -> 1,1 -> 1,2
                if (buttons[1, 0].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 0].FieldState == buttons[1, 2].FieldState ||
                        buttons[1, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 0, activePlayerColor);
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(1, 2, activePlayerColor);
                    }
                }
            }
            
            #endregion

            #region case 1,1

            // case 1,1
            if (row == 1 && column == 1)
            {
                //  == 1,1 -> 1,0 -> 1,2
                if (buttons[1, 1].FieldState == buttons[1, 0].FieldState ||
                    buttons[1, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 1].FieldState == buttons[1, 2].FieldState ||
                        buttons[1, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 0, activePlayerColor);
                        IsEmptySetHint(1, 2, activePlayerColor);
                    }
                }
                //  == 1,1 -> 0,0 -> 2,2
                if (buttons[1, 1].FieldState == buttons[0, 0].FieldState ||
                    buttons[0, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 1].FieldState == buttons[2, 2].FieldState ||
                        buttons[2, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 0, activePlayerColor);
                        IsEmptySetHint(2, 2, activePlayerColor);
                    }
                }
                //  == 1,1 -> 1,0 -> 0,2
                if (buttons[1, 1].FieldState == buttons[1, 0].FieldState ||
                    buttons[1, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 1].FieldState == buttons[0, 2].FieldState)
                        //buttons[0, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 0, activePlayerColor);
                        IsEmptySetHint(0, 2, activePlayerColor);
                    }
                }
                //  == 1,1 -> 2,1 -> 0,1
                if (buttons[1, 1].FieldState == buttons[2, 1].FieldState ||
                    buttons[2, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 1].FieldState == buttons[2, 1].FieldState ||
                        buttons[2, 1].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(2, 1, activePlayerColor);
                        IsEmptySetHint(0, 1, activePlayerColor);
                    }
                }
                //  == 1,1 -> 2,0 -> 0,0
                if (buttons[1, 1].FieldState == buttons[2, 0].FieldState ||
                    buttons[2, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 1].FieldState == buttons[0, 2].FieldState ||
                        buttons[0, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 0, activePlayerColor);
                        IsEmptySetHint(2, 0, activePlayerColor);
                        IsEmptySetHint(0, 2, activePlayerColor);
                    }
                }
            }

            #endregion

            #region  case 1,2

            // case 1,2
            if (row == 1 && column == 2)
            {   //  == 1,2 -> 0,2 -> 2,2
                if (buttons[1, 2].FieldState == buttons[0, 2].FieldState ||
                    buttons[0, 2].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 2].FieldState == buttons[2, 2].FieldState ||
                        buttons[2, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(0, 2, activePlayerColor);
                        IsEmptySetHint(2, 2, activePlayerColor);
                    }
                }
                //  == 1,2 -> 1,1 -> 1,0
                if (buttons[1, 2].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[1, 2].FieldState == buttons[1, 0].FieldState ||
                        buttons[1, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 2, activePlayerColor);
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(1, 0, activePlayerColor);
                    }
                }
            }

            #endregion

            #region case 2,0

            // case 2,0
            if (row == 2 && column == 0)
            {   //  == 2,0 -> 1,0 -> 0,0
                if (buttons[2, 0].FieldState == buttons[1, 0].FieldState ||
                    buttons[1, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[2, 0].FieldState == buttons[0, 0].FieldState ||
                        buttons[0, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 0, activePlayerColor);
                        IsEmptySetHint(0, 0, activePlayerColor);
                    }
                }
                //  == 2,0 -> 1,1 -> 0,2
                if (buttons[2, 0].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[2, 0].FieldState == buttons[0, 2].FieldState ||
                        buttons[0, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(0, 2, activePlayerColor);
                    }
                }
                //  == 2,0 -> 2,1 -> 2,2
                if (buttons[2, 0].FieldState == buttons[2, 1].FieldState ||
                    buttons[2, 1].FieldState != notTheActualPlayer)
                {   
                    if (buttons[2, 0].FieldState == buttons[2, 2].FieldState ||
                        buttons[2, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(2, 0, activePlayerColor);
                        IsEmptySetHint(2, 1, activePlayerColor);
                        IsEmptySetHint(2, 2, activePlayerColor);
                    }
                }
            }

            #endregion

            #region case 2,1

            // case 2,1
            if (row == 2 && column == 1)
            {
                //  == 2,1 -> 2,0 -> 2,2
                if (buttons[2, 1].FieldState == buttons[2, 0].FieldState ||
                    buttons[2, 0].FieldState != notTheActualPlayer)
                {
                    if (buttons[2, 1].FieldState == buttons[2, 2].FieldState ||
                        buttons[2, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(2, 0, activePlayerColor);
                        IsEmptySetHint(2, 2, activePlayerColor);
                    }
                }
                //  == 2,1 -> 1,1 -> 0,1
                if (buttons[2, 1].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[2, 1].FieldState == buttons[0, 1].FieldState ||
                        buttons[0, 1].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(2, 1, activePlayerColor);
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(0, 1, activePlayerColor);
                    }
                }
            }

            #endregion

            #region  case 2,2

            // case 2,2
            if (row == 2 && column == 2)
            {   //  == 2,2 -> 1,2 -> 0,2
                if (buttons[2, 2].FieldState == buttons[1, 2].FieldState ||
                    buttons[1, 2].FieldState != notTheActualPlayer)
                {
                    if (buttons[2, 2].FieldState == buttons[0, 2].FieldState ||
                        buttons[0, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 2, activePlayerColor);
                        IsEmptySetHint(0, 2, activePlayerColor);
                    }
                }
                //  == 2,2 -> 1,1 -> 0,0
                if (buttons[2, 2].FieldState == buttons[1, 1].FieldState ||
                    buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (buttons[2, 2].FieldState == buttons[0, 0].FieldState ||
                        buttons[0, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 1, activePlayerColor);
                        IsEmptySetHint(0, 0, activePlayerColor);
                    }
                }
                //  == 2,2 -> 2,1 -> 2.0
                if (buttons[2, 2].FieldState == buttons[2, 1].FieldState ||
                    buttons[2, 1].FieldState != notTheActualPlayer)
                {   
                    if (buttons[2, 2].FieldState == buttons[2, 0].FieldState ||
                        buttons[2, 0].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(2, 2, activePlayerColor);
                        IsEmptySetHint(2, 1, activePlayerColor);
                        IsEmptySetHint(2, 0, activePlayerColor);
                    }
                }
            }

            #endregion

            buttons[row, column].FieldState = activePlayerColor;
        }


        /// <summary>
        /// Setzt ausgewähltes feld auf Hint, wenn Empty. sonst bleibt aktueller Status bestehen
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="aktivePlayerColor"></param>
        private static void IsEmptySetHint(int column, int row, FieldState aktivePlayerColor)
        {
            
            buttons[column, row].FieldState = buttons[column, row].FieldState == FieldState.Empty ? FieldState.Hint : buttons[column, row].FieldState;
            
            //buttons[column, row].FieldState = buttons[column, row].FieldState == aktivePlayerColor ? FieldState.Hint : buttons[column, row].FieldState;
        }

        public byte GetHint(bool pPlayerID)
        {
            return new byte();
        }
    }
}
