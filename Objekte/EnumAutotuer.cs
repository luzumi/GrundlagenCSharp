using System;
using System.Collections.Generic;
using System.Text;

namespace Objekte
{
    enum EnumAutotuer
    {
        VornLinks = 0b_0000_0001,
        VornRechts = 0b_0000_0010,
        HintenLinks = 0b_0000_0100,
        HintenRechts = 0b_0000_1000,
        KofferRaum = 0b_0001_0000,
        MotorHaube = 0b_0010_0000,
        SchiebeDach = 0b_0100_0000,
        TankDeckel = 0b_1000_0000

    }
}
