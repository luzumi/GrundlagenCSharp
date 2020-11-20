using System;
using System.Threading;

namespace TicTacToe
{
    static class Program
    {
        private static Point Cursor = new Point {X = 0, Y = 0};
        private static TurnResult gameResult;
        private static int horizonzal = 18;
        private static int vertikal = 19;
        public static int programState;
        public static Random rand = new Random();


        static void Main()
        {
            GameLogic s = new GameLogic();
            Timer fpsCounter = new Timer();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetWindowSize(60,30);
            Console.Title = "T~I~C  T~A~C  T~O~E";
            

            ConsoleKey key = ConsoleKey.Attention;


            TextBox tbStart = new TextBox(new Point(10, 10), ConsoleColor.Red);
            tbStart.GetNamePlayerOne();

            TextBox boarder = new TextBox(new Point(0,0), SwitchForegroundColor(rand.Next(0,15)));

            do
            {
                fpsCounter.FpsChecker();
                tbStart.Draw();
                boarder.DrawBoarder();

                if (programState == 2) { ResetBoard2(); }

                if (programState == 3)
                {
                    ResetBoard2();
                    TextBox.OutputTie();
                }

                if (!Console.KeyAvailable) continue;
                // code only processed when a key is down

                key = Console.ReadKey(true).Key;

                if (programState == 2)
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
                        programState = 3;
                        TextBox.OutputTie();
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
        /// <param name="pKey"></param>
        public static void ValidateInput(GameLogic s, ConsoleKey pKey)
        {
            ResetHints();

            switch (pKey)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    gameResult = s.Turn(new Point(Cursor.X, Cursor.Y));
                    break;

                case ConsoleKey.LeftArrow:
                    if (Cursor.X > 0)
                    {
                        Cursor.X--;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Cursor.X, Cursor.Y);
                    }

                    break;
                case ConsoleKey.UpArrow:
                    if (Cursor.Y > 0)
                    {
                        Cursor.Y--;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Cursor.X, Cursor.Y);
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (Cursor.X < 2)
                    {
                        Cursor.X++;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Cursor.X, Cursor.Y);
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (Cursor.Y < 2)
                    {
                        Cursor.Y++;
                        MoveCursor();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        OutputSign(Cursor.X, Cursor.Y);
                    }

                    break;
                case ConsoleKey.F1:
                    s.DrawHint(Cursor.X, Cursor.Y);
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
                    GameLogic.Buttons[column, row].FieldState =
                        GameLogic.Buttons[column, row].FieldState == FieldState.Hint
                            ? FieldState.Empty
                            : GameLogic.Buttons[column, row].FieldState;
                }
            }
        }


        /// <summary>
        /// Bewegt den Cursor mit den Pfeiltasten durch GameLogic und selectiertes Feld blau
        /// </summary>
        private static void MoveCursor()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (row == Cursor.X && column == Cursor.Y)
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
        private static void Win(GameLogic s)
        {
            var delete = "                                      ";

            OutputWinner(s);


            for (int row = 0; row < 8; row++)
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
        private static void OutputWinner(GameLogic s)
        {
            var name = s.GetPlayerID() ? GameLogic.PlayerNames[0] : GameLogic.PlayerNames[1];
            Random r = new Random();
            var winnerText = String.Format("   WINNER  {0}                  ", name);
            for (int row = 0; row < 16; row++)
            {
                name = s.GetPlayerID() ? GameLogic.PlayerNames[0] : GameLogic.PlayerNames[1];
                Console.SetCursorPosition(horizonzal - 2 + r.Next(0, 12), r.Next(6, 18) + 1);
                SwitchBackgroundColor(row);
                Console.WriteLine(name + winnerText.Substring(0, row));
                ResetBoard2();
            }
        }


        

        private static void SwitchBackgroundColor(in int pRow)
        {
            Console.ForegroundColor = (pRow % 5) switch
            {
                0 => ConsoleColor.Gray,
                1 => ConsoleColor.DarkCyan,
                2 => ConsoleColor.DarkMagenta,
                3 => ConsoleColor.DarkGreen,
                _ => ConsoleColor.Black,
            };
        }


        public static ConsoleColor SwitchForegroundColor(in int pRow)
        {
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
                _ => ConsoleColor.Black,
            };


            return Console.ForegroundColor;
        }


        /// <summary>
        /// GameLogic wird erstellt, bzw im laufenden Spiel neu geeschrieben
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
            bool isSelected = (Program.Cursor.X == column && Program.Cursor.Y == row);

            Console.SetCursorPosition((horizonzal + 5) + 4 * column, (vertikal + 2) + 2 * row);

            switch (GameLogic.Buttons[column, row].FieldState)
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
