using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TicTacToe
{
    class Spielfeld
    {
        private FieldState[,] board = new FieldState[3, 3];
        private bool currentPlayerID;
        public string[] PlayerNames = new string[2];
        public Button[,] buttons = new Button[3, 3];
        private int round;
        public bool Running { get; set; }

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

            Running = true;
        }


        public FieldState[,] GetBoard()
        {
            return board;
        }


        public bool GetPlayerID()
        {
            return currentPlayerID;
        }

        public TurnResult Turn(Point pCoordinates)
        {
            if (buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.X ||
                buttons[pCoordinates.X, pCoordinates.Y].FieldState == FieldState.O)
            {
                return TurnResult.Invalid;
            }


            if (GetPlayerID())
            {
                buttons[pCoordinates.X, pCoordinates.Y].FieldState = FieldState.X;
            }
            else
            {
                buttons[pCoordinates.X, pCoordinates.Y].FieldState = FieldState.O;
            }

            if (round > 1 &&
                //Prüfe vertikal
                ((buttons[1, 0].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 0].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[pCoordinates.X, 2].FieldState) ||

                 
                 //Prüfe Vertikal
                 (buttons[1, 0].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[0, pCoordinates.Y].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[1, pCoordinates.Y].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[2, pCoordinates.Y].FieldState) ||

                
                 //Prüfe Diagonal
                 (buttons[1, 1].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[0, 0].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[1, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[2, 2].FieldState ) ||

                 (buttons[1, 1].FieldState != FieldState.Empty &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[0, 2].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[1, 1].FieldState &&
                  buttons[pCoordinates.X, pCoordinates.Y].FieldState == buttons[2, 0].FieldState ) ) ) 
            {
                Running = false;
                return TurnResult.Win;
            }

            if (round == 9)
            {
                Running = false;
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

        public void ResetBoard()
        {
            Console.SetCursorPosition(9, (9));
            Console.Write("-------");
            
            for (int row = 0; row < 3; row++)
            {
                Console.SetCursorPosition(9, (10 + 2 * row));
                Console.Write("| | | |");
                Console.SetCursorPosition(9, (11 + 2 * row));
                Console.Write("-------");
                for (int column = 0; column < 3; column++)
                {
                    OutputSign(column, row);
                }
            }
        }

        public void OutputSign(int column, int row)
        {
            switch (buttons[column, row].FieldState)
            {
                case FieldState.Empty:
                    Console.SetCursorPosition(10 + 2 * column, 10 + 2 * row);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(" ");
                    Console.ResetColor();
                    break;
                case FieldState.X:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(10 + 2 * column, 10 + 2 * row);
                    Console.Write("X");
                    Console.ResetColor();
                    break;
                case FieldState.O:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(10 + 2 * column, 10 + 2 * row);
                    Console.Write("O");
                    Console.ResetColor();
                    break;
                case FieldState.Hint:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(10 + 2 * column, 10 + 2 * row);
                    Console.Write("*");
                    Console.ResetColor();
                    break;
               
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
