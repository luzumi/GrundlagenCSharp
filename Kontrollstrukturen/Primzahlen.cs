using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontrollstrukturen
{
    public static class Primzahlen
    {
        /// <summary>
        /// Berechne Primzahlen im Bereich bist Maximum
        /// </summary>
        /// <param name="maximum">Bis zu diesem Wert werden alle Primezahlen berechnet</param>
        public static void PrimeZahl( int maximum)
        {
            List<int> primesList = new List<int>();
            int max = maximum;

            ListeFüllen(primesList, max);

            PrimezahlenFinden(primesList);

            ListeAusgeben(primesList);

            Console.ReadLine();
        }

        /// <summary>
        /// Ausgabe der berechneten Primzahlen
        /// Ausgabe wird nach jeder 10tenZahl umgebrochen
        /// </summary>
        /// <param name="primesList">Liste der Zahlen zur Überprüfung auf Primzahlen</param>
        private static void ListeAusgeben(List<int> primesList)
        {
            int counter = 0;
            foreach (var wert in primesList)
            {
                counter++;
                if (counter % 10 == 0)
                {
                    Console.WriteLine();
                }

                Console.Write(wert + ", ");
            }
        }

        /// <summary>
        /// Berechnet Primzahlen. Jede Zahl kleiner oder geleich 2 in der Ausgangsliste wird auf Modulo"vorhergehende Zahlen" == 0 überprüft.
        /// Bei jedem Treffer wird anzahl um 1 erhöht
        /// Wenn am Ende anzahl <= 2 ist, werden alle Produkte aus der Liste entfernt
        /// </summary>
        /// <param name="primesList">Liste der zu überprüfenden Zahlen, Wird nach durchlaufen der Methode nur noch Primzahlen enthalten</param>
        private static void PrimezahlenFinden(List<int> primesList)
        {
            for (int i = 2; i <= primesList.Max() + 1; i++)
            {
                int anzahl = 0;
                foreach (var wert in primesList)
                {
                    if (i % wert == 0)
                    {
                        anzahl++;
                    }
                }

                if (anzahl <= 2)
                {
                    LöscheProduktAusListe(primesList, i);
                }
            }
        }

        /// <summary>
        /// Löschte alle Produkte der erkannten Primzahl aus der Ausgangsliste,
        /// solange bis zahl + x*primezahl den Maximalwert der Liste erreicht hat
        /// </summary>
        /// <param name="primesList">Liste der zu überprüfenden Zahlen, alle Produkte der Primzahl werden daraus gelöscht</param>
        /// <param name="primezahl">Die berechnete Primzahl</param>
        private static void LöscheProduktAusListe(List<int> primesList, int primezahl)
        {
            int zahl = primezahl;

            while (zahl <= primesList.Max())
            {
                zahl += primezahl;
                primesList.Remove(zahl);
            }
        }
        
        /// <summary>
        /// Neue Liste mit Zahlen von 1 bis "maximalZahl" wird erstellt
        /// </summary>
        /// <param name="primesList">Leere Liste zum Befüllen</param>
        /// <param name="maximalZahl">Maximalwert der zu erstellenden Liste</param>
        private static void ListeFüllen(List<int> primesList, int maximalZahl)
        {
            for (int i = 1; i <= maximalZahl; i++)
            {
                primesList.Add(i);
            }
        }
    }
}