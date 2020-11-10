using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Geldautomat
{
    /// <summary>
    /// Geldautomat wird mit Startbetrag in Cent erstellt
    /// </summary>
    class Geldautomat
    {
        public int Balance { get; private set; }

        public Geldautomat(int balance)
        {
            Balance = balance;
        }


        public void Withdraw(int pAmount)
        {
            InProgress();

            if ((Balance - pAmount) > 0) //TODO doppelte Berechnung umgehen?
            {
                Balance -= pAmount;
                Message("Auszahlung erfolgreich! ", true);
            }
            else
            {
                Message("Auszahlung nicht möglich! ", false);
                GetBalance();
            }
        }


        /// <summary>
        /// gibt eingegebenen Text aus
        /// Gürn - positv
        /// Rot - Fehlermeldungen
        /// </summary>
        /// <param name="message">Ausgabetext</param>
        /// <param name="status">Message ist Success/Error</param>
        public void Message(string message, bool status)
        {
            InProgress();

            if (status)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(message);

            Console.ResetColor();
        }


        public void GetBalance()
        {
            InProgress();

            if (Balance % 100 == 0) Console.WriteLine("Ihr aktueller Kontostand beträgt : " + (Balance / 100).ToString("##.###") + "," + "00");
            else if(Balance%10 == 0) Console.WriteLine((Balance / 100).ToString("##.###") + "," + (Balance % 100/10) + "0");
            else
            {
                Console.WriteLine("Ihr aktueller Kontostand beträgt : " + (Balance/100) + "," + (Balance%100) + " €");
            }
        }


        private void InProgress()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(50);
                Console.Write("*");
            }

            Console.WriteLine();
        }
    }
}
