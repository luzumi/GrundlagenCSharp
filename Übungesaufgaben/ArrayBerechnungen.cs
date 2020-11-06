using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
        private static void FuelleArray<T>(List<T> pArray) where T : struct
        {
            Random r = new Random();

            for (int i = 0; i < pArray.Count; i++)
            {
                pArray[i] = (T)Convert.ChangeType(r.Next(100), typeof(List<T>));
            }
        }

        /// <summary>
        /// Berechnet Fibunaccizahlen bis zu der Länge des´generischen Arrays und schreibt den errechneten Wert in das generische Array
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>generische Array mit Fibunnacizahlen gefüllt</returns>
        public static T IntitilaizeFibunacci<T>(List<T> pArray) where T : struct
        {
            //TODO System.InvalidCastException
            //HResult=0x80004002
            //Message=Invalid cast from 'System.Int32' to 'System.Collections.Generic.List`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]'.
            //    Source=System.Private.CoreLib
            //StackTrace:
            //at System.Convert.DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
            //at System.Int32.System.IConvertible.ToType(Type type, IFormatProvider provider)
            //at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
            //at System.Convert.ChangeType(Object value, Type conversionType)
            //at Übungesaufgaben.ArrayBerechnungen.IntitilaizeFibunacci[T](List`1 pArray) in E:\VisualStudio-workspace\AnwendungsentwicklungTeil1\Kontrollstrukturen\Übungesaufgaben\ArrayBerechnungen.cs:line 147
            //at Übungesaufgaben.Program.Main(String[] args) in E:\VisualStudio-workspace\AnwendungsentwicklungTeil1\Kontrollstrukturen\Übungesaufgaben\Program.cs:line 24

            pArray.Insert(0, (T)Convert.ChangeType(0, typeof(List<T>)));
            pArray.Insert(1, (T)Convert.ChangeType(0, typeof(List<T>)));

            for (int i = 1; i < pArray.Count; i++)
            {
                decimal summe = (decimal)Convert.ChangeType(i, typeof(List<T>)) +
                                (decimal)Convert.ChangeType(pArray[i], typeof(List<T>));
                pArray[i] = (T)Convert.ChangeType(summe, typeof(List<T>));
            }

            return (T)Convert.ChangeType(pArray, typeof(List<T>));
        }

        /// <summary>
        ///Berechnet die Fakultät aller Zahlen im der Länge des generischen Arrays
        /// </summary>
        /// <param name="pArray"></param>
        /// <returns>generisches Array mit Fakultäten</returns>
        public static T Fakultät<T>(List<T> pArray) where T : struct
        {
            //TODO System.InvalidCastException
            //HResult=0x80004002
            //Message=Invalid cast from 'System.Int32' to 'System.Collections.Generic.List`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]'.
            //    Source=System.Private.CoreLib
            //StackTrace:
            //at System.Convert.DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
            //at System.Int32.System.IConvertible.ToType(Type type, IFormatProvider provider)
            //at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
            //at System.Convert.ChangeType(Object value, Type conversionType)
            //at Übungesaufgaben.ArrayBerechnungen.Fakultät[T](List`1 pArray) in E:\VisualStudio-workspace\AnwendungsentwicklungTeil1\Kontrollstrukturen\Übungesaufgaben\ArrayBerechnungen.cs:line 179
            //at Übungesaufgaben.Program.Main(String[] args) in E:\VisualStudio-workspace\AnwendungsentwicklungTeil1\Kontrollstrukturen\Übungesaufgaben\Program.cs:line 30

            pArray.Insert(0, (T)Convert.ChangeType(0, typeof(List<T>)));
            long Nenner = 1;
            for (int i = 1; i < pArray.Count; i++)
            {
                Nenner = Nenner * i;
                pArray[i] = (T)Convert.ChangeType(Nenner, typeof(List<T>));
            }

            return (T)Convert.ChangeType(pArray, typeof(List<T>));
        }

        #endregion
    }
}
