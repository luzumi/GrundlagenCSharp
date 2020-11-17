using System;
using System.Collections.Generic;
using System.Threading;


namespace Übungesaufgaben
{
    class Program
    {
        static DateTime dt = DateTime.Now;
        private static int counter = 0;
        static List<List<int>> LottoScheine = new List<List<int>>();
        static List<int> Gewinnschein = new List<int>(8);
        static int count = 0;
        static Random r = new Random();

        static void Main()
        {
            Willkommen();

            Console.CursorVisible = false;

            Gewinnschein = EuroJackpot(new List<int>(), new List<int>());


            Thread.Sleep(500);
            List<int> meinTip = ErstelleTipSchein();
            Console.SetCursorPosition(15, 9);
            Console.WriteLine("Sie hatten {0} Zahlen richtig",GewinnCheck(Gewinnschein, meinTip));
            

            Console.WriteLine("Die gezogenen Lottzahlen sind:    " + AusgabeSchein(Gewinnschein));
            Console.WriteLine("Ihre getippten Lottozahlen waren: " + AusgabeSchein(meinTip));
            
            

            Console.WriteLine("out");


            Console.ReadLine();
        }

        private static void Willkommen()
        {
            //Console.CursorVisible = false;
            Console.SetCursorPosition(12, 2);
            Console.Write("LOTTO");
            Console.SetCursorPosition(8, 4);
            Console.Write("Willkommen, möchten Sie einen vorgefertigten Tip abgeben? [y,n]");

            ErstelleTipSchein();
        }

        private static List<int> ErstelleTipSchein()
        {
            if (CheckEingabe(Console.ReadKey().Key) == "N")
            {
                return LottoscheinErstellenManuell();
            }
            if (CheckEingabe(Console.ReadKey().Key) == "Y")
            {
                return LottoscheinErstellenAutomatisch();
            }

            return null;
        }

        private static List<int> LottoscheinErstellenAutomatisch()
        {
            List<int> MeinSchein = new List<int>(8);

            for (int i = 0; i < 7; i++)
            {
                MeinSchein.Add(r.Next(1, 51));
            }

            return MeinSchein;
        }

        private static List<int> LottoscheinErstellenManuell()
        {
            List<int> MeinSchein = new List<int>(8);
            ;
            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(8+ 10*i, 8);
                Console.Write("Zahl{0}: ", (i + 1));

                MeinSchein.Add(CheckNumber(Console.ReadLine()));
            }

            return MeinSchein;
        }

