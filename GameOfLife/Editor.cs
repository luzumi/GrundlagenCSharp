using System;
using GameOfLifeLogic;

namespace GameOfLife
{
    class Editor : Scene
    {
        private UiElement[,] _fieldButton;
        private (byte x, byte y) _selectedField;


        public Editor()
        {
            _saveGameLogic = new GameLogic(0);
            _fieldButton = new Button[GameLogic.size.row, GameLogic.size.col];
            

            for (int row = 0; row < GameLogic.size.row; row++)
            {
                for (int col = 0; col < GameLogic.size.col; col++)
                {
                    _fieldButton[row, col] =
                        new Button((byte)row, (byte)(offset + col), false, "  ") {State = ButtonStates.Dead};
                    Program.NeedsRedraw.Add(_fieldButton[row, col]);
                }
            }
        }

        /// <summary>
        /// Zeichnet Spielfeld
        /// </summary>
        public override void Update()
        {
            if (Program.NeedsRedraw.Count > 0)
            {
                foreach (var item in Program.NeedsRedraw)
                {
                    item.Draw();
                }

                Program.NeedsRedraw.Clear();
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
            foreach (Button button in _fieldButton)
            {
                button.StateChanged();
                Program.NeedsRedraw.Add(button);
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
                    if (_selectedField.y < GameLogic.size.row - 2)
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
                        _selectedField.x-=2;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.RightArrow:
                    if (_selectedField.x < GameLogic.size.col - 2)
                    {
                        FieldDeMarkOld();
                        _selectedField.x+=2;
                        FieldMark();
                    }

                    break;

                case ConsoleKey.Enter:
                    SwitchThisField();
                    break;

                case ConsoleKey.S:
                    _saveGameLogic.SaveGameTxt("SaveGame_"+DateTime.Now.ToString("hh-mm-ss"));
                    Program.SceneRemove();
                    Program.SceneAdd(new Game(_saveGameLogic));
                    break;

                case ConsoleKey.Escape:
                    Program.SceneRemove();
                    break;
            }

            Console.ResetColor();
        }

        private void SwitchThisField()
        {
            _fieldButton[_selectedField.y, _selectedField.x].State =
                _fieldButton[_selectedField.y, _selectedField.x].State switch
                {
                    ButtonStates.Living => _fieldButton[_selectedField.y, _selectedField.x].State =
                        ButtonStates.Dead,
                    ButtonStates.Dead => _fieldButton[_selectedField.y, _selectedField.x].State =
                        ButtonStates.Living,
                    ButtonStates.MarkAndLiving => _fieldButton[_selectedField.y, _selectedField.x].State =
                        ButtonStates.MarkAndDead,
                    ButtonStates.MarkAndDead => _fieldButton[_selectedField.y, _selectedField.x].State =
                        ButtonStates.MarkAndLiving,
                    _ => _fieldButton[_selectedField.y, _selectedField.x].State =
                        ButtonStates.Hidden
                };
            Program.NeedsRedraw.Add(_fieldButton[_selectedField.y, _selectedField.x]);
            _saveGameLogic.FieldFalse[_selectedField.y, _selectedField.x] =
                !_saveGameLogic.FieldFalse[_selectedField.y, _selectedField.y];
        }

        /// <summary>
        /// Setzt Ausgangsfeld auf unmarkierten Zustand zurück
        /// </summary>
        private void FieldDeMarkOld()
        {
            if (_fieldButton[_selectedField.y, _selectedField.x].State == ButtonStates.MarkAndLiving)
            {
                _fieldButton[_selectedField.y, _selectedField.x].State = ButtonStates.Living;
            }
            else if (_fieldButton[_selectedField.y, _selectedField.x].State == ButtonStates.MarkAndDead)
            {
                _fieldButton[_selectedField.y, _selectedField.x].State = ButtonStates.Dead;
            }
            _fieldButton[_selectedField.y, _selectedField.x].StateChanged();

            Program.NeedsRedraw.Add(_fieldButton[_selectedField.y, _selectedField.x]);
        }

        /// <summary>
        /// Setzt gewähltes Feld auf markierten Zustand
        /// </summary>
        private void FieldMark()
        {
            if (_fieldButton[_selectedField.y, _selectedField.x].State == ButtonStates.Living)
            {
                _fieldButton[_selectedField.y, _selectedField.x].State = ButtonStates.MarkAndLiving;
            }
            else if (_fieldButton[_selectedField.y, _selectedField.x].State == ButtonStates.Dead)
            {
                _fieldButton[_selectedField.y, _selectedField.x].State = ButtonStates.MarkAndDead;
            }
            _fieldButton[_selectedField.y, _selectedField.x].StateChanged();


            Program.NeedsRedraw.Add(_fieldButton[_selectedField.y, _selectedField.x]);
        }
    }
}
