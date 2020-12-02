using System;
using System.Drawing;
using System.Text;
using System.Threading;

namespace FPSCounter
{
    class TextBox
    {
        private string[] content;

        public string Content
        {
            get { return content.ToString(); }
        }

        Point position;
        (bool X, bool Y) direction;
        DateTime lastPositionUpdate;
        Random rndGen = new Random();
        ConsoleColor color;
        private int timer = 30;



        public TextBox(Point Position, ConsoleColor TextColor)
        {
            position = Position;
            color = TextColor;
            timer = rndGen.Next(80);

            content = new string[]
            {
                @".s5SSSs.  .s    s.  .s5SSSs. ", @"SS    S%S SS    S%S SS    S%S",
                @"SS    S%S  SS   S%S SS    S%S", @"SS    ;,.   SS  ;,. SS    ;,.",
                @"sS    S%S sS    S%S sS    S%S", @"SS    S%S SS    S%S SS    S%S",
                @"SS    `:;  SS   `:; SS    `:;", @";;;;;;;:'    `:;;:' ;;;;;;;:'"
            };
            string[] clear = new string[]
            {
                "                                 ", "                                 ",
                "                                 ", "                                 ",
                "                                 ", "                                 ",
                "                                 ", "                                 "
            };

            
        }

        public void ProcessKey(ConsoleKey KeyInformation)
        {
            if (KeyInformation == ConsoleKey.DownArrow )
            {
                timer--;
            }
            if (KeyInformation == ConsoleKey.UpArrow )
            {
                timer++;
            }
        }

        public void Draw()
        {

            if ((DateTime.Now - lastPositionUpdate).TotalMilliseconds > timer)
            {

                // altes logo mit leerzeichen überschreiben
                for (int row = 0; row < content.Length; row++)
                {
                    for (int col = 0; col < content[row].Length; col++)
                    {
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Char.UnicodeChar = ' ';
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Attributes = 0;
                    }
                }

                var directionOld = direction;
                if (position.X + content[0].Length >= Program.Widht) direction.X = false;
                if (position.Y + content.Length >= Program.Height) direction.Y = false;
                if (position.X < 1) direction.X = true;
                if (position.Y < 1) direction.Y = true;

                // wenn eine richtung geändert wurde auch eine neue Farbe wählen
                if (directionOld.X != direction.X || directionOld.Y != direction.Y)
                {
                    color = (ConsoleColor)rndGen.Next(1,16);
                    timer = rndGen.Next(15,80) ;
                }

                // neue Position errechnen anhand der richtung
                ChangeDirection();

                // zeit aktualisieren damit das interval neu vergehen muss
                lastPositionUpdate = DateTime.Now;

                // logo mit neuer farbe an neue position zeichnen
                //Console.ForegroundColor = color;


                for (int row = 0; row < content.Length; row++)
                {
                    for (int col = 0; col < content[row].Length; col++)
                    {
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Char.UnicodeChar = content[row][col];
                        Program.ScreenBuffer[(position.Y + row) * Program.Widht + position.X + col].Attributes = (short)color;
                    }
                }
            }

        }

        private void ChangeDirection()
        {
            position.X = (direction.X ? position.X + 1 : position.X - 1);
            position.Y = (direction.Y ? position.Y + 1 : position.Y - 1);
        }
    }
}
