using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TicTacToe
{
    class TextBox
    {
        private readonly StringBuilder _CONTENT;
        private readonly Point POSITION;
        private readonly ConsoleColor COLOR;
        private static readonly int HORIZONTAL = 18;
        private static readonly int VERTICAL = 19;
        private static int _offset;

        public string Content
        {
            get { return _CONTENT.ToString(); }
        }

        public void Draw()
        {
            Console.SetCursorPosition(POSITION.X, POSITION.Y);
            Console.ForegroundColor = COLOR;

            string[] rows = _CONTENT.ToString().Split('\n');

            for (int i = 0; i < rows.Length; i++)
            {
                Console.Write(rows[i]);
                Console.SetCursorPosition(POSITION.X, POSITION.Y + i);
            }

            if (Program.programState == 2)
                DrawSpielfeld();
        }

        public void DrawBoarder()
        {
            for (int row = 0; row < 30; row = Program.rand.Next(0, 45))
            {
                SetSign(row, 0, "Zeichen.txt");
                SetSign(row, 50, "Zeichen.txt");
            }

            for (int row = 0; row < 10; row = Program.rand.Next(0, 15))
            {
                SetSign(row, 10, "Zeichen.txt");
                SetSign(row, 20, "Zeichen.txt");
                SetSign(row, 30, "Zeichen.txt");
                SetSign(row, 40, "Zeichen.txt");
            }

            for (int row = 0; row < 2; row = Program.rand.Next(0, 3))
            {
                SetSign(row + 28, 10, "Zeichen.txt");
                SetSign(row + 28, 20, "Zeichen.txt");
                SetSign(row + 28, 30, "Zeichen.txt");
                SetSign(row + 28, 40, "Zeichen.txt");
            }

            DrawLogo();
        }


        /// <summary>
        /// Erstellt einen Lauftext mit Hilfe eines vorgefertigten Banners in einer Textdatei
        /// </summary>
        public void DrawLogo()
        {
            List<string> logoLines = new List<string>();

            GetRowsFromTxtFile(logoLines);

            string[] output = new string[logoLines.Count];


            CreateMovingBanner(logoLines, output);

            PrintLines(logoLines, output);

            if (_offset < logoLines[0].Length - 1)
            {
                _offset++;
            }
            else
            {
                _offset = 0;
            }
        }

        private static void PrintLines(List<string> logoLines, string[] ausgabe)
        {
            for (int row = 0; row < logoLines.Count; row++)
            {
                Console.SetCursorPosition(0, row + 1);
                Console.Write(ausgabe[row]);
            }
        }

        private static void CreateMovingBanner(List<string> logoLines, string[] output)
        {
            for (int rowInLogo = 0; rowInLogo < logoLines.Count; rowInLogo++)
            {
                string newOutput;

                output[rowInLogo] =
                    logoLines[rowInLogo].Substring(_offset, logoLines[rowInLogo].Length - 1 - _offset);

                if (output[rowInLogo].Length == 0)
                {
                    output[rowInLogo] = logoLines[rowInLogo];
                }

                newOutput = logoLines[rowInLogo].Substring(0, (_offset));

                if (newOutput.Length > 7 && newOutput.Length < logoLines[rowInLogo].Length - 7)
                {
                    output[rowInLogo] += "      " + newOutput.Substring(0, newOutput.Length - 6);
                }
            }
        }

        private static void GetRowsFromTxtFile(List<string> logoLines)
        {
            string line;
            using (var reader = new StreamReader("Logo.txt"))
            {
                while ((line = reader.ReadLine()) is not null)
                {
                    logoLines.Add(line);
                }
            }
        }

        private static void SetSign(int row, int count, string path)
        {
            using (var reader = new StreamReader(path))
            {
                string sign;
                while ((sign = reader.ReadLine()) is not null)
                {
                    row = row switch
                    {
                        >= 1 and <=5 => 0,
                        _ => row
                    };

                    Console.SetCursorPosition(count, row);
                    sign = Program.rand.Next(0, 111) % 11 != 0 ? " " : ((Program.rand.Next(0, 2) % 2 == 0) && (sign != " ")) ? "X" : "O";
                    Program.SwitchForegroundColor(Program.rand.Next(0, 13));
                    Console.Write(sign);
                    count++;
                }
            }
        }

        public void DrawSpielfeld()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(HORIZONTAL + 3 + i * 4, VERTICAL + 2 + j * 2);
                    Console.Write("|");
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(HORIZONTAL + 3, (VERTICAL + 1 + i * 2));

                Console.Write("+---+---+---+");
            }

            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(HORIZONTAL + 31, VERTICAL + 1 + i);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("*");
            }

            if (Program.programState == 3)
            {
                OutputTie();
            }
        }

        public TextBox(Point Position, ConsoleColor TextColor)
        {
            POSITION = Position;
            COLOR = TextColor;
            _CONTENT = new StringBuilder();
        }


        private void GameBoarder()
        {
            const string star = "*";

            _CONTENT.AppendLine("");
            _CONTENT.AppendLine("****************************************");
            _CONTENT.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            _CONTENT.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            _CONTENT.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            _CONTENT.AppendLine(String.Format("*{0,39}", star));
            _CONTENT.AppendLine(String.Format("*{0,39}", star));
            _CONTENT.AppendLine(String.Format("*{0,39}", star));
            _CONTENT.AppendLine(String.Format("*   Spieler 1: {0,-18}  ", GameLogic.PlayerNames[0]));
            _CONTENT.AppendLine(String.Format("*   Spieler 1: {0,-18}  ", GameLogic.PlayerNames[1]));
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("*");
            _CONTENT.AppendLine("****************************************");
        }

        /// <summary>
        /// eingegebene Taste wird behandelt
        /// </summary>
        /// <param name="KeyInformation"></param>
        /// <param name="s"></param>
        public void ProcessKey(ConsoleKey KeyInformation, GameLogic s)
        {
            if (KeyInformation == ConsoleKey.Delete)
            {
                _CONTENT.Clear();
                Console.SetCursorPosition(POSITION.X, POSITION.Y);
                Console.Write("                                ");
            }

            if ((int)KeyInformation > 63 && (int)KeyInformation < 91 || KeyInformation == ConsoleKey.Enter || KeyInformation == ConsoleKey.Backspace)
            {
                switch (Program.programState)
                {
                    case 0:
                        if (KeyInformation == ConsoleKey.Backspace)
                        {
                            if (GameLogic.PlayerNames[0] != null && GameLogic.PlayerNames[0].Length > 0 )
                            {
                                GameLogic.PlayerNames[0] = GameLogic.PlayerNames[0]
                                    .Substring(0, GameLogic.PlayerNames[0].Length - 1);
                                _CONTENT.Clear();
                                GetNamePlayerOne();
                            }
                        }
                        else if (KeyInformation != ConsoleKey.Enter)
                        {
                            GameLogic.PlayerNames[0] += (char)KeyInformation;
                            _CONTENT.Clear();
                            GetNamePlayerOne();
                        }
                        else
                        {
                            Program.programState = 1;
                            _CONTENT.Clear();
                            GetNamePlayerTwo();
                        }
                        break;

                    case 1:
                        if (KeyInformation == ConsoleKey.Backspace)
                        {
                            if (GameLogic.PlayerNames[1] != null && GameLogic.PlayerNames[1].Length > 0 )
                            {
                                GameLogic.PlayerNames[1] = GameLogic.PlayerNames[1]
                                    .Substring(0, GameLogic.PlayerNames[1].Length - 1);
                                _CONTENT.Clear();
                                GetNamePlayerTwo();
                            }
                        }
                        else if (KeyInformation != ConsoleKey.Enter)
                        {
                            GameLogic.PlayerNames[1] += (char)KeyInformation;
                            _CONTENT.Clear();
                            GetNamePlayerTwo();
                        }
                        else
                        {
                            _CONTENT.Clear();
                            Program.programState = 2;
                            GameBoarder();
                            Program.ResetBoard2();
                        }
                        break;

                    case 2:
                        _CONTENT.Clear();
                        GameBoarder();
                        break;

                    case 3:
                        OutputTie();
                        break;

                    default:
                        _CONTENT.Append(KeyInformation);
                        break;
                }
            }
        }


        public void GetNamePlayerOne()
        {
            string row = "*";
            if (GameLogic.PlayerNames[0] != null)
            {
                row += String.Format("   Spieler 1: {0,-18}  ",
                    GameLogic.PlayerNames[0]);
            }
            else
            {
                row += "   Spieler 1:                         *";
            }

            _CONTENT.AppendLine("");
            _CONTENT.AppendLine("****************************************");
            _CONTENT.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            _CONTENT.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            _CONTENT.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*   Bitte geben Sie Ihren Namen ein:   *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine(row);
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("****************************************");
        }

        public void GetNamePlayerTwo()
        {
            string player1 = String.Format("*   Spieler 1: {0,-18}  ",
                GameLogic.PlayerNames[0]);

            string row = "*";

            if (GameLogic.PlayerNames[1] != null)
            {
                row += String.Format("   Spieler 2: {0,-18}  ",
                    GameLogic.PlayerNames[1]);
            }
            else
            {
                row += "   Spieler 2:                         *";
            }

            _CONTENT.AppendLine("");
            _CONTENT.AppendLine("****************************************");
            _CONTENT.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            _CONTENT.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            _CONTENT.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*   Bitte geben Sie Ihren Namen ein:   *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine(player1);
            _CONTENT.AppendLine(row);
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("*                                      *");
            _CONTENT.AppendLine("****************************************");
        }

        public static void OutputTie()
        {
            Random r = new Random();
            string line1 = " U-N-E-N-T-S-C-H-I-E-D-E-N ";
            string replace = line1;
            for (int character = 0; character < line1.Length; character = r.Next(0, line1.Length))
            {
                character++;

                Program.SwitchForegroundColor(r.Next(0, 100));
                Console.CursorVisible = false;
                Console.SetCursorPosition(HORIZONTAL + character, VERTICAL);
                if (character % 3 == 0)
                {
                    replace = line1.Replace('-', '*');

                    if (character % 3 == 1)
                    {
                        replace = line1.Replace('*', '~');
                    }
                    if (character % 3 == 2)
                    {
                        replace = line1.Replace('~', '_');
                    }
                }
                if (character == line1.Length)
                {
                    break;

                } 
                System.Diagnostics.Debug.WriteLine(replace);    //zum überprüfen in das Ausgabefenster schreiben
                Console.Write(replace[character]);
            }
        }
    }
}
