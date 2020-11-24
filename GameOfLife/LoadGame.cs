using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameOfLife
{
    class LoadGame : Scene
    {
        private List<Button> buttons;

        public LoadGame()
        {
            string[] fileNames = Directory.GetFiles(@".\", "*.xml");
            buttons = new List<Button>();
            byte row = 4;
            foreach (var item in fileNames)
            {
                new Button(row, false, item, () =>
                {
                    Program.RemoveScene();
                    Program.AddScene(new Game(item));
                };
            }
        }

        public override void Update()
        {
            
        }

        public override void Activate()
        {
            Console.Clear();
            Console.ResetColor();
            Program.NeedsRedraw.AddRange(buttons);
        }
    }
}
