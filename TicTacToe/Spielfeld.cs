using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe
{
    class Spielfeld
    {
        private FieldState[,] board = new FieldState[3, 3];
        private bool currentPlayerID;
        public string[] PlayerNames = new string[2];
        List<Button> buttons = new List<Button>(9);

        public FieldState[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        public Spielfeld()
        {
            for (byte column = 0; column < 3; column++)
            {
                for (byte row = 0; row < 3; row++)
                {
                    buttons.Add(new Button(new Point((byte)(column * 2), (byte)(row*2))));
                    board[column, row] = FieldState.Hint;
                }
            }

            
        }


        public FieldState[,] GetBoard()
        {
            return board;
        }



        public bool GetPlayerID()
        {
            return false;
        }

        public TurnResult Turn(Point pCoordinates)
        {
            return TurnResult.Invalid;
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
