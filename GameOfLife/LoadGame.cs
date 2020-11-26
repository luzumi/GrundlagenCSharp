using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class LoadGame : Scene
    {

        public LoadGame()
        {
            string[] fileNames = Directory.GetFiles(@".\", "*.gol");
            uiElements = new List<UiElement>();
            byte row = 4;

            foreach (var fileName in fileNames)
            {
                uiElements.Add(new Button(row += 2, false, fileName, () => { Program.SceneRemove(); Program.SceneAdd(new Game(fileName)); }));
            }

            uiElements.Add(new Button(row += 2, false, "Back", () => Program.SceneRemove()));
        }

        public override void Update()
        {
            
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Program.SceneRemove();
                        break;
                    case ConsoleKey.Enter:
                        uiElements[ActiveButtonID].ProcessKey(key);
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
            Program.NeedsRedraw.AddRange(uiElements);
        }
    }
}
