using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class TextBox
    {
        private StringBuilder content;
        private readonly Point position;
        private readonly ConsoleColor color;
        private static int horizonzal = 18;
        private static int vertikal = 19;

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
        }

        public TextBox(Point Position, ConsoleColor TextColor)
        {
            position = Position;
            color = TextColor;
            content = new StringBuilder();
        }

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
                            //StartContent();
                            Program.ResetBoard2();
                        }

                        break;
                    case 2:
                        content.Clear();
                        Spiel();
                        break;
                    default:
                        content.Append(KeyInformation);
                        break;
                }
            }
        }

        public void StartContent()
        {
            string star = "*";
            content.AppendLine("");
            content.AppendLine("****************************************");
            content.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            content.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            content.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
            content.AppendLine(String.Format("*{0,39}",star));
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


        public void Spiel()
        {
            string spieler1 = String.Format("*   Spieler 1: {0,-18}  ", Spielfeld.PlayerNames[0].ToString());
            string spieler2 = String.Format("*   Spieler 1: {0,-18}  ", Spielfeld.PlayerNames[0].ToString());

            string star = "-";

            content.AppendLine("");
            content.AppendLine("****************************************");
            content.AppendLine("* TTT I  CC  TTT  A    CC  TTT OO  EEE *");
            content.AppendLine("*  T  I C     T  A A  C     T O  O EE  *");
            content.AppendLine("*  T  I  CC   T A   A  CC   T  OO  EEE *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine("*                                      *");
            content.AppendLine(spieler1);
            content.AppendLine(star);
            content.AppendLine("*                                      *");
            content.AppendLine("*          +---+---+---+               *");
            content.AppendLine("*          |   |   |   |               *");
            content.AppendLine("*          +---+---+---+               *");
            content.AppendLine("*          |   |   |   |               *");
            content.AppendLine("*          +---+---+---+               *");
            content.AppendLine("*          |   |   |   |               *");
            content.AppendLine(String.Format("*          +---+---+---+{0,16}",star));
            content.AppendLine("****************************************");
        }
    }
} //" + Spielfeld.PlayerNames[0] + "
