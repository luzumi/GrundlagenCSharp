using System;



namespace Übungesaufgaben
{
    class Program
    {
        static void Main()
        {
            Timer fps = new Timer();
            do
            {
                Primzahlen.PrimeZahl(300);     //Primzahlenberechnung bis zu einem definierten Maximalwert

                fps.FpsChecker();
                
            } while (!Console.KeyAvailable);
                

            



            Console.ReadLine();

        }

        


        
    }
}
