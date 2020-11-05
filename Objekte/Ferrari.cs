using System;
using System.Collections.Generic;
using System.Text;

namespace Objekte
{
    /// <summary>
    /// erbt die Eigenschaften der Basisklasse "Auto"
    /// private Elemente aus "Auto" sind hier nicht verfügbar
    /// </summary>
    class Ferrari : Auto
    {
        public Ferrari(byte erlaubteTueren) : base (erlaubteTueren) { }

        public Ferrari()
        {
            _aktivierbareTueren = (byte)(EnumAutotuer.MotorHaube | EnumAutotuer.SchiebeDach | EnumAutotuer.TankDeckel | EnumAutotuer.VornLinks | EnumAutotuer.VornRechts);
        }
    }

    class Delorean : Auto
    {
        public Delorean()
        {
            _aktivierbareTueren = 255;
        }
    }

}
