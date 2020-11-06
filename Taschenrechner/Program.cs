using System;

namespace Taschenrechner
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator Berechne = new Calculator();
            do
            {

                Berechne.NumberA = UserEingabeZahl("Ihre ersteZahl bitte", Berechne);
                Berechne.Operator = DerOperator();
                Berechne.NumberB = UserEingabeZahl("Ihre zweite Zahl bitte", Berechne);


                Console.WriteLine("Ihr Ergebnis " +
                                  Berechne.Calculate(Berechne.NumberA, Berechne.NumberB, Berechne.Operator));

                Console.WriteLine("Möchten Sie noch eine Berechnung durchführen? (y/n)");

            } while (Console.ReadLine().ToLower() == "y");
        }

        /// <summary>
        /// Erbittet Eingaben vom User für die einzelnen teilen der Gleichung
        /// </summary>
        public static void BenutzerEingabenSammeln()
        {
        }

        /// <summary>
        /// Nimmt Usereingabe für die beschriebene Zahl an
        /// </summary>
        /// <param name="zahl"></param>
        /// <returns>Ganzzahl</returns>
        public static int UserEingabeZahl(string zahl, Calculator calc)
        {
            bool run;
            int ConvertedNumber;

            AusgabeConsole(zahl);
            do
            {
                string Eingabe = Console.ReadLine();

                run = false;

                if (!int.TryParse(Eingabe, out ConvertedNumber))
                {
                    Console.WriteLine("Eingabe war Falsch, bitte nur ganze Zahlen verwenden\n");
                    run = true;
                }
            } while (calc.Operator != Operations.UnSet &&
                     (ConvertedNumber == 0 && calc.Operator == Operations.Modulo ||
                      calc.Operator == Operations.Division));


            return ConvertedNumber;
        }


        

            private static void AusgabeConsole(string zahl)
            {
                Console.Write($"Gib bitte {zahl} ein: ");
            }


            /// <summary>
            /// Bestimmt die Rechenmethode anhand des gelieferten Rechenzeichens vom User
            /// </summary>
            /// <returns>Die Rechnart aus enum</returns>
            public static Operations DerOperator()
            {
                bool run;
                AusgabeConsole("Gib die Rechenart an. Verwende dafür die normalen Rechenzeichen: ");

                do
                {
                    run = false;
                    try
                    {
                        string Eingabe = Console.ReadLine();
                    
                        switch (Eingabe)
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
                        AusgabeConsole("Bitte gib ein korrektes Rechenzeichen an: ");
                    }
                } while (run);

                throw new ArgumentException("Error 180");
            }
        }
    }
