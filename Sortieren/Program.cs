using System;

namespace Sortieren
{
    class Program
    {
        private static int[] zahlen;
        private static int[] scratch;
        static DateTime start = DateTime.Now;
        static Random rnd = new Random();


        static void Main()
        {

            Console.WriteLine("MergeSort: 1000 Zahlen....................");
            for (int i = 0; i < 30; i++)
            {
                CreateNewArray(1000);
                start = DateTime.Now;
                MergeSort(zahlen);
                Console.WriteLine(DateTime.Now - start);
            }

            Console.WriteLine("MergeSortOptimized: 1000 Zahlen...........");
            for (int i = 0; i < 30; i++)
            {
                CreateNewArray(1000);
                start = DateTime.Now;
                MergeSortOptimized(zahlen);
                Console.WriteLine(DateTime.Now - start);
            }

            
            //SelectionSort(zahlen);  //00:00:00.1383326  00:00:00.1071895    00:00:00.1095673
            //SelectionsSortOptimized(zahlen); //00:00:00.0722251  00:00:00.0676002    00:00:00.0955446

            Console.ReadLine();
        }

        private static void CreateNewArray( int anzahl)
        {
            zahlen = new int[anzahl];
            scratch = new int[zahlen.Length];

            for (int i = 0; i < zahlen.Length; i++)
            {
                zahlen[i] = rnd.Next();
            }
        }


        private static void AusgabeArray(int[] pZahlen)
        {
            foreach (var i in pZahlen)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine();
        }


        static void SelectionSort(int[] pZahlen)
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


        static void SelectionsSortOptimized(int[] pZahlen)
        {
            int innen;
            int aussen;

            for (aussen = 0; aussen < pZahlen.Length - 1; aussen++)
            {
                int IdKleinsteZahl = aussen;

                for (innen = aussen + 1; innen < pZahlen.Length; innen++)
                {
                    if (pZahlen[aussen] > pZahlen[innen])
                        if (pZahlen[innen] < pZahlen[IdKleinsteZahl])
                        {
                            IdKleinsteZahl = innen;
                        }
                }

                if (aussen != IdKleinsteZahl)
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
            AusgabeArray(pZahlen);
        }


        private static int[] MergeSort(int[] pZahlen)
        {
            //teilen

            if (pZahlen.Length == 1) return pZahlen;

            
            int[] linkesFeld = new int[pZahlen.Length / 2];
            int[] rechtesFeld = new int[pZahlen.Length - linkesFeld.Length];

            for (int i = 0; i < linkesFeld.Length; i++) { linkesFeld[i] = pZahlen[i]; }

            for (int i = 0; i < rechtesFeld.Length; i++) { rechtesFeld[i] = pZahlen[i + linkesFeld.Length] ; }

            linkesFeld = MergeSort(linkesFeld);
            rechtesFeld = MergeSort(rechtesFeld);

            //vergleichen
            int zeigerLinks = 0;
            int zeigerRechts = 0;
            int zeigerPZahlen = 0;

            while(zeigerLinks < linkesFeld.Length && zeigerRechts < rechtesFeld.Length)
            {
                if (rechtesFeld[zeigerRechts] < linkesFeld[zeigerLinks])                   
                {
                    pZahlen[zeigerPZahlen++] = rechtesFeld[zeigerRechts++];
                }
                else 
                {
                    pZahlen[zeigerPZahlen++] = linkesFeld[zeigerLinks++];
                }
            }

            while (zeigerLinks < linkesFeld.Length)
            {
                pZahlen[zeigerPZahlen++] = linkesFeld[zeigerLinks++];
            }

            while (zeigerRechts < rechtesFeld.Length)
            {
                pZahlen[zeigerPZahlen++] = rechtesFeld[zeigerRechts++];
            }

            return pZahlen;
        }


        private static void MergeSortOptimized(int[] pZahlen)
        {
            MergeSortOptimizedSorter(pZahlen, new int[pZahlen.Length], 0, pZahlen.Length-1);
        }


        private static void MergeSortOptimizedSorter(int[] pZahlen, int[] pScratch, int start, int end)
        {
            //teilen

            if (start == end) return;

            int splitpoint = (end - start) /2 + start;

            MergeSortOptimizedSorter(pZahlen, pScratch, start, splitpoint);
            MergeSortOptimizedSorter(pZahlen, pScratch, splitpoint +1, end);


            //vergleichen
            int zeigerLinks = start;
            int zeigerRechts = splitpoint+1;
            int zeigerErgebnis = start;

            while(zeigerLinks <= splitpoint  && zeigerRechts <= end)
            {
                if (pZahlen[zeigerRechts] < pZahlen[zeigerLinks])                   
                {
                    pScratch[zeigerErgebnis++] = pZahlen[zeigerRechts++];
                }
                else 
                {
                    pScratch[zeigerErgebnis++] = pZahlen[zeigerLinks++];
                }
            }

            while (zeigerLinks <= splitpoint)
            {
                pScratch[zeigerErgebnis++] = pZahlen[zeigerLinks++];
            }

            while (zeigerRechts <= end)
            {
                pScratch[zeigerErgebnis++] = pZahlen[zeigerRechts++];
            }

            for (int counter = start; counter <= splitpoint; counter++)
            {
                pZahlen[counter] = pScratch[counter];
            }
        }
    }
}
