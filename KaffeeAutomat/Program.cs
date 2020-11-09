using System;
using System.Collections.Generic;
using System.Threading;

namespace KaffeeAutomat
{
    struct UserInterface
    {
        public string Text;
        public Recipe MachineValue;
    }

    class Program
    {
        static void Main()
        {
            //Begrüßung
            Console.CursorVisible = false;
            Console.WriteLine("Hallo User, dies ist ein Kaffeeautomat");
            Thread.Sleep(2000);
            Console.WriteLine("Kaffeemaschine wird gestartet");
            Thread.Sleep(4000);
            CoffeeMashine coffeeMashine = new CoffeeMashine();

            bool shutdownMashine = false;
            byte lesekopf = 0;
            ConsoleKey pressedKey;

            List<string> buttons = new List<string>();

            buttons.Add(" Kaffee");
            buttons.Add(" Heißes Wasser");
            buttons.Add(" Capucchino");
            buttons.Add(" Milchkaffee");
            buttons.Add(" Heiße Milch");
            buttons.Add(" Wartung");
            buttons.Add(" Kaffeeautomat abschalten");


            Console.Clear();

            do
            {
                Console.WriteLine(
                    "Wählen Sie ihr Getränk mit den Pfeiltasten - bestätigen Sie ihre Auswahl mit der <Enter>-Taste.\n");

                MarkRow(buttons, lesekopf);
                
                pressedKey = Console.ReadKey(true).Key;

                lesekopf = SelectProgram(pressedKey, lesekopf, buttons, coffeeMashine, ref shutdownMashine);


            } while (!shutdownMashine);

            Console.WriteLine("Kaffeeautomat wird beendet");
        }


        /// <summary>
        /// Startet Programm anhand der Usereingabe
        /// </summary>
        /// <param name="pressedKey">UserInput</param>
        /// <param name="lesekopf">Aktive Row</param>
        /// <param name="buttons">Ausgabetexte des Getränks</param>
        /// <param name="coffeeMashine">Aktive Kaffemaschine</param>
        /// <param name="shutdownMashine">Programmende</param>
        /// <returns>Programm</returns>
        private static byte SelectProgram(ConsoleKey pressedKey, byte lesekopf, List<string> buttons, CoffeeMashine coffeeMashine,
            ref bool shutdownMashine)
        {
            switch (pressedKey)
            {
                case ConsoleKey.Enter:
                    if (lesekopf == buttons.Count - 1)
                    {
                        shutdownMashine = true;
                    }
                    else if (lesekopf == buttons.Count - 2)
                    {
                        Console.Clear();
                        coffeeMashine.Maintenance();
                        Console.Write("Wartung wird durchgeführt...");
                        Thread.Sleep(3000);
                        Console.WriteLine(" Container sind wieder in Ordnung.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        if (coffeeMashine.Dispense((Recipe) lesekopf))
                        {
                            Console.WriteLine("Ihr" + buttons[lesekopf] + " wird jetzt zubereitet.");
                            Console.WriteLine("Ihr" + buttons[lesekopf] + " ist jetzt fertig");
                        }
                        else
                        {
                            Console.WriteLine("Container überprüfen. Bitte Wartung durchführen.");
                        }

                        Thread.Sleep(3000);
                        Console.Clear();
                    }

                    break;

                case ConsoleKey.UpArrow:
                    if (lesekopf > 0)
                    {
                        lesekopf--;
                    }

                    Console.SetCursorPosition(0, 0);
                    break;
                case ConsoleKey.DownArrow:
                    if (lesekopf < buttons.Count - 1)
                    {
                        lesekopf++;
                    }

                    Console.SetCursorPosition(0, 0);
                    break;
            }

            return lesekopf;
        }


        /// <summary>
        /// Markiert die aktive Zeile grün
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="lesekopf"></param>
        private static void MarkRow(List<string> buttons, byte lesekopf)
        {
            for (int counter = 0; counter < buttons.Count; counter++)
            {
                if (counter == lesekopf)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(buttons[counter]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(buttons[counter]);
                }
            }
        }
    }
}
