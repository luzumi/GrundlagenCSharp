using System;
using System.Collections.Generic;

namespace Übungesaufgaben
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] Array = new int[20];
            //long[] ArrayLong = new long[20];

            //Console.WriteLine(SummeArray(Array));
            //AusgabeArray(ArrayBerechnungen.IntitilaizeFibunacci(Array));
            //Console.WriteLine();
            //AusgabeArray(ArrayBerechnungen.Fakultät(Array));


            List<int> ListInt = new List<int>(13);
            List<long> ListLong = new List<long>(20);


            Console.WriteLine(ArrayBerechnungen.Fakultät<int>(ListInt));
            Console.WriteLine(ArrayBerechnungen.Fakultät<long>(ListLong));
            
            Console.WriteLine();

            ArrayBerechnungen.FuelleArray(ref ListInt);
            ArrayBerechnungen.FuelleArray(ref ListLong);

            AusgabeArray<int>(ListInt);
            AusgabeArray<long>(ListLong);

            Console.WriteLine();
            ListLong = new List<long>(50);
            ArrayBerechnungen.IntitilaizeFibunacci<int>(ref ListInt);
            ArrayBerechnungen.IntitilaizeFibunacci<long>(ref ListLong);

            AusgabeArray<int>(ListInt);
            AusgabeArray<long>(ListLong);



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
        static void AusgabeArray<T>(List<T> pArray)
        {
            foreach (var VARIABLE in pArray)
            {
                Console.Write("{0} ", VARIABLE);
            }
        }
    }
}
