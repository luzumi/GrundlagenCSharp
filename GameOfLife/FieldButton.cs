using System;


namespace GameOfLife
{
    class FieldButton : IDrawable
    {
        readonly ConsoleColor colorSelected;
        readonly ConsoleColor colorUnSelected;
        readonly ConsoleColor markAndSelected;
        readonly ConsoleColor markAndDead;
        ConsoleColor currentForeground;
        ConsoleColor currentBackground;
        private ButtonStates states;
        protected (int column, int row) mark;

        public FieldButton((int column, int row) pMark)
        {
            mark = pMark;
            colorSelected = ConsoleColor.DarkGray;
            colorUnSelected = ConsoleColor.Black;
            markAndSelected = ConsoleColor.Yellow;
            markAndDead = ConsoleColor.DarkYellow;
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
                        currentBackground = colorSelected;
                        break;
                    case ButtonStates.Living:
                        currentBackground = colorUnSelected;
                        break;
                    case ButtonStates.MarkAndLiving:
                        currentBackground = markAndSelected;
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
