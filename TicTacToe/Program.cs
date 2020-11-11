using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Channels;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Spielfeld s = new Spielfeld();
            Draw(s.Board);

            Console.SetCursorPosition(0, 18);
        }

        public static void Draw(FieldState[,] pFieldStates)
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
                    switch (pFieldStates[column,row])
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
        }
    }
}
