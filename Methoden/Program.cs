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
        ///     <example>For example:
        ///         <code> <c>pTextVorDerZahl</c> - beschreibt die Ausgabe.
        ///         </code>
        ///         <code> <c>pZahl</c> - Ganzahl zur Ausgabe auf der Konsole
        ///         </code>
        ///         <code> int EineZahl = 5;
        ///         </code>
        ///         <code> AusgabeZahl("Testausgabe: ", EineZahl);
        ///         </code>
        ///         results in <c>Testausgabe: 5</c>
        ///     </example>
        /// </summary>
        /// <param name="pTextVorDerZahl">  beschreibt die Ausgabe.  </param>
        /// <param name="pZahl">   Ganzahl zur Ausgabe auf der Konsole  </param>
        static void AusgabeZahl(string pTextVorDerZahl, int pZahl)
        {
            Console.WriteLine(pTextVorDerZahl + pZahl);
        }
        
        /// <summary>
        /// Methode benötigt eine Ganzahl und erhöht diese um 2
        ///     <example>For Example:
        ///         <code> <c>int EineZahl = 5;</c>
        ///         </code>
        ///         <code> <c>Addiere2(EineZahl)</c>
        ///         </code>
        ///         results in <c>7</c>
        ///     </example>
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
        /// <example>
        ///     <code> <c>ref</c> verweist auf die Speicheradresse 
        ///     </code>
        ///     <code> Mit ref verändert sich das Original!
        ///     </code>
        /// </example>
        /// </summary>
        /// <param name="pZahl">Ganzzahl der um 2 erhöht wird und den errechneten Wert behält</param>
        static void Addiere2UndVeraendereAusgangswert(ref int pZahl)        
        {
            pZahl += 2;
        }


        /// <summary>
        /// Methode simmuliert ein i++
        ///     <example>
        ///         <code> <c>int EineZahl = 5 </c>
        ///         </code>
        ///         <code> <c>PostInkrement(EineZahl)</c>
        ///         </code>
        ///         results in <c>5</c>
        ///         <code> Das Original <c>EineZahl</c> bleibt gleich
        ///         </code>
        ///     </example>
        /// </summary>
        /// <param name="pzahl">Verweis auf Speicheradresse einer Ganzzahl</param>
        static int PostInkrement(ref int pzahl)
        {
            int sicherung = pzahl;
            pzahl += 1;
            return sicherung;
        }


        ///<summary>
        /// Methode simmuliert ein ++i
        ///     <example>
        ///         <code> int EineZahl = 5
        ///         </code>
        ///         <code> <c>PostInkrement(EineZahl)</c>
        ///         </code>
        ///         <code>results in <c>6</c></code>
        ///         <c> Das Original <c>EineZahl</c> wird geändert
        ///         </c>
        ///     </example>
        /// </summary>
        /// <returns>veränderten Ausgangswert</returns>
        /// <param name="pZahl">Verweis auf Speicheradresse einer Ganzzahl <c>ref</c></param>
        static int PreInkrement(ref int pZahl)
        {
            return pZahl += 1;
        }
    }
}
