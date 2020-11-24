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
        public static List<SaveGame> saveGames;
        List<MenuButton> menuButtons;
        byte activeButtonID = 0;


        public LoadGame()
        {
            saveGames = new List<SaveGame>();
            menuButtons = new List<MenuButton>();
            for (int i = 0; i < menuButtons.Count; i++)
            {
                menuButtons.Add(new MenuButton(saveGames[i].fileName));
            }
        }

        public override void Update()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(Program.Logo());

            MainMenue.drawButtons(activeButtonID, menuButtons);
            if (Console.KeyAvailable) GetInput(Console.ReadKey(true).Key);
        }


        public void GetInput(ConsoleKey pConsoleKey)
        {
            switch (pConsoleKey)
            {
                case ConsoleKey.UpArrow:
                    if (activeButtonID > 0)
                        activeButtonID--;
                    break;
                case ConsoleKey.DownArrow:
                    if (activeButtonID < menuButtons.Count - 1)
                        activeButtonID++;
                    break;
                case ConsoleKey.Enter:
                    Program.Scenes.Pop();
                    Program.Scenes.Push(new Game(Load(menuButtons[activeButtonID].MenueText)));
                    break;
            }
        }


        public GameLogic Load(string pFileName)
        {
            GameLogic gl = new GameLogic(Intro.size);


            if (!File.Exists(pFileName))
            {
                return null;
            }

            XmlSerializer xml = new XmlSerializer(typeof(SaveGame));
            SaveGame sg;

            using (Stream file = new FileStream(pFileName, FileMode.Open, FileAccess.Read))
            {
                sg = (SaveGame)xml.Deserialize(file);
            }

            bool[,] converteField = new bool[sg.Field.Count, sg.Field[0].Count];

            for (int row = 0; row < converteField.GetLength(0); row++)
            {
                for (int column = 0; column < converteField.GetLength(1); column++)
                {
                    converteField[row, column] = sg.Field[row][column];
                }
            }

            gl.FieldFalse = converteField;

            return gl;
        }
    }
}
