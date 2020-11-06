using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Text;

namespace Taschenrechner
{
    /// <summary>
    /// Stellt die Grundrechenarten zur Verfügung und berechnet das Ergebnis der Eingaben
    /// </summary>
    class Calculator
    {
        public int NumberA { get; set; }
        public int NumberB { get; set; }
        public Operations Operator { get; set; }
        public string UserInput;


        public Calculator()
        {
            Console.WriteLine("Herzlich Willkommen!\n***********************\nLass mich was Rechnen!");
        }

        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a + b</returns>
        public int Addiere(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Substraktion
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a - b</returns>
        public int Minimiere(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplikation
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a * b</returns>
        public int Multipliziere(int a, int b)
        {
            return a * b;
        }


        /// <summary>
        /// Division
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a / b</returns>
        public int Dividiere(int a, int b)
        {
            return a / b;
        }

        /// <summary>
        /// Modulo
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a % b</returns>
        public int Moduliere(int a, int b)
        {
            return a % b;
        }


        /// <summary>
        /// Berechnet Ergebnis aus den gesammelten Usereingaben
        /// </summary>
        /// <returns></returns>
        public int Calculate(int NumberA, int NumberB, Operations op)
        {
            var _result = op switch
            {
                Operations.Addition => Addiere(NumberA, NumberB),
                Operations.Substraktion => Minimiere(NumberA, NumberB),
                Operations.Multiplikation => Multipliziere(NumberA, NumberB),
                Operations.Division => Dividiere(NumberA, NumberB),
                Operations.Modulo => Moduliere(NumberA, NumberB),
                _ => throw new NotImplementedException("Error 106")
            };

            Operator = 0;

            return _result;
        }
    }
}
