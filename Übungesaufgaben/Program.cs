using System;
using System.Collections.Generic;

namespace Übungesaufgaben
{
    class Program
    {
        static void Main()
        {
            DateTime now = new DateTime();
            Timer timer = new Timer();
            int counter = 0;

            do
            {
                timer.TimerNow = DateTime.Now - now;

                timer.Counter++;

                if (DateTime.Now - now >= TimeSpan.FromSeconds(1))
                {
                    counter++;
                    if (counter == 5)
                    {
                        Console.SetCursorPosition(Console.BufferWidth-10,0);
                        Console.WriteLine(timer.Counter / 5);
                        counter = 0;
                    }
                    else
                    {
                        timer.Counter = timer.Counter / counter;
                    }

                    timer.Counter = 0;

                    timer.TimerNow = new TimeSpan(0);

                    now = DateTime.Now;
                }
            } while (!Console.KeyAvailable);

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

            ////Primzahlen.PrimeZahl(1000);     //Primzahlenberechnung bis zu einem definierten Maximalwert
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
