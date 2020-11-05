using System;
using System.Collections;
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
                summe += (int)VARIABLE;
            }

            return summe;
        }

        /// <summary>
        /// Berechnet den Durschnitt aller Werte in einem Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        private static int SummeDurchschnitt<T>(IList<T> pArray)
        {
            return SummeArray(pArray) / pArray.Count;
        }

        /// <summary>
        /// summiert den Inhalt eines Arrays
        /// </summary>
        /// <param name="pArray">generische Liste</param>
        /// <returns>Die Summe aller Werte im Array</returns>
        private static int SummeArray<T>(IList <T> pArray)
        {
            FuelleArray(pArray);
            int summe = 0;

            foreach (var VARIABLE in pArray)
            {
                summe += (int)VARIABLE;
            }

            return summe;
        }

        /// <summary>
        /// Füllt Array mit Zufallszahlen 0-100
        /// </summary>
        /// <param name="pArray"></param>
        private static void FuelleArray<T>(IList<T> pArray)
        {
            Random r = new Random();
            for (int i = 0; i < pArray.Count; i++)
            {
                pArray[i] = r.Next(100);
            }
        }

        /// <summary>
        /// Berechnet Fibunaccizahlen bis zu der Länge des´generischen Arrays und schreibt den errechneten Wert in das generische Array
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>generische Array mit Fibunnacizahlen gefüllt</returns>
        public static T IntitilaizeFibunacci<T>(IList<T> pArray)
        {
            pArray[0] = (T)(int)0;
            pArray[1] = 1;
            
            for (int i = 1; i < pArray.Count; i++)
            {
                pArray[i] = i + pArray.;
            }

            return pArray;
        }

        /// <summary>
        ///Berechnet die Fakultät aller Zahlen im der Länge des generischen Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>generisches Array mit Fakultäten</returns>
        public static T Fakultät<T>(IList<T> pArray)
        {
            pArray[0] = 0;
            int Nenner = 1;
            for (int i = 1; i < pArray.Count; i++)
            {
                Nenner = Nenner * i;
                pArray[i] = (T) Nenner;
            }

            return (T) pArray;
        }

        #endregion
    }
}
