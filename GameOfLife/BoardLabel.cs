using System;

namespace GameOfLife
{
    class BoardLabel : IDrawable
    {
        readonly int Y;
        readonly int X;
        public BoardLabel(int pRow, int pCol)
        {
            Y = pRow;
            X = pCol;
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
            Console.SetCursorPosition((X % 2 == 0 ? X / 2 : X / 2 + 1), Y);
            Console.BackgroundColor = (alive ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed);
            Console.Write("  ");
        }
    }
}
