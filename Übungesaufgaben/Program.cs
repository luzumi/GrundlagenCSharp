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

        static void Main()
        {
            Timer fps = new Timer();
            //do
            //{
                //Primzahlen.PrimeZahl(300);     //Primzahlenberechnung bis zu einem definierten Maximalwert


                Console.CursorVisible = false;
                counter++;

                Gewinnschein = EuroJackpot(new List<int>(), new List<int>());

                for (; DateTime.Now - dt <= TimeSpan.FromSeconds(10);)
                {
                    List<int> Liste1 = new List<int>();
                    List<int> Liste2 = new List<int>();

                    LottoScheine.Add(EuroJackpot(Liste1, Liste2));
                    Console.SetCursorPosition(40,1);
                    Console.Write("Anzahl Lottoscheine: " + counter++);
                }

                Console.WriteLine(LottoScheine.Count);

                Thread.Sleep(500);

                //AusgabeAlleLottoscheine();

                SummeTrefferscheine();

                Console.WriteLine("out");


                fps.FpsChecker();
            //} while (counter == 0);
            //} while (!Console.KeyAvailable);


            Console.ReadLine();
        }

        private static void AusgabeAlleLottoscheine()
        {
            foreach (var lottoschein in LottoScheine)
            {
                string console = GewinnCheck(Gewinnschein, lottoschein) > 2
                    ? AusgabeSchein(lottoschein) + " " + AusgabeTreffer(lottoschein) +
                      " Treffer und " + ZusatzzahlenCheck(Gewinnschein, lottoschein) +
                      " Zusatzzahlen"
                    : AusgabeSchein(lottoschein) + " " + AusgabeTreffer(lottoschein) +
                      " Treffer";
                Console.WriteLine(console);
            }
        }

        private static void SummeTrefferscheine()
        {
            int[] treffer = {0,0,0,0,0};
            foreach (var Treffer in LottoScheine)
            {
                GewinnCheck(Gewinnschein, Treffer);
                switch (Treffer[7])
                {
                    case 0:
                        treffer[0]++;
                        break;
                    case 1 :
                        treffer[1]++;
                        break;
                    case 2:
                        treffer[2]++;
                        break;
                    case 3:
                        treffer[3]++;
                        break;
                    case 4:
                        treffer[4]++;
                        break;
                    case 5:
                        treffer[5]++;
                        break;
                    default:
                        break;
                }
            }

            for (int anzahlTreffer = 0; anzahlTreffer < treffer.Length; anzahlTreffer++)
            {
                Console.WriteLine("Es gab insgesamt {0} Scheine mit {1} Treffer! ", treffer[anzahlTreffer], anzahlTreffer);
            }
        }

        private static string AusgabeTreffer(List<int> lottoschein)
        {
            return " - " + GewinnCheck(Gewinnschein, lottoschein) + " Treffer";
        }


        private static int GewinnCheck(List<int> pGewinnschein, List<int> pLottoScheine)
        {
            int treffer = 0;

            for (int zahl = 0; zahl < 5; zahl++)
            {
                if (pLottoScheine[zahl] == pGewinnschein[zahl])
                {
                    treffer++;
                    
                }
            }
            pLottoScheine.Add(treffer);
            return treffer;
        }


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
