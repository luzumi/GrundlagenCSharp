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
    class Auto
    {
        #region Einleitung Klassen

        public int Reifenanzahl; //public - Alle Klassen können auf das Element zugreifen
        protected int Sitzanzahl; //protected - Nur die Klasse und die erbenden Klassen können auf das Element zugreifen
        private int Ventile; //private - Nur die Klasse selber kann auf das Elemente zugreifen

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
            Ventile = 5;
        }

        #endregion

        #region Übung dazu

        protected byte _tueren; // enthält den zustand von 8 türen
        protected byte _aktivierbareTueren;

        public Auto()
        {
            _aktivierbareTueren = 255;
        }
        public Auto(byte ErlaubteTueren)
        {
            _aktivierbareTueren = ErlaubteTueren;
        }

        public void TuerenBenutzen(byte TuerNummer)
        {
            if ((_aktivierbareTueren & 1 << TuerNummer) == 0) //early exit wenn nicht erlaubte tür benutzt wird
            {
                Console.WriteLine("Tür ist verboten");
                return;
            }


            byte Lesekopf = 0b_0000_0001;
            Lesekopf = (byte)(Lesekopf << TuerNummer); // schiebt das lesebit bei jedem durchlauf eins weiter nach links

            // 0000 1011   // Dez 11 zustand der türen
            // 0000 0010   // Dez  2 lesekopf wenn nach tür 1 gefragt wird
            // 0000 0010   // Dez  2 ergebnis der UND verknüpfung

            if ((_tueren & Lesekopf) > 0)
            {
                // das gesetzte bit muss auf 0 zurück
                _tueren = (byte)(_tueren ^ Lesekopf);
                Console.WriteLine("Tür " + TuerNummer + " ist jetzt zu");
            }
            else
            {
                // das leere bit muss gesetzt werden
                _tueren += Lesekopf;
                Console.WriteLine("Tür " + TuerNummer + " ist jetzt auf");
            }
        }

        public void TuerenBenutzen(EnumAutotuer Tuer)
        {
            if ((_aktivierbareTueren & (byte)Tuer) == 0) //early exit wenn nicht erlaubte tür benutzt wird
            {
                Console.WriteLine("Tür ist verboten");
                return;
            }

            if ((_tueren & (byte)Tuer) > 0)
            {
                // das gesetzte bit muss auf 0 zurück
                _tueren = (byte)(_tueren ^ (byte)Tuer);
                Console.WriteLine("Tür " + Tuer + " ist jetzt zu");
            }
            else
            {
                // das leere bit muss gesetzt werden
                _tueren += (byte)Tuer;
                Console.WriteLine("Tür " + Tuer + " ist jetzt auf");
            }




        }

        public void TuerenAusgeben()
        {
            byte Lesekopf = 0b_0000_0001;
            for (int i = 0; i < 8; i++) //Hack: geht fest von 8 möglichen türen aus
            {
                Console.Write(((_tueren & Lesekopf) > 0 ? "  true" : " false"));
                Lesekopf = (byte)(Lesekopf << 1);
            }
            Console.WriteLine();
        }
    }

        #endregion
}

