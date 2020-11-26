using System;

namespace GameOfLife
{
    class BoardLabel : IDrawable
    {
        readonly int row;
        readonly int col;
        public BoardLabel(int pRow, int pCol)
        {
            row = pRow;
            col = pCol;
        }

        private bool alive;

        public bool Alive
        {
            get { return alive; }
            set
            {
                if (alive != value)
                {
                    alive = value;
                    Program.NeedsRedraw.Add(this);
                }
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = (alive ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed);
            Console.Write("  ");
        }
    }
}
