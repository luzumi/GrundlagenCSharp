//Solution GrundlagenCSharp
// .NetCore Application 'Kontrollstrukturen' erstellt
//Haken bei Solution im Projektverzeichnis rausgenommen, alle weiteren Projekte werden dann Teil der Solution

using System;
// normaler kommetar
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
        /// <summary>
        /// Diese Funktion startet das Programm
        /// </summary>
        /// <param name="args">Kommandozeilenparameter</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
