using System;
using System.Collections.Generic;
using System.Text;

namespace KaffeeAutomat
{
    class StartCoffeeMashine
    {
        static void Main()
        {
            CoffeeMashine coffeeMashine = new CoffeeMashine();

            while(coffeeMashine.run)
            {
                coffeeMashine.RunTheProgramm();
            }

            Console.WriteLine("Press any Key to close...");
            Console.ReadKey();
        }
    }
}
