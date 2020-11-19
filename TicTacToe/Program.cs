using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    static class Program
    {
        private static Point Lesekopf = new Point {X = 0, Y = 0};
        private static TurnResult gameResult;
        private static int horizonzal = 18;
        private static int vertikal = 19;
        private static List<TextBox> screenTextBoxes = new List<TextBox>();
        public static int programmZustand = 0;
        public static Random rand = new Random();


        static void Main()
        {
            Spielfeld s = new Spielfeld();
            Timer fpsCounter = new Timer();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;


            ConsoleKey key = ConsoleKey.Attention;


            TextBox tbStart = new TextBox(new Point(10, 10), ConsoleColor.DarkRed);
            tbStart.Spieler1Abfragen();

            TextBox rahmen = new TextBox(new Point(0,0), SwitchForegroundColor(rand.Next(0,15)));
            

            do
            {
                fpsCounter.FpsChecker();
                tbStart.Draw();
                rahmen.DrawRahmen();

                if (programmZustand == 2)
                {
                    ResetBoard2();
                }

                if (programmZustand == 3)
                {
                    ResetBoard2();
                    OutputTie();
                }

                if (!Console.KeyAvailable) continue;
                // code only processed when a key is down

                key = Console.ReadKey(true).Key;

                if (programmZustand == 2)
                {
                    ValidateInput(s, key);
                    if (gameResult == TurnResult.Valid || gameResult == TurnResult.Invalid)
                    {
                        ResetBoard2();
                    }
                    else if (gameResult == TurnResult.Win)
                    {
                        Win(s);
                    }
                    else if (gameResult == TurnResult.Tie)
                    {
                        programmZustand = 3;
                        OutputTie();
                    }
                }

                else
                {
                    switch (key)
                    {
                        default:
                            tbStart.ProcessKey(key, s);
                            break;
                    }
                }
            } while (key != ConsoleKey.Escape);

            Console.ResetColor();

            Console.ReadKey(true);
        }


        /// <summary>
        /// Spielzug wird auf Ergebnis geprüft.
        /// Win/Tie/Valid und Invalid sind mögliche Resultate
        /// Tastatuteingabe wird behandelt
        /// </summary>
        /// <param name="s"></param>
        public static void ValidateInput(Spielfeld s, ConsoleKey key)
        {
            ResetHints();

            switch (key)
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
                        Console.SetCursorPosition((horizonzal + 1 + column * 4),
                            ((vertikal + 1) + row * 2));
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
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(horizonzal - 5, i);

                Console.Write(pDelete);
            }
        }


        /// <summary>
        /// Bestandteil des Siegerbildschirms
        /// </summary>
        /// <param name="s"></param>
        private static void OutputWinner(Spielfeld s)
        {
            var name = s.GetPlayerID() ? Spielfeld.PlayerNames[0] : Spielfeld.PlayerNames[1];
            Random r = new Random();
            var gewonnen = String.Format("   WINNER  {0}                  ", name);
            for (int row = 0; row < 16; row++)
            {
                name = s.GetPlayerID() ? Spielfeld.PlayerNames[0] : Spielfeld.PlayerNames[1];
                Console.SetCursorPosition(horizonzal - 2 + r.Next(0, 12), r.Next(6, 18) + 1);
                SwitchBackgroundColor(row);
                Console.WriteLine(name + gewonnen.ToString().Substring(0, row));
                ResetBoard2();
            }
        }


        public static void OutputTie()
        {
            Random r = new Random();
            string line1 = " U-N-E-N-T-S-C-H-I-E-D-E-N ";
            string ausgabe = "";
            for (int zeichen = 0; zeichen < line1.Length; zeichen = r.Next(0, line1.Length))
            {
                zeichen++;

                SwitchForegroundColor(r.Next(0, 100));
                Console.CursorVisible = false;
                Console.SetCursorPosition(horizonzal + zeichen, vertikal);
                if (zeichen % 2 == 0)
                {
                    line1[zeichen].ToString().Replace('-', '*');

                    if (zeichen % 3 == 0)
                    {
                        line1[zeichen].ToString().Replace('-', '~');
                    }
                }
                if (zeichen == line1.Length)
                {
                    break;

                } 
                Console.Write(line1[zeichen]);
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


        public static ConsoleColor SwitchForegroundColor(in int pRow)
        {
            switch (pRow % 5)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.ForegroundColor = (pRow % 12) switch
            {
                0 => ConsoleColor.Gray,
                1 => ConsoleColor.DarkCyan,
                2 => ConsoleColor.DarkMagenta,
                3 => ConsoleColor.DarkGreen,
                4 => ConsoleColor.White,
                5 => ConsoleColor.Yellow,
                6 => ConsoleColor.Magenta,
                7 => ConsoleColor.DarkYellow,
                8 => ConsoleColor.DarkBlue,
                9 => ConsoleColor.Green,
                10 => ConsoleColor.Red,
                11 => ConsoleColor.Cyan,
            };


            return Console.ForegroundColor;
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
        public static void ResetBoard2()
        {
            for (int row = 0; row < 3; row++)
            {
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

            Console.SetCursorPosition((horizonzal + 5) + 4 * column, (vertikal + 2) + 2 * row);

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
