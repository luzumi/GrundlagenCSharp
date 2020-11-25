using System;


namespace GameOfLife
{
    class Button : IDrawable
    {
        readonly string buttonText;
        readonly byte posX = 2;
        readonly byte posY;
        ConsoleColor colorSelected;
        ConsoleColor colorUnSelected;
        ConsoleColor colorActive;
        ConsoleColor colorInactive;
        ConsoleColor living;
        ConsoleColor dead;
        ConsoleColor markAndLiving;
        ConsoleColor markAndDead;
        ConsoleColor currentForeground = ConsoleColor.DarkYellow;
        ConsoleColor currentBackground;
        readonly bool center;
        private readonly Action method;

        private ButtonStates states;

        public ButtonStates State
        {
            get { return states; }
            set
            {
                states = value;
                switch (states)
                {
                    case ButtonStates.Selected:
                        currentBackground = colorSelected;
                        currentForeground = colorActive;
                        break;
                    case ButtonStates.Available:
                        currentBackground = colorUnSelected;
                        currentForeground = colorActive;
                        break;
                    case ButtonStates.Inactive:
                        currentBackground = colorUnSelected;
                        currentForeground = colorInactive;
                        break;
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
            Console.SetCursorPosition((center? Console.WindowWidth / 2 - buttonText.Length / 2: posX), 2 + posY);
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
            Console.Write("{0}", buttonText);
        }


        public Button(byte pRow, bool pCentered, string pButtonText)
        {
            posY = pRow;
            buttonText = pButtonText;
            SetColors();
            center = pCentered;
            states = ButtonStates.Available;

        }


        public Button(byte pRow, byte pColumn, bool pCentered, string pButtonText)
        {
            posX = pColumn;
            posY = pRow;
            buttonText = pButtonText;
            SetColors();
            center = pCentered;
            states = ButtonStates.Available;
        }


        public Button(byte pRow, bool pCentered, string pButtonText, Action pMethodToExecute)
        {
            posY = pRow;
            buttonText = pButtonText;
            SetColors();
            center = pCentered;
            states = ButtonStates.Available;
            method = pMethodToExecute;
        }


        private void SetColors()
        {
            colorSelected = ConsoleColor.Gray;
            colorUnSelected = ConsoleColor.Blue;
            colorActive = ConsoleColor.Green;
            colorInactive = ConsoleColor.DarkGreen;
            living = ConsoleColor.Yellow;
            dead = ConsoleColor.DarkRed;
            markAndLiving = ConsoleColor.DarkYellow;
            markAndDead = ConsoleColor.DarkGray;
        }


        public void Execute()
        {
            method();
        }
    }
}
