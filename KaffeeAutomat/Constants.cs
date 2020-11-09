using System;
using System.Collections.Generic;
using System.Text;

namespace KaffeeAutomat
{
    public static class Constants
    {
        private static int waste = 0;
        public  static int Water = 1;
        public  static int Coffee = 2;
        private static int milk = 3;
        private static int cacao = 4;
        private static int tea = 5;
        private static int sugar = 6;
        private static int maintenance = 9;
        private static int stop = 0;
                
        public  static int Waste { get => waste; set => waste = value; }
        public  static int Milk { get => milk; set => milk = value; }
        public  static int Cacao { get => cacao; set => cacao = value; }
        public  static int Tea { get => tea; set => tea = value; }
        public  static int Sugar { get => sugar; set => sugar = value; }
        public static int StopProgramm { get => stop; set => stop = value; }
        public static int Maintenance { get => maintenance; set => maintenance = value; }
    }
}
