using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Editor : Scene
    {
        private GameLogic saveGameLogic;
        private List<FieldButton> fieldButtons;
        private (byte x, byte y) selectedField;

        public Editor()
        {
            saveGameLogic = new GameLogic(Intro.size);
            fieldButtons = new List<FieldButton>(Intro.size.x * Intro.size.y);

            for (int row = 0; row < Intro.size.y; row++)
            {
                for (int column = 0; column < Intro.size.x; column++)
                {
                    fieldButtons.Add(new FieldButton((column, row)));
                }
            }
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                GetInput(Console.ReadKey().Key);
            }
        }

        private void Draw()
        {
            Console.SetCursorPosition(0, 3);

            bool[,] arrayToDraw = saveGameLogic.Field;

            for (int row = 0; row < arrayToDraw.GetLength(1); row++)
            {
                for (int column = 0; column < arrayToDraw.GetLength(0); column++)
                {
                    Console.Write("{0}", (arrayToDraw[column, row] ? "▒" : " "));
                }

                Console.WriteLine();
            }
        }


        public void GetInput(ConsoleKey pConsoleKey)
        {
            switch (pConsoleKey)
            {
                case ConsoleKey.UpArrow:
                    if (selectedField.y > 0)
                    {
                        selectedField.y--;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (selectedField.y < (fieldButtons.Count / Intro.size.y) - 1)
                    {
                        selectedField.y++;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (selectedField.x < (fieldButtons.Count / Intro.size.x) - 1)
                    {
                        selectedField.x--;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (selectedField.x < (fieldButtons.Count / Intro.size.x) - 1)
                    {
                        selectedField.x++;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    break;
                case ConsoleKey.Enter:
                    saveGameLogic.Field[selectedField.x, selectedField.y] =
                        !saveGameLogic.Field[selectedField.x, selectedField.y];
                    Draw();
                    break;
                case ConsoleKey.S:
                    saveGameLogic.SaveGame("SaveGame");
                    Program.Scenes.Pop();
                    Program.Scenes.Push(new Game(saveGameLogic));
                    break;
                case ConsoleKey.Escape:
                    Program.Scenes.Pop();
                    break;
            }

            Console.ResetColor();
        }
    }
}
