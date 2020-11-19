using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Objekte
{
    /// <summary>
    /// Basisklasse für alle Autos
    /// <c>erbt von Klasse "Object"</c>
    /// </summary>
    public class Auto
    {
        #region Einleitung Klassen

        public int Reifenanzahl; //public - Alle Klassen können auf das Element zugreifen
        protected int Sitzanzahl; //protected - Nur die Klasse und die erbenden Klassen können auf das Element zugreifen
        private int _ventile; //private - Nur die Klasse selber kann auf das Elemente zugreifen

        private int
            _zylinder; //private Elemente müssen über public Propertys angesprochen werden um sie lesen oder schreiben zu können

        public int
            Zylinder
        {
            get { return Zylinder; }
            set { _zylinder = value; }
        } //über die Property kann auf das Element zugegriffen werden. Das ermöglicht den Zugriff zu manipulieren. Der Code in get oder set kann verändert/ergänzt werden


        public void Zugriffstest()
        {
            Reifenanzahl = 4;
            Sitzanzahl = 2;
            _ventile = 5;
        }

        #endregion

        #region Übung dazu

        private byte _tueren; // enthält den zustand von 8 türen
        protected byte AktivierbareTueren;

        public Auto()
        {
            AktivierbareTueren = 255;
        }

        public Auto(byte erlaubteTueren)
        {
            AktivierbareTueren = erlaubteTueren;
        }

        public void TuerenBenutzen(byte tuerNummer)
        {
            if ((AktivierbareTueren & 1 << tuerNummer) == 0) //early exit wenn nicht erlaubte tür benutzt wird
            {
                Console.WriteLine("Tür ist verboten");
                return;
            }


            byte lesekopf = 0b_0000_0001;
            lesekopf = (byte)(lesekopf << tuerNummer); // schiebt das lesebit bei jedem durchlauf eins weiter nach links

            // 0000 1011   // Dez 11 zustand der türen
            // 0000 0010   // Dez  2 lesekopf wenn nach tür 1 gefragt wird
            // 0000 0010   // Dez  2 ergebnis der UND verknüpfung

            if ((_tueren & lesekopf) > 0)
            {
                // das gesetzte bit muss auf 0 zurück
                _tueren = (byte)(_tueren ^ lesekopf);
                Console.WriteLine("Tür " + tuerNummer + " ist jetzt zu");
            }
            else
            {
                // das leere bit muss gesetzt werden
                _tueren += lesekopf;
                Console.WriteLine("Tür " + tuerNummer + " ist jetzt auf");
            }
        }

        public void TuerenBenutzen(EnumAutotuer tuer)
        {
            if ((AktivierbareTueren & (byte)tuer) == 0) //early exit wenn nicht erlaubte tür benutzt wird
            {
                Console.WriteLine("Tür ist verboten");
                return;
            }

            if ((_tueren & (byte)tuer) > 0)
            {
                // das gesetzte bit muss auf 0 zurück
                _tueren = (byte)(_tueren ^ (byte)tuer);
                Console.WriteLine("Tür " + tuer + " ist jetzt zu");
            }
            else
            {
                // das leere bit muss gesetzt werden
                _tueren += (byte)tuer;
                Console.WriteLine("Tür " + tuer + " ist jetzt auf");
            }
        }

        public void TuerenAusgeben()
        {
            byte lesekopf = 0b_0000_0001;
            for (int i = 0; i < 8; i++) //Hack: geht fest von 8 möglichen türen aus
            {
                Console.Write(((_tueren & lesekopf) > 0 ? "  true" : " false"));
                lesekopf = (byte)(lesekopf << 1);
            }

            Console.WriteLine();
        }
    }

    #endregion
}
