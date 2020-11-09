using System;
using System.Globalization;

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
        private readonly string _userInput = "Herzlich Willkommen!\n***********************\nLass mich was Rechnen!";


        public Calculator()
        {
            foreach (var t in _userInput)
            {
                Program.VordergrundFarbauswahl();
                Console.Write(t);
            }

            Console.WriteLine();
        }


        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a + b</returns>
        private int Addiere(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Substraktion
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a - b</returns>
        private int Minimiere(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplikation
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a * b</returns>
        private int Multipliziere(int a, int b)
        {
            return a * b;
        }


        /// <summary>
        /// Division
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a / b</returns>
        private int Dividiere(int a, int b)
        {
            return (int)Math.Round((double)a / b, 2);
        }

        /// <summary>
        /// Modulo
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a % b</returns>
        private int Moduliere(int a, int b)
        {
            return a % b;
        }


        /// <summary>
        /// Berechnet Ergebnis aus den gesammelten Usereingaben
        /// </summary>
        /// <returns></returns>
        public string Calculate(int numberA, int numberB, Operations op)
        {
            var result = op switch
            {
                Operations.Addition => Addiere(numberA, numberB),
                Operations.Substraktion => Minimiere(numberA, numberB),
                Operations.Multiplikation => Multipliziere(numberA, numberB),
                Operations.Division => Dividiere(numberA, numberB),
                Operations.Modulo => Moduliere(numberA, numberB),
                _ => throw new NotImplementedException("Error 106")
            };

            Operator = 0;

            return result.ToString(CultureInfo.InvariantCulture);
        }
    }
}
