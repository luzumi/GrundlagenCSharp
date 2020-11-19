using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TicTacToe
{
    class TextBox
    {
        private StringBuilder content;
        private readonly Point position;
        private readonly ConsoleColor color;
        private static int horizonzal = 18;
        private static int vertikal = 19;
        private static int offset = 0;

        public string Content
        {
            get { return content.ToString(); }
        }

        public void Draw()
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.ForegroundColor = color;

            string[] zeilen = content.ToString().Split('\n');

            for (int i = 0; i < zeilen.Length; i++)
            {
                Console.Write(zeilen[i]);
                Console.SetCursorPosition(position.X, position.Y + i);
            }

            if (Program.programmZustand == 2)
                DrawSpielfeld();
        }

        public void DrawRahmen()
        {
            for (int row = 0; row < 30; row = Program.rand.Next(0, 45))
            {
                ZeichenSetzen(row, 0, "Zeichen.txt");
                ZeichenSetzen(row, 50, "Zeichen.txt");
            }

            for (int row = 0; row < 10; row = Program.rand.Next(0, 15))
            {
                ZeichenSetzen(row, 10, "Zeichen.txt");
                ZeichenSetzen(row, 20, "Zeichen.txt");
                ZeichenSetzen(row, 30, "Zeichen.txt");
                ZeichenSetzen(row, 40, "Zeichen.txt");
            }

            for (int row = 0; row < 2; row = Program.rand.Next(0, 3))
            {
                ZeichenSetzen(row + 28, 10, "Zeichen.txt");
                ZeichenSetzen(row + 28, 20, "Zeichen.txt");
                ZeichenSetzen(row + 28, 30, "Zeichen.txt");
                ZeichenSetzen(row + 28, 40, "Zeichen.txt");
            }

            DrawLogo();
        }

        public void DrawLogo()
        {
            List<string> logoLines = new List<string>();
            string line;

            using (var reader = new StreamReader("Logo.txt"))
            {
                while ((line = reader.ReadLine()) is not null)
                {
                    logoLines.Add(line);
                }
            }

            string[] ausgabe = new string[logoLines.Count];


            for (int zeileInLogo = 0; zeileInLogo < logoLines.Count; zeileInLogo++)
            {
                string neueAusgabe;

                ausgabe[zeileInLogo] =
                    logoLines[zeileInLogo].Substring(offset, logoLines[zeileInLogo].Length - 1 - offset);

                if (ausgabe[zeileInLogo].Length == 0)
                {
                    ausgabe[zeileInLogo] = logoLines[zeileInLogo];
                }

                neueAusgabe = logoLines[zeileInLogo].Substring(0, (offset));

                if (neueAusgabe.Length > 7 && neueAusgabe.Length < logoLines[zeileInLogo].Length-7)
                {
                    ausgabe[zeileInLogo] += "      " + neueAusgabe.Substring(0,neueAusgabe.Length-6);
                }
            }

            for (int zeile = 0; zeile < logoLines.Count; zeile++)
            {
                Console.SetCursorPosition(0, zeile + 1);
                Console.Write(ausgabe[zeile]);
            }

            if (offset < logoLines[0].Length-1)
            {
                offset++;
            }
            else
            {
                offset = 0;
            }
        }

        private static void ZeichenSetzen(int row, int count, string path)
        {
            using (var reader = new StreamReader(path))
            {
                string zeichen;
                while ((zeichen = reader.ReadLine()) is not null)
                {
                    row = row switch
                    {
                        >= 1 and <=5 => 0,
                        _ => row
                    };

                    Console.SetCursorPosition(count, row);
                    zeichen = Program.rand.Next(0, 2) % 2 == 0 ? " " : zeichen;
                    Program.SwitchForegroundColor(Program.rand.Next(0, 13));
                    Console.Write(zeichen);
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
                    Console.SetCursorPosition(horizonzal + 3 + i * 4, vertikal + 2 + j * 2);
                    Console.Write("|");
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(horizonzal + 3, (vertikal + 1 + i * 2));

                Console.Write("+---+---+---+");
            }

            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(horizonzal + 31, vertikal + 1 + i);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("*");
            }

            if (Program.programmZustand == 3)
            {
                Program.OutputTie();
            }
        }

        public TextBox(Point Position, ConsoleColor TextColor)
        {
            position = Position;
            color = TextColor;
            content = new StringBuilder();
        }


        /// <summary>
        /// eingegebene Taste wird behandelt
        /// </summary>
        /// <param name="KeyInformation"></param>
        /// <param name="s"></param>
        public void ProcessKey(ConsoleKey KeyInformation, Spielfeld s)
        {
            if (KeyInformation == ConsoleKey.Delete)
            {
                content.Clear();
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("                                ");
            }

            if ((int)KeyInformation > 63 && (int)KeyInformation < 91 || (int)KeyInformation == 13)
            {
                switch (Program.programmZustand)
                {
                    case 0:
                        if (KeyInformation != ConsoleKey.Enter)
                        {
                            Spielfeld.PlayerNames[0] += (char)KeyInformation;
                            content.Clear();
                            Spieler1Abfragen();
                        }
                        else
                        {
                            Program.programmZustand = 1;
                            content.Clear();
                            Spieler2Abfragen();
                        }

                        break;
                    case 1:
                        if (KeyInformation != ConsoleKey.Enter)
                        {
                            Spielfeld.PlayerNames[1] += (char)KeyInformation;
                            content.Clear();
                            Spieler2Abfragen();
                        }

                        else
                        {
                            content.Clear();
                            Program.programmZustand = 2;
                            Spiel();
                            Program.ResetBoard2();
                        }

                        break;
                    case 2:
                        content.Clear();
                        Spiel();
                        break;
                    case 3:
                        Program.OutputTie();

                        break;
                    default:
                        content.Append(KeyInformation);
                        break;
                }
            }
        }

        public void Spiel()
        {
            string spieler1 = String.Format("*   Spieler 1: {0,-18}  ", Spielfeld.PlayerNames[0].ToString());
            string spieler2 = String.Format("*   Spieler 1: {0,-18}  ", Spielfeld.PlayerNames[0].ToString());
            string star = "*";

            content.AppendLine("");
            content.AppendLine("****************************************");
            content.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            content.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            content.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            content.AppendLine(String.Format("*{0,39}", star));
            content.AppendLine(String.Format("*{0,39}", star));
            content.AppendLine(String.Format("*{0,39}", star));
            content.AppendLine(String.Format("*   Spieler 1: {0,-18}  ", Spielfeld.PlayerNames[0].ToString()));
            content.AppendLine(String.Format("*   Spieler 1: {0,-18}  ", Spielfeld.PlayerNames[1].ToString()));
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("*");
            content.AppendLine("****************************************");
        }


        public void Spieler1Abfragen()
        {
            string zeile = "*";
            if (Spielfeld.PlayerNames[0] != null)
            {
                zeile += String.Format("   Spieler 1: {0,-18}  ",
                    Spielfeld.PlayerNames[0].ToString());
            }
            else
            {
                zeile += "   Spieler 1:                         *";
            }

            content.AppendLine("");
            content.AppendLine("****************************************");
            content.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            content.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            content.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            content.AppendLine("*                                      *");
            content.AppendLine("*   Bitte geben Sie Ihren Namen ein:   *");
            content.AppendLine("*                                      *");
            content.AppendLine(zeile);
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("****************************************");
        }

        public void Spieler2Abfragen()
        {
            string spieler1 = String.Format("*   Spieler 1: {0,-18}  ",
                Spielfeld.PlayerNames[0].ToString());

            string zeile = "*";

            if (Spielfeld.PlayerNames[1] != null)
            {
                zeile += String.Format("   Spieler 2: {0,-18}  ",
                    Spielfeld.PlayerNames[1].ToString());
            }
            else
            {
                zeile += "   Spieler 2:                         *";
            }

            content.AppendLine("");
            content.AppendLine("****************************************");
            content.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            content.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            content.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            content.AppendLine("*                                      *");
            content.AppendLine("*   Bitte geben Sie Ihren Namen ein:   *");
            content.AppendLine("*                                      *");
            content.AppendLine(spieler1);
            content.AppendLine(zeile);
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("****************************************");
        }
    }
}
