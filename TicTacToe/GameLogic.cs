namespace TicTacToe
{
    /// <summary>
    /// Ein TicTacToe GameLogic 3x3
    /// </summary>
    class GameLogic
    {
        private bool _currentPlayerId;
        public static readonly string[] PlayerNames = new string[2];
        public static readonly Button[,] Buttons = new Button[3, 3];
        private int _round;

        private FieldState[,] Board { get; set; } = new FieldState[3, 3];

        public GameLogic()
        {
            _round = 1;
            for (byte column = 0; column < 3; column++)
            {
                for (byte row = 0; row < 3; row++)
                {
                    Buttons[column, row] =
                        new Button(new Point((byte)(column * 4), (byte)(row * 2))) {FieldState = FieldState.Empty};
                }
            }
        }


        public FieldState[,] GetBoard()
        {
            return Board;
        }


        public bool GetPlayerID()
        {
            return _currentPlayerId;
        }


        /// <summary>
        /// Prüft aktuellen Zug auf Gültigkeit und Spielende - Sieg/Tie
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <returns></returns>
        public TurnResult Turn(Point pCoordinates)
        {
            //Haben aktuelle Positionen bereits X oder O?
            if (Buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.X ||
                Buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.O)
            {
                return TurnResult.Invalid;
            }

            //Auswahl des zu schreibenden Spielsteins
            Buttons[pCoordinates.X, pCoordinates.Y].FieldState = GetPlayerID() ? FieldState.X : FieldState.O;

            //prüfen ob Ein Siegzug gemacht wurde
            if (_round > 1 &&
                //Prüfe vertikal
                ((Buttons[pCoordinates.X, pCoordinates.Y].FieldState != FieldState.Empty &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[pCoordinates.X, 0].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[pCoordinates.X, 1].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[pCoordinates.X, 2].FieldState) ||

                 //Prüfe Vertikal
                 (Buttons[pCoordinates.X, pCoordinates.Y].FieldState != FieldState.Empty &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[0, pCoordinates.Y].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[1, pCoordinates.Y].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[2, pCoordinates.Y].FieldState) ||

                 //Prüfe Diagonal
                 (Buttons[1, 1].FieldState != FieldState.Empty &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[0, 0].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[1, 1].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[2, 2].FieldState) ||

                 (Buttons[1, 1].FieldState != FieldState.Empty &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[0, 2].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[1, 1].FieldState &&
                  Buttons[pCoordinates.X, pCoordinates.Y].FieldState == Buttons[2, 0].FieldState)))
            {
                return TurnResult.Win;
            }

            //Letzte Runde (9) ergibt das Unentschieden
            if (_round == 9)
            {
                Program.programState = 3;
                return TurnResult.Tie;
            }

            _round++;
            _currentPlayerId = !_currentPlayerId;
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
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void DrawHint(int row, int column)
        {
            var notTheActualPlayerColor = !GetPlayerID() ? FieldState.X : FieldState.O;
            var activePlayerColor = Buttons[row, column].FieldState;

            //if(buttons[row, column].FieldState == activePlayerColor)

            #region case 0,0

           
            if (row == 0 && column == 0)
            {   //  == 0,0 -> 1,0 -> 2,0
                if (Buttons[0, 0].FieldState == Buttons[1, 0].FieldState ||
                    Buttons[1, 0].FieldState != notTheActualPlayerColor) // O != X        E != X
                {   
                    if (Buttons[0, 0].FieldState == Buttons[2, 0].FieldState ||
                        Buttons[2, 0].FieldState != notTheActualPlayerColor)
                    {
                        //IsEmptySetHint(0,0, activePlayerColor);
                        IsEmptySetHint(1,0);
                        IsEmptySetHint(2, 0);
                    }
                }
                //  == 0,0 -> 0,1 -> 0,2
                if (Buttons[0, 0].FieldState == Buttons[0, 1].FieldState ||
                    Buttons[0, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[0, 0].FieldState == Buttons[1, 2].FieldState ||
                        Buttons[1, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 1);
                        IsEmptySetHint(0, 2);
                    }
                }
                //  == 0,0 -> 1,1 -> 2,2
                if (Buttons[0, 0].FieldState == Buttons[1, 1].FieldState )
                    // buttons[1, 1].FieldState != notTheActualPlayer)
                {
                    if (Buttons[0, 0].FieldState == Buttons[2, 2].FieldState ||
                        Buttons[2, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 0);
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(2, 2);
                    }
                }
            }
            #endregion

            #region case 0,1

            //case 0,1
            if (row == 0 && column == 1)
            {
                if (Buttons[0, 1].FieldState == Buttons[0, 0].FieldState ||
                    Buttons[0, 0].FieldState != notTheActualPlayerColor)
                {   //  == 0,1 -> 0,0 -> 0,2
                    if (Buttons[0, 1].FieldState == Buttons[0, 2].FieldState ||
                        Buttons[0, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 0);
                        IsEmptySetHint(0, 2);
                    }
                }
                //  == 0,1 -> 1,1 -> 2,1                            
                if (Buttons[0, 1].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[0, 1].FieldState == Buttons[2, 1].FieldState ||
                        Buttons[2, 1].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 1);
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(2, 1);
                    }
                }
            }
            #endregion

            #region case 0,1

            //case 0,2                          
            if (row == 0 && column == 2)
            {   //  == 0,2 -> 0,1 -> 0,0  
                if (Buttons[0, 2].FieldState == Buttons[0, 1].FieldState ||
                    Buttons[0, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[0, 2].FieldState == Buttons[0, 0].FieldState ||
                        Buttons[0, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 1);
                        IsEmptySetHint(0, 0);
                    }
                }
                //  == 0,2 -> 1,2 -> 2,2
                if (Buttons[0, 2].FieldState == Buttons[1, 2].FieldState ||
                    Buttons[1, 2].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[0, 2].FieldState == Buttons[2, 2].FieldState ||
                        Buttons[2, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 2);
                        IsEmptySetHint(2, 2);
                    }
                }
                //  == 0,2 -> 1,1 -> 2.0
                if (Buttons[0, 2].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[0, 2].FieldState == Buttons[2, 0].FieldState ||
                        Buttons[2, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 2);
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(2, 0);
                    }
                }
            }

            #endregion

            #region case 1,0

            // case 1,0
            if (row == 1 && column == 0)
            {   //  == 1,0 -> 0,0 -> 2,0
                if (Buttons[1, 0].FieldState == Buttons[0, 0].FieldState ||
                    Buttons[0, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 0].FieldState == Buttons[2, 0].FieldState ||
                        Buttons[2, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 0);
                        IsEmptySetHint(2, 0);
                    }
                }
                //  == 1,0 -> 1,1 -> 1,2
                if (Buttons[1, 0].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 0].FieldState == Buttons[1, 2].FieldState ||
                        Buttons[1, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 0);
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(1, 2);
                    }
                }
            }
            
            #endregion

            #region case 1,1

            // case 1,1
            if (row == 1 && column == 1)
            {
                //  == 1,1 -> 1,0 -> 1,2
                if (Buttons[1, 1].FieldState == Buttons[1, 0].FieldState ||
                    Buttons[1, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 1].FieldState == Buttons[1, 2].FieldState ||
                        Buttons[1, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 0);
                        IsEmptySetHint(1, 2);
                    }
                }
                //  == 1,1 -> 0,0 -> 2,2
                if (Buttons[1, 1].FieldState == Buttons[0, 0].FieldState ||
                    Buttons[0, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 1].FieldState == Buttons[2, 2].FieldState ||
                        Buttons[2, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 0);
                        IsEmptySetHint(2, 2);
                    }
                }
                //  == 1,1 -> 1,0 -> 0,2
                if (Buttons[1, 1].FieldState == Buttons[1, 0].FieldState ||
                    Buttons[1, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 1].FieldState == Buttons[0, 2].FieldState)
                        //buttons[0, 2].FieldState != notTheActualPlayer)
                    {
                        IsEmptySetHint(1, 0);
                        IsEmptySetHint(0, 2);
                    }
                }
                //  == 1,1 -> 2,1 -> 0,1
                if (Buttons[1, 1].FieldState == Buttons[2, 1].FieldState ||
                    Buttons[2, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 1].FieldState == Buttons[2, 1].FieldState ||
                        Buttons[2, 1].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(2, 1);
                        IsEmptySetHint(0, 1);
                    }
                }
                //  == 1,1 -> 2,0 -> 0,0
                if (Buttons[1, 1].FieldState == Buttons[2, 0].FieldState ||
                    Buttons[2, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 1].FieldState == Buttons[0, 2].FieldState ||
                        Buttons[0, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 0);
                        IsEmptySetHint(2, 0);
                        IsEmptySetHint(0, 2);
                    }
                }
            }

            #endregion

            #region  case 1,2

            // case 1,2
            if (row == 1 && column == 2)
            {   //  == 1,2 -> 0,2 -> 2,2
                if (Buttons[1, 2].FieldState == Buttons[0, 2].FieldState ||
                    Buttons[0, 2].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 2].FieldState == Buttons[2, 2].FieldState ||
                        Buttons[2, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(0, 2);
                        IsEmptySetHint(2, 2);
                    }
                }
                //  == 1,2 -> 1,1 -> 1,0
                if (Buttons[1, 2].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[1, 2].FieldState == Buttons[1, 0].FieldState ||
                        Buttons[1, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 2);
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(1, 0);
                    }
                }
            }

            #endregion

            #region case 2,0

            // case 2,0
            if (row == 2 && column == 0)
            {   //  == 2,0 -> 1,0 -> 0,0
                if (Buttons[2, 0].FieldState == Buttons[1, 0].FieldState ||
                    Buttons[1, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[2, 0].FieldState == Buttons[0, 0].FieldState ||
                        Buttons[0, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 0);
                        IsEmptySetHint(0, 0);
                    }
                }
                //  == 2,0 -> 1,1 -> 0,2
                if (Buttons[2, 0].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[2, 0].FieldState == Buttons[0, 2].FieldState ||
                        Buttons[0, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(0, 2);
                    }
                }
                //  == 2,0 -> 2,1 -> 2,2
                if (Buttons[2, 0].FieldState == Buttons[2, 1].FieldState ||
                    Buttons[2, 1].FieldState != notTheActualPlayerColor)
                {   
                    if (Buttons[2, 0].FieldState == Buttons[2, 2].FieldState ||
                        Buttons[2, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(2, 0);
                        IsEmptySetHint(2, 1);
                        IsEmptySetHint(2, 2);
                    }
                }
            }

            #endregion

            #region case 2,1

            // case 2,1
            if (row == 2 && column == 1)
            {
                //  == 2,1 -> 2,0 -> 2,2
                if (Buttons[2, 1].FieldState == Buttons[2, 0].FieldState ||
                    Buttons[2, 0].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[2, 1].FieldState == Buttons[2, 2].FieldState ||
                        Buttons[2, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(2, 0);
                        IsEmptySetHint(2, 2);
                    }
                }
                //  == 2,1 -> 1,1 -> 0,1
                if (Buttons[2, 1].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[2, 1].FieldState == Buttons[0, 1].FieldState ||
                        Buttons[0, 1].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(2, 1);
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(0, 1);
                    }
                }
            }

            #endregion

            #region  case 2,2

            // case 2,2
            if (row == 2 && column == 2)
            {   //  == 2,2 -> 1,2 -> 0,2
                if (Buttons[2, 2].FieldState == Buttons[1, 2].FieldState ||
                    Buttons[1, 2].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[2, 2].FieldState == Buttons[0, 2].FieldState ||
                        Buttons[0, 2].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 2);
                        IsEmptySetHint(0, 2);
                    }
                }
                //  == 2,2 -> 1,1 -> 0,0
                if (Buttons[2, 2].FieldState == Buttons[1, 1].FieldState ||
                    Buttons[1, 1].FieldState != notTheActualPlayerColor)
                {
                    if (Buttons[2, 2].FieldState == Buttons[0, 0].FieldState ||
                        Buttons[0, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(1, 1);
                        IsEmptySetHint(0, 0);
                    }
                }
                //  == 2,2 -> 2,1 -> 2.0
                if (Buttons[2, 2].FieldState == Buttons[2, 1].FieldState ||
                    Buttons[2, 1].FieldState != notTheActualPlayerColor)
                {   
                    if (Buttons[2, 2].FieldState == Buttons[2, 0].FieldState ||
                        Buttons[2, 0].FieldState != notTheActualPlayerColor)
                    {
                        IsEmptySetHint(2, 2);
                        IsEmptySetHint(2, 1);
                        IsEmptySetHint(2, 0);
                    }
                }
            }

            #endregion

            Buttons[row, column].FieldState = activePlayerColor;
        }


        /// <summary>
        /// Setzt ausgewähltes feld auf Hint, wenn Empty. sonst bleibt aktueller Status bestehen
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        private static void IsEmptySetHint(int column, int row)
        {
            
            Buttons[column, row].FieldState = Buttons[column, row].FieldState == FieldState.Empty ? FieldState.Hint : Buttons[column, row].FieldState;
            
        }

        public byte GetHint(bool pPlayerID)
        {
            return new byte();
        }
    }
}
