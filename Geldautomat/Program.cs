﻿using System;
using System.Diagnostics;

namespace Geldautomat
{
    static class Program
    {
        static void Main()
        {
            ATM BankAutomat = new ATM();

            Console.Write("Hallo User, ich bin ein Geldautomat!\n\nBitte geben sie ihren Namen ein : ");
            // zuerst wird Console.ReadLine() ausgeführt und ein text von der Konsole gelesen
            // der rückgabewert der methode ist ein string. auf diesem string führen wir direkt
            // die methode ToLower() aus. Der rückgabewert davon in ein kleingeschriebener text
            // dieser zurückgegebene text wird in der variable userName abgelegt.
            string userName = Console.ReadLine()?.ToLower();
            Console.Write("Bitte geben sie ihren PIN ein: ");
            string userPinS = Console.ReadLine();

            // versucht aus dem string userPinS einen ushort zu machen, falls es !nicht! klappt
            // wird es als true gewertet für das if. Das Ausrufezeichen ist dabei für die invertierung
            // zuständig. Der zweite test prüft ob der original-string alles andere als exakt 4 zeichen
            // enthält. Dadurch haben wir alle möglichen fehler getestet und gehen bei einem fehler
            // in das if rein.
            Debug.Assert(userPinS != null, nameof(userPinS) + " != null");
            if (! ushort.TryParse(userPinS, out var userPin) || userPinS.Length != 4)
            {
                Console.WriteLine("Die Eingabe entspricht nicht dem PIN format, bye!");
                return; // beendet die Main vorzeitig
            }

            // der letzte parameter wird im inlined stil befüllt. Es wird die Variable userBalance
            // erstellt und diese direkt als parameter übergeben.
            switch (BankAutomat.GetBalance(userName, userPin, out int userBalance))
            {
                case ATMError.NoError:
                    Console.WriteLine("Ihr Kontostand beträgt " + userBalance);
                    break; // beendet den switch
                case ATMError.PinError:
                case ATMError.UserError:
                    Console.WriteLine("Kommst hier nicht rein, du hacker!!");
                    return; // beendet die ganze Main
                default:
                    Console.WriteLine("Der Gerät klappt irgendwie nischt...tschuligom");
                    return; // beendet die ganze Main
            }

            Console.WriteLine("Möchten Sie Geld abheben? [y/n]");

            ConsoleKey nutzerTaste = Console.ReadKey(true).Key;// liesst die gedrückte taste auf der tastatur aus
            if (nutzerTaste == ConsoleKey.Y ) // ist die gedrückte taste die Y taste gewesen?
            {
                Console.WriteLine("Wieviel möchten sie abheben?");
                if (!int.TryParse(Console.ReadLine(), out var convertedNumber)) // versuchen die nutzereingabe in eine Zahl zu verwandeln. Wenn es nicht klappt wird das if ausgeführt (wegen dem ! )
                {
                    // wenn tryparse sagt das die konvertierung fehlschlägt
                    Console.WriteLine("Das war keine gültige Zahl. Ende!");
                    return; // beendet die Main
                }

                switch (BankAutomat.Withdraw(userName, userPin, convertedNumber))
                {
                    case ATMError.NoError:
                        Console.WriteLine("Das Geld wurde abgebucht");
                        BankAutomat.GetBalance(userName, userPin, out int newBalance);
                        Console.WriteLine("Neuer Kontostand : " + newBalance);
                        break;
                    case ATMError.PinError:
                    case ATMError.UserError:
                        Console.WriteLine("Kommst hier nicht rein, du hacker!!");
                        return;
                    case ATMError.BalanceError:
                        Console.WriteLine("Sie haben nicht genug Geld");
                        break;
                    default:
                        Console.WriteLine("Der Gerät klappt irgendwie nischt...tschuligom");
                        return; // beendet die ganze Main
                }
            }
            Console.WriteLine("Bye");
        }
    }
}
