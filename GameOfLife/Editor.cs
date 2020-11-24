using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Editor : Scene
    {
        private GameLogic saveGameLogic;
        private FieldButton[,] fieldButtons;
        private (byte x, byte y) selectedField;
        readonly List<IDrawable> needsRedraw;
        readonly List<FieldButton> inactiveButtons;
        private sbyte activeButton;

        public Editor()
        {
            saveGameLogic = new GameLogic(Intro.size);
            fieldButtons = new FieldButton[Intro.size.x, Intro.size.y];

            for (int row = 0; row < Intro.size.y; row++)
            {
                for (int column = 0; column < Intro.size.x; column++)
                {
                    fieldButtons[column, row] = new FieldButton((column, row));
                }
            }
        }

        public override void Update()
        {
            if (needsRedraw.Count > 0)
            {
                foreach (var item in needsRedraw)
                {
                    item.Draw();
                }
                needsRedraw.Clear(); 
            }

            if (Console.KeyAvailable)
            {
                GetInput(Console.ReadKey().Key);
            }
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            needsRedraw.AddRange(inactiveButtons);
            needsRedraw.AddRange(fieldButtons.Cast<FieldButton>().ToList());
        }

        private void Draw()
        {
            for (int row = 0; row < fieldButtons.GetLength(1); row++)
            {
                for (int column = 0; column < fieldButtons.GetLength(0); column++)
                {
                    if (saveGameLogic.Field[column, row])
                    {
                        fieldButtons[column, row].Draw();
                    }
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
                        MarkField();
                        selectedField.y--;
                        MarkField();
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (selectedField.y < (Intro.size.y) - 1)
                    {
                        MarkField();
                        selectedField.y++;
                        MarkField();
                    }

                    break;

                case ConsoleKey.LeftArrow:
                    if (selectedField.x < (Intro.size.x) - 1)
                    {
                        MarkField();
                        selectedField.x--;
                        MarkField();
                    }

                    break;

                case ConsoleKey.RightArrow:
                    if (selectedField.x < (Intro.size.x) - 1)
                    {
                        MarkField();
                        selectedField.x++;
                        MarkField();
                    }

                    break;

                case ConsoleKey.Enter:
                    fieldButtons[selectedField.x, selectedField.y].State =
                        fieldButtons[selectedField.x, selectedField.y].State switch
                        {
                            ButtonStates.Living => fieldButtons[selectedField.x, selectedField.y].State =
                                ButtonStates.Dead,
                            ButtonStates.Dead => fieldButtons[selectedField.x, selectedField.y].State =
                                ButtonStates.Living,
                            ButtonStates.MarkAndLiving => fieldButtons[selectedField.x, selectedField.y].State =
                                ButtonStates.MarkAndDead,
                            ButtonStates.MarkAndDead => fieldButtons[selectedField.x, selectedField.y].State =
                                ButtonStates.MarkAndLiving
                        };
                    needsRedraw.Add(fieldButtons[selectedField.x, selectedField.y]);
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

        private void MarkField()
        {
            if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.Living)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.MarkAndLiving;
            }
            else if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.Dead)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.MarkAndDead;
            }
            else if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.MarkAndLiving)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.Living;
            }
            else if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.MarkAndDead)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.Dead;
            }

            needsRedraw.Add(fieldButtons[selectedField.x, selectedField.y]);
        }
    }
}
