using System;
using System.Collections.Generic;
using System.Threading;


namespace Übungesaufgaben
{
    class Program
    {
        static void Main()
        {
            Thread newThread = new Thread(Timer.FpsChecker)
            {
                Name = "FpsChecker"
            };

            newThread.Start();
                
            Primzahlen.PrimeZahl(30000);     //Primzahlenberechnung bis zu einem definierten Maximalwert

            
            ////int[] Array = new int[20];
            ////long[] ArrayLong = new long[20];

            ////Console.WriteLine(SummeArray(Array));
            ////AusgabeArray(ArrayBerechnungen.IntitilaizeFibunacci(Array));
            ////Console.WriteLine();
            ////AusgabeArray(ArrayBerechnungen.Fakultät(Array));


            //List<int> listInt = new List<int>(13);
            //List<long> listLong = new List<long>(20);


            //Console.WriteLine(ArrayBerechnungen.Fakultät<int>(listInt));
            //Console.WriteLine(ArrayBerechnungen.Fakultät<long>(listLong));

            //Console.WriteLine();

            //ArrayBerechnungen.FuelleArray(ref listInt);
            //ArrayBerechnungen.FuelleArray(ref listLong);

            //AusgabeArray<int>(listInt);
            //AusgabeArray<long>(listLong);

            //Console.WriteLine();
            //listLong = new List<long>(50);
            //ArrayBerechnungen.IntitilaizeFibunacci<int>(ref listInt);
            //ArrayBerechnungen.IntitilaizeFibunacci<long>(ref listLong);

            //AusgabeArray<int>(listInt);
            //AusgabeArray<long>(listLong);


            //Console.ReadLine();

        }

        


        /// <summary>
        /// Gibt alle Elemente des Arrays in einer Zeile auf der Console aus
        /// </summary>
        /// <param name="pArray"></param>
        static void AusgabeArray(int[] pArray)
        {
            foreach (var item in pArray)
            {
                Console.Write("{0} ", item);
            }
        }

        /// <summary>
        /// Gibt alle Elemente des generischen Arrays in einer Zeile auf der Console aus
        /// </summary>
        /// <param name="pArray">generisches Array</param>
        static void AusgabeArray<T>(List<T> pArray)
        {
            foreach (var item in pArray)
            {
                Console.Write("{0} ", item);
            }
        }
    }
}
