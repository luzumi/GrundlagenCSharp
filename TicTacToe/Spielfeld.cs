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
        public Button[,] buttons = new Button[3,3];

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
                    buttons[column, row] = new Button(new Point((byte)(column * 2), (byte)(row*2)));
                    buttons[column, row].FieldState = FieldState.Hint;
                }
            }
        }


        public FieldState[,] GetBoard()
        {
            return board;
        }



        public bool GetPlayerID()
        {
            return ! currentPlayerID;
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

        public void ResetBoard()
        {
            Console.SetCursorPosition(9 , (9));
            Console.Write("-------");
            for (int row = 0; row < 3; row++)
            {
                Console.SetCursorPosition(9, (10 + 2*  row));
                Console.Write("| | | |");
                Console.SetCursorPosition(9 , (11 + 2*  row));
                Console.Write("-------");
                for (int column= 0; column < 3; column++)
                {
                    switch (board[column,row])
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
                            Console.SetCursorPosition(10 + 2 *column, 10 + 2 * row);
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
                            Console.SetCursorPosition(10 + 2 * column, 10 + 2 *row);
                            Console.Write("*");
                            Console.ResetColor();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            Console.SetCursorPosition(12,12);
        }
    }
}
