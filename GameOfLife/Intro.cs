using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    class Intro : Scene
    {
        bool needsRedraw = true;
        private List<string> LogoLines { get; }

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
                Console.SetCursorPosition(Console.WindowWidth / 2 - anyKey.Length / 2, 20 + row);
                Console.Write(anyKey);

                needsRedraw = false;
            }

            if (Console.KeyAvailable) // prüft nur ob eine taste gerade unten ist, diese wird nicht aus liste der gedrückten tasten entfernt.
            {
                Console.ReadKey(true); // ohne readkey bleibt die gedrückte taste erhalten und das Hauptmenu reagiert bereits darauf.
                Program.SceneRemove();
            }
        }


        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            needsRedraw = true;
        }
    }
}
