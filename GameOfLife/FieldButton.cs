using System;


namespace GameOfLife
{
    class FieldButton : IDrawable
    {
        readonly ConsoleColor living;
        readonly ConsoleColor dead;
        readonly ConsoleColor markAndLiving;
        readonly ConsoleColor markAndDead;
        ConsoleColor currentForeground;
        ConsoleColor currentBackground;
        private ButtonStates states;
        protected (int column, int row) mark;

        public FieldButton((int column, int row) pMark)
        {
            mark = pMark;
            living = ConsoleColor.Yellow;
            dead = ConsoleColor.Black;
            markAndLiving = ConsoleColor.DarkYellow;
            markAndDead = ConsoleColor.DarkGray;
        }


        public ButtonStates State
        {
            get { return states; }
            set
            {
                states = value;
                switch (states)
                {
                    case ButtonStates.Dead:
                        currentBackground = dead;
                        break;
                    case ButtonStates.Living:
                        currentBackground = living;
                        break;
                    case ButtonStates.MarkAndLiving:
                        currentBackground = markAndLiving;
                        break;
                    case ButtonStates.MarkAndDead:
                        currentBackground = markAndDead;
                        break;
                }
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(mark.column, mark.row);
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
            Console.Write(" ");
        }
    }
}
