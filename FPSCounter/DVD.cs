using System;
using System.Drawing;

namespace FPSCounter
{
    class DVD
    {   Point position;
        Random rndGen = new Random();
        private int timer;

        public DVD()
        {
            position.X = rndGen.Next(80);
            position.Y = rndGen.Next(20);
            timer = rndGen.Next(80);
        }
        string[] logo = new string[] {  @" *******   **      ** *******  ",
                                        @"/**////** /**     /**/**////** ",
                                        @"/**    /**/**     /**/**    /**",
                                        @"/**    /**//**    ** /**    /**",
                                        @"/**    /** //**  **  /**    /**",
                                        @"/**    **   //****   /**    ** ",
                                        @"/*******     //**    /*******  ",
                                        @"///////       //     ///////   "};
        string[] clear = new string[] {  "                               ", 
                                         "                               ", 
                                         "                               ", 
                                         "                               ", 
                                         "                               ", 
                                         "                               ", 
                                         "                               ", 
                                         "                               " };


        
        (bool X, bool Y) direction;
        DateTime lastPositionUpdate;
        
        ConsoleColor color;

        public void Draw()
        {

            if ((DateTime.Now - lastPositionUpdate).TotalMilliseconds > timer)
            {

                // altes logo mit leerzeichen überschreiben
                for (int row = 0; row < logo.Length; row++)
                {
                    for (int col = 0; col < logo[row].Length; col++)
                    {
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Char.UnicodeChar = ' ';
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Attributes =0;

                    }
                }

                // testen ob eine richtungsänderung nötig ist
                var directionOld = direction;
                if (position.X + logo[0].Length >= Program.Widht) direction.X = false;
                if (position.Y + logo.Length >= Program.Height) direction.Y = false;
                if (position.X < 1) direction.X = true;
                if (position.Y < 1) direction.Y = true;

                // wenn eine richtung geändert wurde auch eine neue Farbe wählen
                if (directionOld.X != direction.X || directionOld.Y != direction.Y)
                {
                    color = (ConsoleColor)rndGen.Next(1,16);
                    timer = rndGen.Next(15,80);
                }

                // neue Position errechnen anhand der richtung
                position.X = (direction.X ? position.X + 1 : position.X - 1);
                position.Y = (direction.Y ? position.Y + 1 : position.Y - 1);

                // zeit aktualisieren damit das interval neu vergehen muss
                lastPositionUpdate = DateTime.Now;

                // logo mit neuer farbe an neue position zeichnen
                //Console.ForegroundColor = color;
                for (int row = 0; row < logo.Length; row++)
                {
                    for (int col = 0; col < logo[row].Length; col++)
                    {
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Char.UnicodeChar = logo[row][col];
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Attributes = (short)color;

                    }
                }
            }

        }
    }
}
