using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontrollstrukturen
{
    public static class Primzahlen
    {
        /// <summary>
        /// statische Methode
        /// Berechne Primzahlen im Bereich bist Maximum
        /// </summary>
        /// <param name="maximum">Bis zu diesem Wert werden alle Primezahlen berechnet</param>
        public static void PrimeZahl( int maximum)
        {
            List<int> primzahlenListe = new List<int>();
            
            ListeFuellen(primzahlenListe, maximum);

            PrimezahlenFinden(primzahlenListe);

            ListeAusgeben(primzahlenListe);

            Console.ReadLine();
        }


        /// <summary>
        /// Neue Liste mit Zahlen von 1 bis "maximalZahl" wird erstellt
        /// </summary>
        /// <param name="primzahlenListe">Leere Liste zum Befüllen</param>
        /// <param name="maximalZahl">Maximalwert der zu erstellenden Liste</param>
        private static void ListeFuellen(List<int> primzahlenListe, int maximalZahl)
        {
            for (int i = 1; i <= maximalZahl; i++)
            {
                primzahlenListe.Add(i);
            }
        }

        #region Berechnung

        /// <summary>
        /// Berechnet Primzahlen. Jede Zahl kleiner oder gleich 2 in der Ausgangsliste wird auf Modulo"vorhergehende Zahlen" == 0 überprüft.
        /// Bei jedem Treffer wird anzahl um 1 erhöht
        /// Wenn am Ende anzahl <= 2 ist, werden alle Produkte aus der Liste entfernt
        /// </summary>
        /// <param name="primzahlenListe">Liste der zu überprüfenden Zahlen, Wird nach durchlaufen der Methode nur noch Primzahlen enthalten</param>
        private static void PrimezahlenFinden(List<int> primzahlenListe)
        {
            for (int i = 2; i <= primzahlenListe.Max() + 1; i++)
            {
                // int anzahl = 0;
                // foreach (var wert in primzahlenListe)                 //Überprüfung auf Anzahl der Teiler ist nicht notwendig
                // {
                //     if (i % wert == 0)
                //     {
                //         anzahl++;
                //     }
                // }
                //
                // if (anzahl <= 2)
                // {
                    LoescheProduktAusListe(primzahlenListe, i);
                // }
            }
        }

        /// <summary>
        /// Löschte alle Produkte der erkannten Primzahl aus der Ausgangsliste,
        /// solange bis zahl + x*primzahl den Maximalwert der Liste erreicht hat
        /// </summary>
        /// <param name="primzahlenListe">Liste der zu überprüfenden Zahlen, alle Produkte der Primzahl werden daraus gelöscht</param>
        /// <param name="primzahl">Die berechnete Primzahl</param>
        private static void LoescheProduktAusListe(List<int> primzahlenListe, int primzahl)
        {
            int zahl = primzahl;

            while (zahl <= primzahlenListe.Max())
            {
                zahl += primzahl;
                primzahlenListe.Remove(zahl);
            }
        }
        
        #endregion
        
        /// <summary>
        /// Ausgabe der berechneten Primzahlen
        /// Ausgabe wird nach jeder 10tenZahl umgebrochen
        /// </summary>
        /// <param name="primzahlenListe">Liste der Zahlen zur Überprüfung auf Primzahlen</param>
        private static void ListeAusgeben(List<int> primzahlenListe)
        {
            int zaehler = 0;
            foreach (var wert in primzahlenListe)
            {
                zaehler++;
                if (zaehler % 10 == 0)
                {
                    Console.WriteLine();
                }

                Console.Write(wert + ", ");
            }
        }
    }
}