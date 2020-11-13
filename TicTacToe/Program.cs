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
                //TODO: change to read
                s.PlayerNames[i - 1] = Console.ReadLine();
                Console.SetCursorPosition(2, 4 + i);
                Console.Write("Spieler{0}: {1,2}                                        ", i, s.PlayerNames[i - 1]);
            }

            Console.CursorVisible = false;

            ResetBoard2();

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

            ResetBoard2();
        }


        /// <summary>
        /// Spielzug wird auf Ergebnis geprüft.
        /// Win/Tie/Valid und Invalid sind mögliche Resultate
        /// Tastatuteingabe wird behandelt
        /// </summary>
        /// <param name="s"></param>
        private static void ValidateInput(Spielfeld s)
        {
            ResetHints();

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
                    ResetBoard2();
                    break;

                case ConsoleKey.Escape:
                    break;
            }

            Console.ResetColor();
        }


        /// <summary>
        /// gesetzte Hints werden zurückgesetzt
        /// </summary>
        private static void ResetHints()
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
                        Console.SetCursorPosition((horizonzal + 1 + column * 4), (10 + row * 2));
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
                ScreenClear(delete);

                //Console.SetCursorPosition(horizonzal, 10);

                Thread.Sleep(200);

                OutputWinner(s);

                ResetBoard2();

                Thread.Sleep(500);
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
            }
        }


        /// <summary>
        /// Bestandteil des Siegerbildschirms
        /// </summary>
        /// <param name="s"></param>
        private static void OutputWinner(Spielfeld s)
        {
            var name = s.GetPlayerID() ? s.PlayerNames[0] : s.PlayerNames[1];
            Random r = new Random();
            var gewonnen = String.Format(" WINNER  {0}                ", name);
            for (int row = 0; row < 25; row++)
            {
                name = s.GetPlayerID() ? s.PlayerNames[0] : s.PlayerNames[1];
                Console.SetCursorPosition(2 + r.Next(0, 12), r.Next(0, 25));
                SwitchBackgroundColor(row);
                Console.WriteLine(name + gewonnen.ToString().Substring(0, row));
                ResetBoard2();
            }
        }

        private static void SwitchBackgroundColor(in int pRow)
        {
            switch (pRow % 5)
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case 1:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case 4:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
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
                SwitchBackgroundColor(row * 8);
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
        /// Spielfeld wird erstellt, bzw im laufenden Spiel neu geeschrieben
        /// </summary>
        private static void ResetBoard2()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(horizonzal - 1, (9));
            Console.Write("+---+---+---+");

            for (int row = 0; row < 3; row++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(horizonzal - 1, (10 + 2 * row));
                Console.Write("|   |   |   |");
                Console.SetCursorPosition(horizonzal - 1, (11 + 2 * row));
                Console.Write("+---+---+---+");
                for (int column = 0; column < 3; column++)
                {
                    OutputSign(column, row);
                }
            }

            Console.ResetColor();
        }


        /// <summary>
        /// Einzelnes Feld wird mit anhand des FieldStates beschrieben
        /// </summary>
        /// <param name="column">left:</param>
        /// <param name="row">top:</param>
        private static void OutputSign(int column, int row)
        {
            bool isSelected = (Program.Lesekopf.X == column && Program.Lesekopf.Y == row);

            Console.SetCursorPosition((horizonzal + 1) + 4 * column, 10 + 2 * row);

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
