using System;

namespace Taschenrechner
{
    static class Program
    {
        static void Main()
        {
            Calculator berechnung = new Calculator();
            do
            {
                BenutzerEingabeDerGleichung(berechnung);

                Console.WriteLine("Ihr Ergebnis " +
                                  berechnung.Calculate(berechnung.NumberA, berechnung.NumberB, berechnung.Operator));

                VordergrundFarbauswahl();

                Console.WriteLine("Möchten Sie noch eine Berechnung durchführen? (y/n)");

            } while (Console.ReadLine()?.ToLower() == "y");
        }


        private static void BenutzerEingabeDerGleichung(Calculator berechnung)
        {
            berechnung.NumberA = UserEingabeZahl("Deine erste Zahl ", berechnung);
            berechnung.Operator = BestimmeOperator();
            berechnung.NumberB = UserEingabeZahl("Deine zweite Zahl ", berechnung);
        }


        /// <summary>
        /// Nimmt Usereingabe für die beschriebene Zahl an
        /// </summary>
        /// <param name="gefordeZahl"> </param>
        /// <param name="calc">Instanz vom Calculator, wir benötigt um aktuellen operator auszulesen</param>
        /// <returns>Ganzzahl</returns>
        private static int UserEingabeZahl(string gefordeZahl, Calculator calc)
        {
            bool run;
            int convertedNumber;

            do
            {
                AusgabeConsole(gefordeZahl);
                string input = Console.ReadLine();

                run = false;

                if (!int.TryParse(input, out convertedNumber))
                {
                    FehlerMeldung("Eingabe war Falsch, bitte nur ganze Zahlen verwenden\n");
                    run = true;
                }
                else if (calc.Operator != Operations.UnSet &&
                         (convertedNumber == 0 && (calc.Operator == Operations.Modulo ||
                                                   calc.Operator == Operations.Division)))
                {
                    FehlerMeldung(
                        "Eingabe war Falsch, bitte beachten sie das eine Division oder Modulorechnung mit '0' nicht definiert ist! ");
                    run = true;
                }
            } while (run);


            return convertedNumber;
        }


        private static void AusgabeConsole(string zahl)
        {
            Console.Write($"Gib bitte {zahl} ein: ");
        }


        /// <summary>
        /// Bestimmt die Rechenmethode anhand des gelieferten Rechenzeichens vom User
        /// </summary>
        /// <returns>Die Rechnart aus enum</returns>
        private static Operations BestimmeOperator()
        {
            bool run;
            AusgabeConsole("Gib die Rechenart an. Verwende dafür die normalen Rechenzeichen: ");

            do
            {
                run = false;
                try
                {
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "+":
                            return Operations.Addition;
                        case "-":
                            return Operations.Substraktion;
                        case "*":
                            return Operations.Multiplikation;
                        case "/":
                            return Operations.Division;
                        case "%":
                            return Operations.Modulo;
                        default:
                            run = true;
                            throw new ArgumentException("Error 172");
                    }
                }
                catch (Exception)
                {
                    FehlerMeldung("Bitte gib ein korrektes Rechenzeichen an: ");
                }
            } while (run);

            throw new ArgumentException("Error 180");
        }


        public static void VordergrundFarbauswahl()
        {
            Random r = new Random();

            Console.ForegroundColor = r.Next(10) switch
            {
                0 => ConsoleColor.Yellow,
                1 => ConsoleColor.Cyan,
                2 => ConsoleColor.DarkGray,
                3 => ConsoleColor.Red,
                4 => ConsoleColor.Magenta,
                5 => ConsoleColor.White,
                6 => ConsoleColor.DarkYellow,
                7 => ConsoleColor.Blue,
                8 => ConsoleColor.DarkCyan,
                9 => ConsoleColor.Gray,
                _ => ConsoleColor.Yellow
            };
        }

        /// <summary>
        /// Consolenausgabe in gelber Schrift auf rotem Hintergrund
        /// </summary>
        /// <paramref name="text"><code>Fehlertext</code></paramref>
        private static void FehlerMeldung(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine(text);

            Console.ResetColor();
        }
    }
}
