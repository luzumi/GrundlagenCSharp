using System;

namespace Übungesaufgaben
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] Array = new int[20];
            long[] ArrayLong = new long[20];

            //Console.WriteLine(SummeArray(Array));

            AusgabeArray(IntitilaizeFibunacci(ArrayLong));

            Console.WriteLine();

            AusgabeArray(Fakultät(ArrayLong));

            Console.ReadLine();
            //Primzahlen.PrimeZahl(1000);     //Primzahlenberechnung bis zu einem definierten Maximalwert
        }


        /// <summary>
        /// summiert den Inhalt eines Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>Die Summe aller Werte im Array</returns>
        static int SummeArray(long[] pArray)
        {
            FuelleArray(pArray);
            int summe = 0;

            foreach (int VARIABLE in pArray)
            {
                summe += VARIABLE;
            }

            return summe;
        }

        /// <summary>
        /// Füllt Array mit Zufallszahlen 0-100
        /// </summary>
        /// <param name="pArray"></param>
        static void FuelleArray(long[] pArray)
        {
            Random r = new Random();
            for (int i = 0; i < pArray.Length; i++)
            {
                pArray[i] = r.Next(100);
            }
        }

        /// <summary>
        /// Berechnet Fibunaccizahlen bis zu der Länge des´Arrays und schreibt den errechneten Wert in das Array
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>Array mit Fibunnacizahlen gefüllt</returns>
        static long[] IntitilaizeFibunacci(long[] pArray)
        {
            pArray[0] = 0;
            pArray[1] = 1;
            
            for (int i = 1; i < pArray.Length; i++)
            {
                pArray[i] = i + pArray[i - 1];
            }

            return pArray;
        }

        /// <summary>
        ///Berechnet die Fakultät aller Zahlen im der Länge des Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>Array mit Fakultäten</returns>
        static long[] Fakultät(long[] pArray)
        {
            pArray[0] = 0;
            long Nenner = 1;
            for (int i = 1; i < pArray.Length; i++)
            {
                Nenner = Nenner * i;
                pArray[i] = Nenner;
            }

            return pArray;
        }


        /// <summary>
        /// Gibt alle Elemente des Arrays in einer Zeile auf der Console aus
        /// </summary>
        /// <param name="pArray"></param>
        static void AusgabeArray(long[] pArray)
        {
            foreach (var VARIABLE in pArray)
            {
                Console.Write("{0} ", VARIABLE);
            }
        }
    }
}
