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
            _fieldButtons = new Button[GameLogic.size.x, GameLogic.size.y];
            _needsRedraw = new List<IDrawable>();

            for (int row = 0; row < GameLogic.size.y; row++)
            {
                for (int column = 0; column < GameLogic.size.x; column++)
                {
                    _fieldButtons[column, row] =
                        new Button((byte)column, (byte)row, false, " ") {State = ButtonStates.Dead};
                    _needsRedraw.Add(_fieldButtons[column, row]);
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
                    if (_selectedField.y > 0)
                    {
                        FieldDeMarkOld();
                        _selectedField.y--;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (_selectedField.y < GameLogic.size.y - 1)
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
                    if (_selectedField.x < GameLogic.size.x - 1)
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
            _fieldButtons[_selectedField.x, _selectedField.y].State =
                _fieldButtons[_selectedField.x, _selectedField.y].State switch
                {
                    ButtonStates.Living => _fieldButtons[_selectedField.x, _selectedField.y].State =
                        ButtonStates.Dead,
                    ButtonStates.Dead => _fieldButtons[_selectedField.x, _selectedField.y].State =
                        ButtonStates.Living,
                    ButtonStates.MarkAndLiving => _fieldButtons[_selectedField.x, _selectedField.y].State =
                        ButtonStates.MarkAndDead,
                    ButtonStates.MarkAndDead => _fieldButtons[_selectedField.x, _selectedField.y].State =
                        ButtonStates.MarkAndLiving,
                    _ => _fieldButtons[_selectedField.x, _selectedField.y].State =
                        ButtonStates.Hidden
                };
            _needsRedraw.Add(_fieldButtons[_selectedField.x, _selectedField.y]);
            _saveGameLogic.FieldFalse[_selectedField.x, _selectedField.y] =
                !_saveGameLogic.FieldFalse[_selectedField.x, _selectedField.y];
        }

        /// <summary>
        /// Setzt Ausgangsfeld auf unmarkierten Zustand zurück
        /// </summary>
        private void FieldDeMarkOld()
        {
            if (_fieldButtons[_selectedField.x, _selectedField.y].State == ButtonStates.MarkAndLiving)
            {
                _fieldButtons[_selectedField.x, _selectedField.y].State = ButtonStates.Living;
            }
            else if (_fieldButtons[_selectedField.x, _selectedField.y].State == ButtonStates.MarkAndDead)
            {
                _fieldButtons[_selectedField.x, _selectedField.y].State = ButtonStates.Dead;
            }

            _needsRedraw.Add(_fieldButtons[_selectedField.x, _selectedField.y]);
        }

        /// <summary>
        /// Setzt gewähltes Feld auf markierten Zustand
        /// </summary>
        private void FieldMark()
        {
            if (_fieldButtons[_selectedField.x, _selectedField.y].State == ButtonStates.Living)
            {
                _fieldButtons[_selectedField.x, _selectedField.y].State = ButtonStates.MarkAndLiving;
            }
            else if (_fieldButtons[_selectedField.x, _selectedField.y].State == ButtonStates.Dead)
            {
                _fieldButtons[_selectedField.x, _selectedField.y].State = ButtonStates.MarkAndDead;
            }

            _needsRedraw.Add(_fieldButtons[_selectedField.x, _selectedField.y]);
        }
    }
}
