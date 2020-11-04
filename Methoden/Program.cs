using System;

namespace Methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            int MeineZahl = 12;

            AusgabeZahl("Ausgangszahl: ", MeineZahl);

            int andereZahl = Addiere2(Addiere2(MeineZahl));
            AusgabeZahl("AndereZahl nach addiere2(addiere2)): = " , andereZahl);

            AusgabeZahl("MeineZahl aktuell: " , MeineZahl);
            Console.WriteLine();

            Addiere2UndVeraendereAusgangswert(ref MeineZahl);
            AusgabeZahl("Addiere2UndVeraendereAusgangswert: = " , MeineZahl);
            Console.WriteLine();

            AusgabeZahl("MeineZahl aktuell: " , MeineZahl);
            AusgabeZahl("Postinkrement (i++): = ", + PostInkrement(ref MeineZahl));
            AusgabeZahl("MeineZahl aktuell: " , MeineZahl);
            Console.WriteLine();

            AusgabeZahl("MeineZahl aktuell: " , MeineZahl);
            AusgabeZahl("PreInkrement (++i): = ", PreInkrement(ref MeineZahl));
            AusgabeZahl("MeineZahl aktuell: " , MeineZahl);
            
            
            
            Console.ReadLine();
        }
        
        /// <summary>
        /// Benötigt eine Ganzzahl und gibt diese auf der Konsole aus.  
        /// 
        /// <c>pTextVorDerZahl</c> - beschreibt die Ausgabe.  
        /// <c>pZahl</c> - Ganzahl zur Ausgabe auf der Konsole
        /// </summary>
        /// <param name="pTextVorDerZahl">  beschreibt die Ausgabe.  </param>
        /// <param name="pZahl">   Ganzahl zur Ausgabe auf der Konsole  </param>
        static void AusgabeZahl(string pTextVorDerZahl, int pZahl)
        {
            Console.WriteLine(pTextVorDerZahl + pZahl);
        }
        
        /// <summary>
        /// Methode benötigt eine Ganzahl und erhöht diese um 2
        /// </summary>
        /// <param name="pZahl">Zahl die um 2 erhöht werden soll</param>
        /// <returns>Ausgangswert + 2</returns>
        static int Addiere2(int pZahl)          //Methode benötigt eine Ganzahl im Aufruf und legt sie in den Parameter pZahl ab
        {
            pZahl += 2; // pZahl = pZahl + 2;   //erhöht pZahl um 2, Der Wert der eingebenen Variable wird dabei nicht verändert
            return pZahl;                       //Gibt das Ergbnis an den Aufrufer zurüch, pZahl verfällt im Anschluss
                                                //und kann ausserhalb dieser Methode nicht aufgerufen werden
        }


        /// <summary>
        /// Methode erhöht Eingabewert um 2 und verändert den Ausgangswert der Variable
        /// ref verweist auf die Speicheradresse der Variable und verändert den Wert dieser Speicheradresse.
        /// Mit ref verändert sich das Original!
        /// </summary>
        /// <param name="pZahl">Ganzzahl der um 2 erhöht wird und den errechneten Wert behält</param>
        static void Addiere2UndVeraendereAusgangswert(ref int pZahl)        
        {
            pZahl += 2;
        }


        /// <summary>
        /// Methode simmuliert ein i++
        /// </summary>
        /// <param name="pzahl">Verweis auf Speicheradresse einer Ganzzahl ref</param>
        /// <returns>Ausgangswert</returns>
        static int PostInkrement(ref int pzahl)
        {
            int sicherung = pzahl;
            pzahl += 1;
            return sicherung;
        }


        /// <summary>
        /// Methode simuliert ein ++i
        /// </summary>
        /// <param name="pZahl">Verweis auf Speicheradresse einer Ganzzahl  ref</param>
        /// <returns>veränderten Ausgangswert</returns>
        static int PreInkrement(ref int pZahl)
        {
            return pZahl += 1;
        }
    }
}
