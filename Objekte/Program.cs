﻿using System;
using System.Collections.Concurrent;

namespace Objekte
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Übungsaufgabe

            // Ein Auto hat standardmässig 4 Türen
            // Ein Ferrari erbt von Auto und hat aber nur 2 Türen
            // Ein Delorian hat 3 Türen 
            // Erstelle eine Methode die den Status der Tür prüft,
            // Den status verändert und den status jeder tür ausgibt

            #endregion

            Auto meinAuto = new Auto();

            Ferrari f40 = new Ferrari();

            Delorean delorean = new Delorean();

            //automobil.AlleTuerenBenutzen();
            //Console.WriteLine();
            //
            //f40.AlleTuerenBenutzen();
            //Console.WriteLine();
            //
            //delorian.AlleTuerenBenutzen();
            //
            //Console.WriteLine();
            //Console.WriteLine();
            //
            //MeinAuto.gibTuerenStatusAus();
            //Console.WriteLine();
            //MeinAuto.AlleTuerenBenutzen();
            //MeinAuto.gibTuerenStatusAus();
            //Console.WriteLine();
            ////MeinAuto.EinzelneTuerBenutzen(3);
            //MeinAuto.EinzelneTuerBenutzen(automobil.BenutzerAuswahl());

            meinAuto.TuerenAusgeben();
            meinAuto.TuerenBenutzen(EnumAutotuer.VornLinks);
            meinAuto.TuerenAusgeben();
            meinAuto.TuerenBenutzen(EnumAutotuer.VornLinks);
            meinAuto.TuerenAusgeben();

            Console.ReadLine();
        }
    }
}
