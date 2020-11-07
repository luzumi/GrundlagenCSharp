using System;
using System.Collections.Generic;

namespace Übungesaufgaben
{
    class ArrayBerechnungen
    {
        #region Methoden mit festen Datentypen

        /// <summary>
        /// summiert den Inhalt eines Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>Die Summe aller Werte im Array</returns>
        private static int SummeArray(int[] pArray)
        {
            FuelleArray(pArray);
            int summe = 0;

            foreach (var VARIABLE in pArray)
            {
                summe += VARIABLE;
            }

            return summe;
        }

        /// <summary>
        /// Füllt Array mit Zufallszahlen 0-100
        /// </summary>
        /// <param name="pArray"></param>
        private static void FuelleArray(int[] pArray)
        {
            Random r = new Random();

            for (int i = 0; i < pArray.Length; i++)
            {
                pArray[i] = r.Next(100);
            }
        }

        /// <summary>
        /// Berechnet den Durschnitt aller Werte in einem Array
        /// </summary>
        /// <typeparam T> 
        ///     <name>T</name>
        /// </typeparam>
        /// <param name="pArray"></param>
        /// <returns></returns>
        private static int SummeDurchschnitt(int[] pArray)
        {
            return SummeArray(pArray) / pArray.Length;
        }

        /// <summary>
        /// Berechnet Fibunaccizahlen bis zu der Länge des´Arrays und schreibt den errechneten Wert in das Array
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>Array mit Fibunnacizahlen gefüllt</returns>
        public static int[] IntitilaizeFibunacci(int[] pArray)
        {
            pArray[0] = 0;
            pArray[1] = 1;

            for (int i = 1; i < pArray.Length; i++)
            {
                pArray[i] = i + pArray[i];
            }

            return pArray;
        }

        /// <summary>
        ///Berechnet die Fakultät aller Zahlen im der Länge des Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>Array mit Fakultäten</returns>
        public static int[] Fakultät(int[] pArray)
        {
            pArray[0] = 0;
            int Nenner = 1;
            for (int i = 1; i < pArray.Length; i++)
            {
                Nenner = Nenner * i;
                pArray[i] = Nenner;
            }

            return pArray;
        }

        #endregion

        #region überladene Methoden mit generischen Datentypen

        /// <summary>
        /// Berechnet den Durschnitt aller Werte in einem generischem Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pArray"></param>
        /// <returns>gibt ganzzahligen Durchnittswert des generischen Arrays zurück</returns>
        private static int SummeDurchschnitt<T>(List<T> pArray) where T : struct
        {
            decimal summe = (decimal)Convert.ChangeType(pArray.Count, typeof(List<T>)) /
                            (decimal)Convert.ChangeType(pArray, typeof(List<T>));
            return (int)summe;
        }

        /// <summary>
        /// summiert den Inhalt eines Arrays
        /// </summary>
        /// <param name="pArray">generische Liste</param>
        /// <returns>Die Summe aller Werte im Array</returns>
        private static int SummeArray<T>(List<T> pArray) where T : struct
        {
            int summe = 0;
            if (pArray[0].Equals(typeof(int)))
            {
                foreach (T VARIABLE in pArray)
                {
                    summe += (int)Convert.ChangeType(VARIABLE, typeof(List<T>));
                }
            }

            return summe;
        }

        /// <summary>
        /// Füllt Array mit Zufallszahlen 0-100
        /// </summary>
        /// <param name="pArray"></param>
        public static void FuelleArray<T>(ref List<T> pArray) where T : struct
        {
            int laenge = pArray.Capacity;
            Random r = new Random();
            pArray = new List<T>(laenge);

            for (int i = 0; i < laenge; i++)
            {
                pArray.Add((T)Convert.ChangeType(r.Next(100), typeof(T)));
            }
        }

        /// <summary>
        /// Berechnet Fibunaccizahlen bis zu der Länge des´generischen Arrays und schreibt den errechneten Wert in das generische Array
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>generische Array mit Fibunnacizahlen gefüllt</returns>
        public static List<T> IntitilaizeFibunacci<T>(ref List<T> pArray) where T : struct
        {
            //TODO System.InvalidCastException
            //HResult=0x80004002
            //Message=Invalid cast from 'System.Int32' to 'System.Collections.Generic.List`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]'.
            //    Source=System.Private.CoreLib
            //StackTrace:

            int laenge = pArray.Capacity;
            pArray = new List<T>(laenge);

            pArray.Add((T)Convert.ChangeType(0, typeof(T)));
            pArray.Add((T)Convert.ChangeType(1, typeof(T)));

            for (int i = 2; i < laenge; i++)
            {
                pArray.Add((T)Convert.ChangeType(((decimal)Convert.ChangeType(pArray[i - 1], typeof(decimal)) + 
                                                            (decimal)Convert.ChangeType(pArray[i - 2], typeof(decimal))), typeof(T)));
            }

            return pArray;
        }

        /// <summary>
        ///Berechnet die Fakultät aller Zahlen im der Länge des generischen Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>generisches Array mit Fakultäten</returns>
        public static T Fakultät<T>(List<T> pArray) where T : struct
        {
            pArray.Add((T)Convert.ChangeType(0, typeof(T)));
            decimal Nenner = 1;
            for (int i = 1; i < pArray.Capacity; i++)
            {
                Nenner = Nenner * i;
                //pArray.Add((T)Convert.ChangeType(Nenner, typeof(T)));
            }

            return (T)Convert.ChangeType(Nenner, typeof(T));
        }

        #endregion
    }
}
