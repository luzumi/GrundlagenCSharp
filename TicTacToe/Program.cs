using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace TicTacToe
{
    static class Program
    {
        private static Point Lesekopf = new Point {X = 0, Y = 0};
        private static TurnResult gameResult;
        private static int horizonzal = 18;


        static void Main()
        {
            //Fenstereinstellungen
            Console.SetWindowSize(40, 25);
            Console.SetCursorPosition(15, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TIC TAC TOE");


            Spielfeld s = new Spielfeld();

            //Abfrage Spielernamen
            for (int i = 1; i <= s.PlayerNames.Length; i++)
            {
                Console.SetCursorPosition(2, 4 + i);
                Console.Write("Spieler {0}, Ihr Namen bitte: ", i);
                s.PlayerNames[i - 1] = Console.ReadLine();
                Console.SetCursorPosition(2, 4 + i);
                Console.Write("Spieler{0}: {1,2}                                        ", i, s.PlayerNames[i - 1]);
            }

            Console.CursorVisible = false;

            ResetBoard();

            //Spiel läuft ab bis Sieg oder Win
            do
            {
                Draw(s);
            } while (gameResult == TurnResult.Valid || gameResult == TurnResult.Invalid);


            if (gameResult == TurnResult.Win)
            {
                Win(s);
            }
            else
            {
                Console.WriteLine("Unentschieden");
            }

            Console.ReadKey(true);
        }


        /// <summary>
        /// Spielablauf
        /// </summary>
        /// <param name="s"></param>
        static void Draw(Spielfeld s)
        {
            s.GetBoard();

            ValidateInput(s);

            ResetBoard();
        }


        /// <summary>
        /// Spielzug wird auf Ergebnis geprüft.
        /// Win/Tie/Valid und Invalid sind mögliche Resultate
        /// Tastatuteingabe wird behandelt
        /// </summary>
        /// <param name="s"></param>
        private static void ValidateInput(Spielfeld s)
        {
            //Hints zurücksetzen
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    Spielfeld.buttons[column, row].FieldState =
                        Spielfeld.buttons[column, row].FieldState == FieldState.Hint
                            ? FieldState.Empty
                            : Spielfeld.buttons[column, row].FieldState;
                }
            }

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
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Lesekopf.X, Lesekopf.Y);
                    }

                    break;
                case ConsoleKey.UpArrow:
                    if (Lesekopf.Y > 0)
                    {
                        Lesekopf.Y--;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Lesekopf.X, Lesekopf.Y);
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (Lesekopf.X < 2)
                    {
                        Lesekopf.X++;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Lesekopf.X, Lesekopf.Y);
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (Lesekopf.Y < 2)
                    {
                        Lesekopf.Y++;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Lesekopf.X, Lesekopf.Y);
                    }

                    break;
                case ConsoleKey.F1:
                    s.DrawHint(Lesekopf.X, Lesekopf.Y);
                    ResetBoard();
                    break;


                case ConsoleKey.Escape:
                    break;
            }

            Console.ResetColor();
        }


        /// <summary>
        /// Bewegt den Cursor mit den Pfeiltasten durch Spielfeld und selectiertes Feld blau
        /// </summary>
        private static void MoveCursor()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (row == Lesekopf.X && column == Lesekopf.Y)
                    {
                        Console.SetCursorPosition((horizonzal + column * 2), (10 + row * 2));
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(column, row);
                    }
                }
            }
        }

        /// <summary>
        /// Siegerbildschirm
        /// </summary>
        /// <param name="s"></param>
        private static void Win(Spielfeld s)
        {
            var delete = "                                      ";

            OutputWinner(s);


            for (int J = 0; J < 8; J++)
            {
                Console.SetCursorPosition(horizonzal, 10);

                Thread.Sleep(200);

                OutputWinner(s);

                ResetBoard();

                Thread.Sleep(500);

                ScreenClear(delete);
            }
        }


        /// <summary>
        /// Füllt die Console mit Leerzeichen
        /// </summary>
        /// <param name="pDelete"></param>
        private static void ScreenClear(string pDelete)
        {
            for (int i = 0; i < 22; i++)
            {
                Console.SetCursorPosition(0, i);

                Console.Write(pDelete);
                Thread.Sleep(8);
            }
        }


        /// <summary>
        /// Bestandteil des Siegerbildschirms
        /// </summary>
        /// <param name="s"></param>
        private static void OutputWinner(Spielfeld s)
        {
            Random r = new Random();
            string gewonnen = " WINNER                  ";
            for (int row = 0; row < 25; row++)
            {
                Console.SetCursorPosition(2 + r.Next(0, 12), r.Next(0, 25));
                string name = s.GetPlayerID() ? s.PlayerNames[0] : s.PlayerNames[1];
                Console.WriteLine(name + gewonnen.Substring(0, row));
                ResetBoard();
            }
        }


        /// <summary>
        /// Spielfeld wird erstellt, bzw im laufenden Spiel neu geeschrieben
        /// </summary>
        private static void ResetBoard()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(horizonzal - 1, (9));
            Console.Write("-------");

            for (int row = 0; row < 3; row++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(horizonzal - 1, (10 + 2 * row));
                Console.Write("| | | |");
                Console.SetCursorPosition(horizonzal - 1, (11 + 2 * row));
                Console.Write("-------");
                for (int column = 0; column < 3; column++)
                {
                    OutputSign(column, row);
                }
            }
        }


        /// <summary>
        /// Einzelnes Feld wird mit anhand des FieldStates beschrieben
        /// </summary>
        /// <param name="column">left:</param>
        /// <param name="row">top:</param>
        private static void OutputSign(int column, int row)
        {
            bool isSelected = (Program.Lesekopf.X == column && Program.Lesekopf.Y == row);

            Console.SetCursorPosition(horizonzal + 2 * column, 10 + 2 * row);

            switch (Spielfeld.buttons[column, row].FieldState)
            {
                case FieldState.Empty:
                    Console.BackgroundColor = (isSelected ? ConsoleColor.Blue : ConsoleColor.Black);
                    Console.WriteLine(" ");
                    break;
                case FieldState.X:
                    Console.BackgroundColor = (isSelected ? ConsoleColor.Blue : ConsoleColor.Gray);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("X");
                    break;
                case FieldState.O:
                    Console.BackgroundColor = (isSelected ? ConsoleColor.Blue : ConsoleColor.DarkGray);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("O");
                    break;
                case FieldState.Hint:
                    Console.BackgroundColor = (isSelected ? ConsoleColor.Blue : ConsoleColor.Black);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("*");
                    Console.ResetColor();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.ResetColor();
        }
    }
}
