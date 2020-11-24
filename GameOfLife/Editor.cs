using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace GameOfLife
{
    class Editor : Scene
    {
        private GameLogic saveGameLogic;
        private FieldButton[,] fieldButtons;
        private (byte x, byte y) selectedField;
        readonly List<IDrawable> needsRedraw;


        public Editor()
        {
            saveGameLogic = new GameLogic(GameLogic.size);
            fieldButtons = new FieldButton[GameLogic.size.x, GameLogic.size.y];
            needsRedraw = new List<IDrawable>();

            for (int row = 0; row < GameLogic.size.y; row++)
            {
                for (int column = 0; column < GameLogic.size.x; column++)
                {
                    fieldButtons[column, row] = new FieldButton((column, row));
                    fieldButtons[column, row].State = ButtonStates.Dead;
                }
            }
        }

        /// <summary>
        /// Zeichnet Spielfeld
        /// </summary>
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
                GetInput(Console.ReadKey(true).Key);
            }
        }

        public override void Activate()
        {
            Console.ResetColor();
            Console.Clear();
            foreach (FieldButton button in fieldButtons)
            {
                needsRedraw.Add(button);
            }
        }

        private void Draw()
        {
            Console.Write(" ");
        }

        /// <summary>
        /// [Pfeiltasten], [S], [Enter]
        /// werden verabreitet
        /// </summary>
        /// <param name="pConsoleKey"></param>
        public void GetInput(ConsoleKey pConsoleKey)
        {
            switch (pConsoleKey)
            {
                case ConsoleKey.UpArrow:
                    if (selectedField.y > 0)
                    {
                        FieldDeMarkOld();
                        selectedField.y--;
                        FieldMark();
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedField.y < GameLogic.size.y - 1)
                    {
                        FieldDeMarkOld();
                        selectedField.y++;
                        FieldMark();
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (selectedField.x > 0)
                    {
                        FieldDeMarkOld();
                        selectedField.x--;
                        FieldMark();
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (selectedField.x < GameLogic.size.x - 1)
                    {
                        FieldDeMarkOld();
                        selectedField.x++;
                        FieldMark();
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
                                ButtonStates.MarkAndLiving,
                            _ => fieldButtons[selectedField.x, selectedField.y].State =
                                ButtonStates.Hidden
                        };
                    needsRedraw.Add(fieldButtons[selectedField.x, selectedField.y]);
                    saveGameLogic.FieldFalse[selectedField.x, selectedField.y] =
                        !saveGameLogic.FieldFalse[selectedField.x, selectedField.y];
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

        /// <summary>
        /// Setzt Ausgangsfeld auf unmarkierten Zustand zurück
        /// </summary>
        private void FieldDeMarkOld()
        {
            if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.MarkAndLiving)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.Living;
            }
            else if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.MarkAndDead)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.Dead;
            }
            needsRedraw.Add(fieldButtons[selectedField.x, selectedField.y]);
        }

        /// <summary>
        /// Setzt gewähltes Feld auf markierten Zustand
        /// </summary>
        private void FieldMark()
        {
            if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.Living)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.MarkAndLiving;
            }
            else if (fieldButtons[selectedField.x, selectedField.y].State == ButtonStates.Dead)
            {
                fieldButtons[selectedField.x, selectedField.y].State = ButtonStates.MarkAndDead;
            }

            needsRedraw.Add(fieldButtons[selectedField.x, selectedField.y]);
        }
    }
}
