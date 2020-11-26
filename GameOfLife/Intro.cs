using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    class Intro : Scene
    {
        bool needsRedraw = true;
        private List<string> LogoLines { get; set; }

        public Intro()
        {

            LogoLines = new List<string>();
            using (StreamReader reader = new StreamReader("LogoSmall.txt"))
            {
                string newLine;
                while ((newLine = reader.ReadLine()) != null)
                {
                    LogoLines.Add(newLine);
                }
            }
        }


        public override void Update()
        {
            if (needsRedraw)
            {
                int row = 0;
                for (; row < LogoLines.Count; row++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - LogoLines[row].Length / 2, 2 + row);
                    Console.Write(LogoLines[row]);
                }

                string anyKey = "< press any key >";
                row += 2;
                Console.SetCursorPosition(Console.WindowWidth / 2 - anyKey.Length / 2, 2 + row);
                Console.Write(anyKey);

                needsRedraw = false;
            }

            if (Console.KeyAvailable) // prüft nur ob eine taste gerade unten ist, diese wird nicht aus liste der gedrückten tasten entfernt.
            {
                Console.ReadKey(true); // ohne readkey bleibt die gedrückte taste erhalten und das Hauptmenu reagiert bereits darauf.
                Program.SceneRemove();
                Program.SceneAdd(new MainMenue());
            }
        }


        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            needsRedraw = true;
        }
       
        
        //public override void Update()
        //{
        //    Console.ForegroundColor = ConsoleColor.DarkYellow;
        //    Console.CursorVisible = false;

        //    LogoLines = new List<string>();
        //    LogoLines = PrintLogo();

        //    Console.SetCursorPosition(33, 28);
        //    Console.WriteLine("Press 'ESC' to abort the Program");

        //    Console.SetCursorPosition(27, 15);
        //    Console.WriteLine("Wie Gross soll das Spielfeld denn sein?");

        //    Console.SetCursorPosition(GameLogic.size.x == 0 ? 31 : 51,  17);
        //    Console.Write(GameLogic.size.x == 0 ? "Horizontal: " : "Vertikal: ");

        //    //ArrayGröße abfragen
        //    if (Console.KeyAvailable && GameLogic.size.y == 0)
        //    {
        //        if (GameLogic.size.x == 0)
        //        {
        //            Int32.TryParse(Console.ReadLine(), out int resultx);
        //            {
        //                GameLogic.size.x = resultx;
        //            }
        //        }
        //        else
        //        {
        //            Int32.TryParse(Console.ReadLine(), out int resulty);
        //            {
        //                GameLogic.size.y = resulty;

        //                Console.SetCursorPosition(37, 23);
        //                Console.WriteLine("Press [Enter] to Continue");
        //            }
        //        }
        //    }

        //    if (Console.KeyAvailable && GameLogic.size.y != 0)
        //    {
        //        if (Console.ReadKey().Key == ConsoleKey.Enter)
        //        {
        //            Program.SceneRemove();
        //            Program.SceneAdd(new MainMenue());
        //        }
        //    }
        //    //TODO: animations

        //}
    }
}
