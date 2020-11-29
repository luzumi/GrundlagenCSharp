using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class LoadGame : Scene
    {
        //GameScene preview; //TODO load file for preview, just constructor, no update or activate
        public LoadGame()
        {
            List<string> fileNames = new List<string>();
            fileNames.AddRange( Directory.GetFiles(@".\", "*.xml"));
            fileNames.AddRange( Directory.GetFiles(@".\", "*.gol"));
            
            byte row = 4;
            uiElements = new List<UiElement>();

            foreach (var item in fileNames)
            {
                uiElements.Add(new Button(row += 2, false, item.Substring(2,item.Length-2), () => { Program.SceneRemove(); Program.SceneAdd(new Game(item)); }));
            }

            uiElements.Add(new Button(row += 2, false, "Back", () => Program.SceneRemove()));
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            Program.NeedsRedraw.AddRange(uiElements);
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                var pressedKey = Console.ReadKey(true);
                switch (pressedKey.Key)
                {
                    case ConsoleKey.Escape:
                        Program.SceneRemove();
                        break;
                    case ConsoleKey.UpArrow:
                        ActiveButtonID--;
                        break;
                    case ConsoleKey.DownArrow:
                        ActiveButtonID++;
                        break;
                    default:
                        uiElements[ActiveButtonID].ProcessKey(pressedKey);
                        break;
                }
            }
        }
    }
}
