using System;

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

            bool Wahrheitswert; // true/false

            #endregion

            #region Gleitkommazahlen

            float Gleitkommazahl32Bit; // ca  6 Stellen nach Komma genau

            double Gleitkommazahl64Bit; // ca 15 Stellen nach Komma genau

            decimal Gleitkommazahl128Bit; // ca 30 Stellen nach Komma genau

            #endregion

            byte AktiveOption = (byte) (OptionenEnum.A | OptionenEnum.D); //Werte aus enum werden mathematisch ODER verknüpft = 1 | 8 = 9

            Console.WriteLine(AktiveOption);
            Console.ReadLine();
        }

        enum OptionenEnum
        {
            A = 1,      // Wert 1
            B = A * 2,  // Wert 2
            C = B * 2,  // Wert 4
            D = C * 2   // Wert 8
        }
    }
}