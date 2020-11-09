//Solution GrundlagenCSharp
// .NetCore Application 'Kontrollstrukturen' erstellt
//Haken bei Solution im Projektverzeichnis rausgenommen, alle weiteren Projekte werden dann Teil der Solution

using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

// normaler Kommentar
/*
 * mehrzeiliger kommentar
 * oder Kommentar der mitten in eine Zeile geschrieben werden kann
 */

namespace Kontrollstrukturen
{
    /// <summary>
    /// Kommentar der zu jeder Funktion eine Beschreibung mitleifert, der dann auch direkt angezeigt wird,
    /// wenn die Maus über den Funktionsnamen gehalten wird
    /// </summary>
    class Program
    {
        

        #region Was ist eine Region

        //Eine Region wird genutzt und Codeblöcke über mehrere Methoden und Teilabschnitte zu bündeln
        //und einklappbar zu machen. Die Region ist kein kompilierbarer Code, eher Deko für den Programmierer

        #endregion

        #region Shortcuts und anderes

        //TODO: Dieser Kommentar erstellt einen Eintrag in die Taskliste
        //Hack auch (Zu finden ist die Taskliste unter Menueintrag View)

        //STRG+. zeigt Vorschläge an
        //Autovervollständigen mit TAB, TAB TAB
        //Auskommentieren und Komentar zurücksetzen ALT + Up/Down, STRG + k,c/k,u
        //(um einzelne Shortcuts nachzuschlagen, bzw zu editieren in Titelleiste nach Keymap suchen)

        #endregion

        #region MainFunktion

        /// <summary>
        /// Diese Funktion startet das Programm
        /// Sie MUSS static sein, da sie ohne Instanzieirung gestartet wird
        /// </summary>
        /// <param name="args">Kommandozeilenparameter</param>
        static void Main(string[] args)
        {


            #region Verzweigungen und Schleifen

            int zahl = 3;

            if (zahl == 0)
            {
                //code wenn bedingung erfüllt
            }
            else
            {
                //code wenn nicht erfüllt
            }

            //Kurzform
            //Inline if-Abfrage - zahl = wenn 3 < 2 true dann 5, sonst 3
            zahl = (3 < 2 ? 5 : 3);

            for (int j = 0; j < 10; j++) //standard for
            {
                for (int i = 0; i < 10; i++)
                {
                    i++;
                }
            }

            for (;; zahl++) //Bereiche sind optional. endlosschleife wäre for (;;) { code }

            {
                if (zahl < 10)
                {
                    break;
                }
            }

            List<int> listOfNumbers = new List<int>();
            //___Unterschied List -Array___:
            /*Array enthält den Speicherplatz des ersten Elements. die darauffolgenden Werte sind in festen darauf folgenden Speicherplätzen 
                Sie sind fest in der Größe und können nicht nachträglich vergrößert/verklenert werden*/
            /*List einhält jedes Element den Speicherplatz der vorhergehenden Elelemnts, den Wert und den Speicherplatz des nachfolgenden Elements
                Eine List kann nachträglich in der Größe verändert werden*/

            foreach (var item in listOfNumbers
            ) //foreach geht jedes Element in einem Container (z.B. Liste oder Array) durch
            {
            }


            zielpunkt:  //Zielposition für ein goto

            zahl--;

            if (zahl > 0)
            {
                goto zielpunkt; //springt auf die Position an der "zielpunkt:" erstellt wurde
            }

            zahl = 3;
            switch (zahl)
            {
                case 1:
                    //code wenn in zahl die 1 steht
                    break;  //break; verhindert das Ausführen weiterer cases
                case 2:
                    //code wenn in zahl die 1 steht
                    break;
                default:
                    //code der ausgeführt wird, wenn kein case zutraf
                    break;
            }

            Programmzustaende meinProgramm = Programmzustaende.Starting;

            switch (zahl)
            {
                case 1:
                    meinProgramm = Programmzustaende.Starting;
                    break;
                case 2:
                    meinProgramm = Programmzustaende.Running;
                    break;      //beendet switch block
                case 3:
                    meinProgramm = Programmzustaende.Stopping;
                    break;      //fehlt break: wird code bis zum nächsten break ausgeführt!
                case 4:
                    meinProgramm = Programmzustaende.Stopped;
                    break;
                default:
                    Console.WriteLine("Unbekannter Zustand");
                    break;
            }

            //Kurzform des Switchblocks
            meinProgramm = zahl switch
            {
                1 => Programmzustaende.Running,
                2 => Programmzustaende.Stopped,
                3 => Programmzustaende.Stopping,
                _ => Programmzustaende.Starting      // _  = Default
            };

            #endregion


        }

        #endregion

        #region Methoden

        #endregion
    }
}
