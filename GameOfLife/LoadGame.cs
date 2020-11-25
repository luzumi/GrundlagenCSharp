using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class LoadGame : Scene
    {

        public LoadGame()
        {
            string[] fileNames = Directory.GetFiles(@".\", "*.xml");
            buttons = new List<Button>();
            byte row = 4;

            foreach (var fileName in fileNames)
            {
                buttons.Add(new Button(row += 2, false, fileName, () => { Program.SceneRemove(); Program.SceneAdd(new Game(fileName)); }));
            }

            buttons.Add(new Button(row += 2, false, "Back", () => Program.SceneRemove()));
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        Program.SceneRemove();
                        break;
                    case ConsoleKey.Enter:
                        buttons[ActiveButtonID].Execute();
                        break;
                    case ConsoleKey.UpArrow:
                        ActiveButtonID--;
                        break;
                    case ConsoleKey.DownArrow:
                        ActiveButtonID++;
                        break;
                }
            }
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            Program.NeedsRedraw.AddRange(buttons);
        }
    }
}
