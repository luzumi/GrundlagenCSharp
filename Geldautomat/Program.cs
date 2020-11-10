using System;
using System.Drawing;

namespace Geldautomat
{
    class Program
    {
        static void Main(string[] args)
        {
            Geldautomat geldautomat = new Geldautomat(1234500);
            byte activeButtonID = 0;
            
            Console.WriteLine("Welcome !");
            
            geldautomat.GetBalance();

            do
            {
                QueryWithdrawMoney(geldautomat);

                Console.WriteLine("Möchten Sie das Programm beenden? [y/n] ");

            } while (Console.ReadKey(true).Key == ConsoleKey.N);

            geldautomat.Message("Auf Wiedersehen!", true);
        }


        #region Methods

        private static void QueryWithdrawMoney(Geldautomat geldautomat)
        {
            Console.WriteLine("Möchten SIe Geld abheben? (Y/N)");

            switch (Console.ReadKey(true).KeyChar.ToString().ToLower())
            {
                case "y":
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine("\nGeben SIe einen Betrag (in Cent) ein");

                    GetAmount(geldautomat);
                    break;
            }
        }


        private static void GetAmount(Geldautomat geldautomat)
        {
            int convertedNumber;
            
            if (int.TryParse(Console.ReadLine(), out convertedNumber))
            {
                geldautomat.Withdraw(convertedNumber);

                geldautomat.GetBalance();
            }
            else
            {
                
                Console.WriteLine("Das war keine gültige zahl");
                return;
            }
        }

        static void erstelleButtons(Buttons[] Rows)
        {
            for (int i = 0; i < Rows.Length; i++)
            {

                Rows[i] = new Buttons();

            }

            Rows[0].Text   = "************************************";
            Rows[1].Text   = "*                                  *";
            Rows[2].Text   = "*                                  *";
            Rows[3].Text   = "*                                  *";
            Rows[6].Text   = "*                                  *";
            Rows[4].Text   = "*                                  *";
            Rows[5].Text   = "*                                  *";
            Rows[7].Text   = "*                                  *";
            Rows[8].Text   = "*                                  *";
            Rows[9].Text   = "*                                  *";
            Rows[10].Text  = "*                                  *";
            Rows[11].Text  = "************************************";
        }

        #endregion
    }
}
