using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Channels;

namespace TicTacToe
{
    class Program
    {
        static Point Lesekopf = new Point {X = 0, Y = 0};
        static private TurnResult gameResult;


        static void Main(string[] args)
        {
            Console.SetWindowSize(60, 25);
            Console.SetCursorPosition(9, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TIC TAC TOE");


            Spielfeld s = new Spielfeld();

            for (int i = 1; i <= s.PlayerNames.Length; i++)
            {
                Console.SetCursorPosition(9, 4 + i);
                Console.Write("Spieler{0}, geben Sie Ihren Namen ein:", i);
                s.PlayerNames[i - 1] = Console.ReadLine();
            }

            s.ResetBoard();

            Console.SetCursorPosition(10, 10);
            Thread.Sleep(1000);

            do
            {
                Draw(s.Board, s);
            } while (gameResult == TurnResult.Valid || gameResult == TurnResult.Invalid);

            if(gameResult == TurnResult.Win)
            {
                string name = s.GetPlayerID() ? s.PlayerNames[0] : s.PlayerNames[1];
                Console.WriteLine(name + " hat das Spiel gewonnen");
            }
            else
            {
                Console.WriteLine("Unentschieden");
            }

            Console.ReadKey(true);

            Console.Clear();
        }
        
    

        static void Draw(FieldState[,] pFieldStates, Spielfeld s)
        {
            s.GetBoard();

            ValidateInput(s);

            s.ResetBoard();
        }

        private static void ValidateInput(Spielfeld s)
        
        
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    gameResult = s.Turn(new Point(Lesekopf.X, Lesekopf.Y));
                    break;

                case ConsoleKey.LeftArrow:
                    if (Lesekopf.X > 0)
                    {
                        Lesekopf.X--;
                        MoveCursor(s);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        s.OutputSign((int)Lesekopf.X, (int)Lesekopf.Y);
                        Console.ResetColor();
                    }

                    break;
                case ConsoleKey.UpArrow:
                    if (Lesekopf.Y > 0)
                    {
                        Lesekopf.Y--;
                        MoveCursor(s);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        s.OutputSign((int)Lesekopf.X, (int)Lesekopf.Y);
                        Console.ResetColor();
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (Lesekopf.X < 2)
                    {
                        Lesekopf.X++;
                        MoveCursor(s);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        s.OutputSign((int)Lesekopf.X, (int)Lesekopf.Y);
                        Console.ResetColor();
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (Lesekopf.Y < 2)
                    {
                        Lesekopf.Y++;
                        MoveCursor(s);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        s.OutputSign((int)Lesekopf.X, (int)Lesekopf.Y);
                        Console.ResetColor();
                    }

                    break;
                case ConsoleKey.Help:
                    break;


                case ConsoleKey.Escape:
                    break;

                default:
                    //throw new ArgumentOutOfRangeException();
                    
                    break;
            }
        }


        private static void MoveCursor(Spielfeld s)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (row == Lesekopf.X && column == Lesekopf.Y)
                    {
                        Console.SetCursorPosition((10 + row * 2), (10 + column * 2));
                        Console.BackgroundColor = ConsoleColor.Blue;
                        s.OutputSign(column, row);
                    }
                }
            }
        }
    }
}
