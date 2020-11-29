using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class LoadGame : Scene
    {
        List<string> fileNames = new List<string>();

        //GameScene preview; //TODO load file for preview, just constructor, no update or activate
        public LoadGame()
        {
            fileNames.AddRange(Directory.GetFiles(@".\", "*.xml"));
            fileNames.AddRange(Directory.GetFiles(@".\", "*.gol"));

            byte row = 4;
            //uiElements = new List<UiElement>();
            uiTextBoxes = new List<UiElement>();

            foreach (var item in fileNames)
            {
                //uiElements.Add(new Button(row += 2, false, "                                    ", 
                //   () => { Program.SceneRemove(); Program.SceneAdd(new Game(item)); }));

                uiTextBoxes.Add(new TextBox(row += 2, false, item.Substring(2, item.Length - 2)));
            }

            //uiElements.Add(new Button(row += 2, false, "", () => Program.SceneRemove()));
            uiTextBoxes.Add(new TextBox(row += 2, false, "Back"));
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            Program.NeedsRedraw.AddRange(uiTextBoxes);
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
                    case ConsoleKey.Enter:
                        Program.SceneAdd(new Game(fileNames[ActiveButtonID]));
                        break;
                    default:
                        uiTextBoxes[ActiveButtonID].ProcessKey(pressedKey);
                        break;
                }
            }
        }

        public override sbyte ActiveButtonID
        {
            get { return activeButton; }
            set
            {
                if (activeButton != value)
                {
                    uiTextBoxes[activeButton].State = ButtonStates.Available;
                    Program.NeedsRedraw.Add(uiTextBoxes[activeButton]);
                }

                activeButton = value;
                // TODO: replace with search for next active button
                if (activeButton < 0)
                {
                    activeButton = (sbyte)(uiTextBoxes.Count - 1);
                }
                else if (activeButton == uiTextBoxes.Count)
                {
                    activeButton = 0;
                }

                uiTextBoxes[activeButton].State = ButtonStates.Selected;
                Program.NeedsRedraw.Add(uiTextBoxes[activeButton]);
            }
        }
    }
}
