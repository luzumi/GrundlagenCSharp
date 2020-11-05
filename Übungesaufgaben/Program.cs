using System;
using System.Collections.Generic;

namespace Übungesaufgaben
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] Array = new int[20];
            long[] ArrayLong = new long[20];

            //Console.WriteLine(SummeArray(Array));

            AusgabeArray(ArrayBerechnungen.IntitilaizeFibunacci(Array));

            Console.WriteLine();

            AusgabeArray(ArrayBerechnungen.Fakultät(Array));

            Console.ReadLine();
            //Primzahlen.PrimeZahl(1000);     //Primzahlenberechnung bis zu einem definierten Maximalwert
        }


        /// <summary>
        /// Gibt alle Elemente des Arrays in einer Zeile auf der Console aus
        /// </summary>
        /// <param name="pArray"></param>
        static void AusgabeArray(int[] pArray)
        {
            foreach (var VARIABLE in pArray)
            {
                Console.Write("{0} ", VARIABLE);
            }
        }

        /// <summary>
        /// Gibt alle Elemente des generischen Arrays in einer Zeile auf der Console aus
        /// </summary>
        /// <param name="pArray">generisches Array</param>
        static void AusgabeArray<T>(IList<T> pArray)
        {
            foreach (var VARIABLE in pArray)
            {
                Console.Write("{0} ", VARIABLE);
            }
        }
    }
}
