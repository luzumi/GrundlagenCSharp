using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace GameOfLife
{
    class LoadGame : Scene
    {
        List<string> fileNames = new List<string>();

        //GameScene preview; //TODO load file for preview, just constructor, no update or activate
        public LoadGame()
        {
            var fileNames = GameOfLifeLogic.GameLogic.GetAvailableGames();

            byte row = 4;
            uiElements = new List<UiElement>();

            foreach (var item in fileNames)
            {
                if (item.FromDatabase)
                {
                    uiElements.Add(new Button(row += 2, false, "DB " + item.Name, () => startLevel(item.Name)));
                }
                else
                {
                    uiElements.Add(new Button(row += 2, false, "FL " + item.Name.Substring(2, item.Name.Length - 2 - 4), () => startLevel(item.Name)));
                }
            }

            uiElements.Add(new Button(row += 2, false, "Back", () => Program.SceneRemove()));
            activeButton = 0;
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


        private void startLevel(string pFileName)
        {
            Program.SceneAdd(new Game(pFileName));
        }
    }
}
