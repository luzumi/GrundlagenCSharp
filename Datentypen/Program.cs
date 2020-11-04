using System;
using System.Collections.Generic;

namespace Datentypen
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Ganzzahlen

            long Ganzzahl64Bit = long.MaxValue; // 18.446.744.073.709.551.615

            ulong Ganzzahl64BitPositiv = long.MaxValue; // 9.223.372.036.854.775.807

            int Ganzzahl32Bit = Int32.MaxValue; // 2.147.483.647

            uint Ganzzahl32BitPositiv = UInt32.MaxValue; // 4.294.967.295

            short Ganzzahl16Bit = short.MaxValue; //32767

            ushort ganzzahl16BitPositivUshort = ushort.MaxValue; // 65535

            sbyte Ganzzahl8Bit = sbyte.MaxValue; //-128 bis 127

            byte Ganzzahl8BitPositiv = byte.MaxValue; //0 bis 255

            bool Wahrheitswert; // true/false, als standard wird false gesetzt

            #endregion

            #region Gleitkommazahlen

            float Gleitkommazahl32Bit; // ca  6 Stellen nach Komma genau

            double Gleitkommazahl64Bit; // ca 15 Stellen nach Komma genau

            decimal Gleitkommazahl128Bit; // ca 30 Stellen nach Komma genau

            #endregion

            #region Listen, Arrays

            char Buchstabe = 'a'; //16Bit (UTF8)
            string Text = "Mein Text"; // Array aus Char mit verstecktem NULL am Ende

            byte[]
                ByteArray =
                    new byte[20]; //Belegt festen Speichebereich (hier mit 20 byte) ohne Inhalt, aufgerufen werden können die Felder einzeln mit [0] - [19]

            List<byte> listeBytes = new List<byte>();
                // List ist ein Container für generische Datentypen, der in den <> festgelegt wird. Sie beinhaltet dann ausschliesslig Objekte dieses Datentypes
                // Liste ist ein Array mit dynamischer Länge. Jedes Feld enthält neben dem Wert auch Informationen über den Speicherplatz anderer Felder aus dieser List
                // Fordert versteckt mehr als den benötigten Arbeitsspeicher an, damit weitere Elemente angehangen wertden können.
                // Wenn der Speicherplatz beim erweitern nicht ausreicht, wird auf die doppelte Speichergrösse erweitert.
                // Dann wird der Inhalt in den neuen Speicherbereich kopiert und der alte Ram wird freigegeben
                // MIt List<byte> listeBytes = new List<byte>(1024); wird die Anfangsgröße festgelegt. Das vermeidet das langsame erweitern in neue Speicherbereiche
                //
                // Console.WriteLine(listeBytes.Count); gibt Anzahl der Elemente aus
                // Console.WriteLine(listeBytes.Capacity); gibt Größe der Liste aus

            #endregion


            byte AktiveOption =
                (byte) (OptionenEnum.A |
                        OptionenEnum.D); //Werte aus enum werden mathematisch ODER verknüpft = 1 | 8 = 9

            Console.WriteLine(AktiveOption);
            Console.ReadLine();
        }

        enum OptionenEnum
        {
            A = 1, // Wert 1
            B = A * 2, // Wert 2
            C = B * 2, // Wert 4
            D = C * 2 // Wert 8
        }
    }
}