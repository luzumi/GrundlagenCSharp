using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class Editor : Scene
    {
        private readonly GameLogic _saveGameLogic;
        private readonly Button[,] _fieldButtons;
        private (byte x, byte y) _selectedField;
        readonly List<IDrawable> _needsRedraw;


        public Editor()
        {
            _saveGameLogic = new GameLogic(GameLogic.size);
            _fieldButtons = new Button[GameLogic.size.row, GameLogic.size.col];
            _needsRedraw = new List<IDrawable>();

            for (int row = 0; row < GameLogic.size.row; row++)
            {
                for (int col = 0; col < GameLogic.size.col; col++)
                {
                    _fieldButtons[row, col] =
                        new Button((byte)row, (byte)col, false, "  ") {State = ButtonStates.Dead};
                    _needsRedraw.Add(_fieldButtons[row, col]);
                }
            }
        }

        /// <summary>
        /// Zeichnet Spielfeld
        /// </summary>
        public override void Update()
        {
            if (_needsRedraw.Count > 0)
            {
                foreach (var item in _needsRedraw)
                {
                    item.Draw();
                }

                _needsRedraw.Clear();
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
            foreach (Button button in _fieldButtons)
            {
                _needsRedraw.Add(button);
            }
        }

        private void Draw()
        {
            Console.Write("  ");
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
                    if (_selectedField.y > 0)
                    {
                        FieldDeMarkOld();
                        _selectedField.y--;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (_selectedField.y < GameLogic.size.row - 1)
                    {
                        FieldDeMarkOld();
                        _selectedField.y++;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.LeftArrow:
                    if (_selectedField.x > 0)
                    {
                        FieldDeMarkOld();
                        _selectedField.x--;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.RightArrow:
                    if (_selectedField.x < GameLogic.size.col - 1)
                    {
                        FieldDeMarkOld();
                        _selectedField.x++;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.Enter:
                    SwitchThisField();
                    break;

                case ConsoleKey.S:
                    _saveGameLogic.SaveGame("SaveGame_.xml");
                    Program.Scenes.Pop();
                    Program.Scenes.Push(new Game(_saveGameLogic));
                    break;

                case ConsoleKey.Escape:
                    Program.Scenes.Pop();
                    break;
            }

            Console.ResetColor();
        }

        private void SwitchThisField()
        {
            _fieldButtons[_selectedField.y, _selectedField.x].State =
                _fieldButtons[_selectedField.y, _selectedField.x].State switch
                {
                    ButtonStates.Living => _fieldButtons[_selectedField.y, _selectedField.x].State =
                        ButtonStates.Dead,
                    ButtonStates.Dead => _fieldButtons[_selectedField.y, _selectedField.x].State =
                        ButtonStates.Living,
                    ButtonStates.MarkAndLiving => _fieldButtons[_selectedField.y, _selectedField.x].State =
                        ButtonStates.MarkAndDead,
                    ButtonStates.MarkAndDead => _fieldButtons[_selectedField.y, _selectedField.x].State =
                        ButtonStates.MarkAndLiving,
                    _ => _fieldButtons[_selectedField.y, _selectedField.x].State =
                        ButtonStates.Hidden
                };
            _needsRedraw.Add(_fieldButtons[_selectedField.y, _selectedField.x]);
            _saveGameLogic.FieldFalse[_selectedField.y, _selectedField.x] =
                !_saveGameLogic.FieldFalse[_selectedField.y, _selectedField.y];
        }

        /// <summary>
        /// Setzt Ausgangsfeld auf unmarkierten Zustand zurück
        /// </summary>
        private void FieldDeMarkOld()
        {
            if (_fieldButtons[_selectedField.y, _selectedField.x].State == ButtonStates.MarkAndLiving)
            {
                _fieldButtons[_selectedField.y, _selectedField.x].State = ButtonStates.Living;
            }
            else if (_fieldButtons[_selectedField.y, _selectedField.x].State == ButtonStates.MarkAndDead)
            {
                _fieldButtons[_selectedField.y, _selectedField.x].State = ButtonStates.Dead;
            }

            _needsRedraw.Add(_fieldButtons[_selectedField.y, _selectedField.x]);
        }

        /// <summary>
        /// Setzt gewähltes Feld auf markierten Zustand
        /// </summary>
        private void FieldMark()
        {
            if (_fieldButtons[_selectedField.y, _selectedField.x].State == ButtonStates.Living)
            {
                _fieldButtons[_selectedField.y, _selectedField.x].State = ButtonStates.MarkAndLiving;
            }
            else if (_fieldButtons[_selectedField.y, _selectedField.x].State == ButtonStates.Dead)
            {
                _fieldButtons[_selectedField.y, _selectedField.x].State = ButtonStates.MarkAndDead;
            }

            _needsRedraw.Add(_fieldButtons[_selectedField.y, _selectedField.x]);
        }
    }
}
