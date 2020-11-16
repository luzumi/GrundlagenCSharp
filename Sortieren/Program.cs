using System;
using System.Linq;

namespace Sortieren
{
    class Program
    {
        private static int[] zahlen = {23, 74, 2, 756, 12, -23, 77, 9, -12, 12, 5};

        static int count = 0;

        static DateTime start = DateTime.Now;


        static void Main(string[] args)
        {
            AusgabeArray();

            Console.WriteLine();

            //Sortieren(zahlen);  //00:00:00.1383326  00:00:00.1071895    00:00:00.1095673
            Sortieren2(zahlen); //00:00:00.0722251  00:00:00.0676002    00:00:00.0955446

            Console.WriteLine();
            Console.WriteLine(DateTime.Now - start);
        }


        private static void AusgabeArray()
        {
            foreach (var i in zahlen)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine();
        }


        static void Sortieren(int[] pZahlen)
        {
            for (int innen = 0; innen < pZahlen.Length; innen++)
            {
                for (int aussen = innen; aussen < pZahlen.Length; aussen++)
                {
                    if (pZahlen[aussen] < pZahlen[innen])
                    {
                        Tauschen(pZahlen, innen, aussen);

                    }
                }
            }
        }


        static void Sortieren2(int[] pZahlen)
        {
            int innen;
            int aussen;

            for ( aussen = 0; aussen < pZahlen.Length-1; aussen++)
            {
                int IdKleinsteZahl = aussen;

                for (innen = aussen+1; innen < pZahlen.Length; innen++)
                {
                    if (pZahlen[aussen] > pZahlen[innen])
                        if (pZahlen[innen] < pZahlen[IdKleinsteZahl])
                        {
                            IdKleinsteZahl = innen;
                        }
                }
                if(aussen != IdKleinsteZahl)
                {
                    Tauschen(pZahlen, aussen, IdKleinsteZahl);
                }
            }
        }


        private static void Tauschen(int[] pZahlen, int pGroessereZahl, int pKleinereZahl)
        {
            int tmp = pZahlen[pGroessereZahl];
            pZahlen[pGroessereZahl] = pZahlen[pKleinereZahl];
            pZahlen[pKleinereZahl] = tmp;
            AusgabeArray();
        }
    }
}
