using System;

namespace Taschenrechner
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator Berechne = new Calculator();

            Console.WriteLine(Berechne.Calculate());
        }
    }
}
