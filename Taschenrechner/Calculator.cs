using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Taschenrechner
{
    /// <summary>
    /// Stellt die Grundrechenarten zur Verfügung und berechnet das Ergebnis der Eingaben
    /// </summary>
    class Calculator
    {
        public string UserInput;
        public int NumberA { get; set; }
        public int NumberB { get; set; }
        public Operations Operator { get; set; }

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
        /// Minus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a - b</returns>
        public int Minimiere(int a, int b)
        {
            return a - b;
        }
        
        /// <summary>
        /// Mal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a * b</returns>
        public int Multipliziere(int a, int b)
        {
            return a * b;
        }
        

        /// <summary>
        /// Geteilt
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
        public int Modoluiere(int a, int b)
        {
            return a % b;
        }


        /// <summary>
        /// Berechnet Ergebnis aus den gesammelten Usereingaben
        /// </summary>
        /// <returns></returns>
        public int Calculate()
        {
            BenutzerDatenSammeln();

            switch (Operator)
            {
                case Operations.Plus:
                    return Addiere(NumberA, NumberB);
                    break;
                case Operations.Minus:
                    return Minimiere(NumberA, NumberB);
                    break;
                case Operations.Mal:
                    return Multipliziere(NumberA, NumberB);
                    break;
                case Operations.Geteilt:
                    return Dividiere(NumberA, NumberB);
                    break;
                case Operations.Modulo:
                    return Modoluiere(NumberA, NumberB);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Nimmt Usereingabe für die beschriebene Zahl an
        /// </summary>
        /// <param name="zahl"></param>
        /// <returns>Ganzzahl</returns>
        private int UserEingabeZahl(string zahl)
        {
            Console.WriteLine($"Gib bitte { zahl } ein");

            string Eingabe = Console.ReadLine();

            int ConvertedNumber = int.Parse(Eingabe);

            //Abfangen falscher Eingaben
            if (int.TryParse(Eingabe, out ConvertedNumber))
            {
                return ConvertedNumber;
            }
            else
            {
                Console.WriteLine("Eingabe war Falsch, bitte nur ganze Zahlen verwenden\n");
                
                UserEingabeZahl(zahl);
            }

            return ConvertedNumber;
        }


        /// <summary>
        /// Bestimmt die Rechenmethode anhand des gelieferten Rechenzeichens vom User
        /// </summary>
        /// <returns>Die Rechnart aus enum</returns>
        public Operations DerOperator()
        {
            Console.WriteLine("Gib die Rechenart an. Verwende dafür die normalen Rechenzeichen");

            string Eingabe = Console.ReadLine();

            switch (Eingabe)
            {
                case "+":
                    return Operations.Plus;
                case "-":
                    return Operations.Minus;
                case "*":
                    return Operations.Mal;
                case "/":
                    return Operations.Geteilt;
                case "%":
                    return Operations.Modulo;
                default:
                    DerOperator();
                    break;
            }

            return Operator;
        }

        /// <summary>
        /// Erbittet Eingaben vom User für die einzelnen teilen der Gleichung
        /// </summary>
        private void BenutzerDatenSammeln()
        {
            NumberA = UserEingabeZahl("Ihre ersteZahl bitte");
            Operator = DerOperator();
            NumberB = UserEingabeZahl("Ihre zweite Zahl bitte");
        }
    }
}