        private static int CheckNumber(string pReadKey)
        {
            
            if (!int.TryParse(pReadKey, out var userZahl) && userZahl > 0 && userZahl < 51)
            {
                Console.SetCursorPosition(8,5);
                Console.Write("Die Eingabe entspricht keiner gültigen Lottozahl!");
                count++;
                if (count <= 3)
                {
                    CheckNumber(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Keine Zeit für Späße! bye bye");
                }
            }

            return userZahl;
        }



        private static string CheckEingabe(ConsoleKey pReadKey)
        {

            return  pReadKey == ConsoleKey.Y || pReadKey == ConsoleKey.N ? pReadKey.ToString() : "Bitte mit 'Y' oder 'N' antworten: ";
        }


        /// <summary>
        /// erstellt 10sek lang Lottoscheine
        /// </summary>
        private static void ErstelleLottoScheine()
        {
            for (; DateTime.Now - dt <= TimeSpan.FromSeconds(10);)
            {
                List<int> Liste1 = new List<int>();
                List<int> Liste2 = new List<int>();

                LottoScheine.Add(EuroJackpot(Liste1, Liste2));
                Console.SetCursorPosition(40, 1);
                Console.Write("Anzahl Lottoscheine: " + counter++);
            }
        }

        /// <summary>
        /// Ausgabe aller lottoscheine auf der Console
        /// </summary>
        private static void AusgabeAlleLottoscheine()
        {
            foreach (var lottoschein in LottoScheine)
            {
                string console = GewinnCheck(Gewinnschein, lottoschein) > 2
                    ? AusgabeSchein(lottoschein) + " " + lottoschein[7] +
                      " Treffer und " + ZusatzzahlenCheck(Gewinnschein, lottoschein) +
                      " Zusatzzahlen"
                    : AusgabeSchein(lottoschein) + " " + lottoschein[7] +
                      " Treffer";
                Console.WriteLine(console);
            }
        }


        /// <summary>
        /// Ermittelt die Anzahl der Treffer jedes Lottoscheins
        /// </summary>
        private static void SummeTrefferscheine(List<int> Lottoscheine)
        {
            int[] treffer = {0, 0, 0, 0, 0, 0};
            foreach (var Treffer in LottoScheine)
            {
                GewinnCheck(Gewinnschein, Treffer);
                treffer[Treffer[7]]++;
            }

            //Ausgabe des Ergebnisses auf der Console
            for (int anzahlTreffer = 0; anzahlTreffer < treffer.Length; anzahlTreffer++)
            {
                Console.WriteLine("Es gab insgesamt {0} Scheine mit {1} Treffer! ", treffer[anzahlTreffer],
                    anzahlTreffer);
            }
        }


        /// <summary>
        /// Zählt Treffer eines Scheins und Added Anzahl an Lottoschein 
        /// </summary>
        /// <param name="pGewinnschein"></param>
        /// <param name="pLottoScheine"></param>
        /// <returns></returns>
        private static int GewinnCheck(List<int> pGewinnschein, List<int> pLottoScheine)
        {
            pLottoScheine.Add(0);
            int treffer = 0;

            for (int zahl = 0; zahl < 5; zahl++)
            {
                if (pLottoScheine[zahl] == pGewinnschein[zahl])
                {
                    treffer++;
                }
            }

            pLottoScheine[7] = treffer;
            return treffer;
        }


        /// <summary>
        /// Überprüft die Zusatzzahlen eines Scheins
        /// </summary>
        /// <param name="pGewinnschein"></param>
        /// <param name="pLottoScheine"></param>
        /// <returns>Anzahl getroffener Zusatzzahlen</returns>
        private static int ZusatzzahlenCheck(List<int> pGewinnschein, List<int> pLottoScheine)
        {
            int treffer = 0;

            for (int zahl = 5; zahl < 7; zahl++)
            {
                if (pLottoScheine[zahl] == pGewinnschein[zahl])
                {
                    treffer++;
                }
            }

            return treffer;
        }


        /// <summary>
        /// Gibt einzelnen Schein auf der Console aus
        /// </summary>
        /// <param name="pLottoschein"></param>
        /// <returns>String der Lottozahlen</returns>
        private static string AusgabeSchein(List<int> pLottoschein)
        {
            string zahlen = "";

            for (int zahl = 0; zahl < 5; zahl++)
            {
                zahlen += pLottoschein[zahl] + ", ";
            }

            zahlen += "Zusatzzahlen: ";

            for (int zahl = 5; zahl < 7; zahl++)
            {
                zahlen += pLottoschein[zahl] + ", ";
            }

            return zahlen;
        }


        static void PrintLotto(List<int> Numbers, List<int> Special)
        {
            Console.Write("EuroJackpot Zahlen:");
            foreach (var item in Numbers)
            {
                Console.Write("{0,3}", item);
            }

            Console.Write(" Zusatz:");
            foreach (var item in Special)
            {
                Console.Write("{0,3}", item);
            }

            Console.WriteLine();
        }

        static List<int> EuroJackpot(List<int> Numbers, List<int> Special)
        {
            Random rndGen = new Random();


            while (Numbers.Count < 5)
            {
                int newNumber = rndGen.Next(1, 51);
                bool insertAllowed = true;

                foreach (var item in Numbers)
                {
                    if (item == newNumber)
                    {
                        insertAllowed = false;
                        break;
                    }
                }

                if (insertAllowed)
                {
                    Numbers.Add(newNumber);
                }
            }

            //Zusatzzahlen
            while (Numbers.Count < 7)
            {
                int newNumber = rndGen.Next(1, 11);
                bool insertAllowed = true;

                foreach (var item in Special)
                {
                    if (item == newNumber)
                    {
                        insertAllowed = false;
                        break;
                    }
                }

                if (insertAllowed)
                {
                    Numbers.Add(newNumber);
                }
            }

            return Numbers;
        }
    }
}
