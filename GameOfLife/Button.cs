using System;
using System.Runtime.CompilerServices;


namespace GameOfLife
{
    class Button : UiElement
    {
        readonly string buttonText;
        readonly byte col = 2;
        
        ConsoleColor living;
        ConsoleColor dead;
        ConsoleColor markAndLiving;
        ConsoleColor markAndDead;
        private readonly Action method;


        public Button(byte pRow, bool pCentered, string pButtonText) : base(pRow, pCentered)
        {
            buttonText = pButtonText;
            SetColors();
            center = pCentered;
            states = ButtonStates.Available;
        }

        
        public Button(byte pRow, byte pColumn, bool pCentered, string pButtonText) : base(pRow, pCentered)
        {
            col = pColumn;
            row = pRow;
            buttonText = pButtonText;
            SetColors();
            center = pCentered;
            states = ButtonStates.Available;
        }


        public Button(byte pRow, bool pCentered, string pButtonText, Action pMethodToExecute) : base(pRow, pCentered)
        {
            buttonText = pButtonText;
            SetColors();
            states = ButtonStates.Available;
            method = pMethodToExecute;

            OnStateChanged = StateChanged;

            if (pMethodToExecute == null)
            {
                State = ButtonStates.Inactive;
            }
            else
            {
                State = ButtonStates.Available;
                method = pMethodToExecute;
            }
        }

        protected override void SetColors()
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
        


        public override void ProcessKey(ConsoleKeyInfo KeyInfo)
        {
            if (KeyInfo.Key == ConsoleKey.Enter)
            {
                if (State is ButtonStates.Available or 
                    ButtonStates.Selected or 
                    ButtonStates.MarkAndLiving or 
                    ButtonStates.MarkAndDead
                    && method != null)
                {
                    method();
                }
            }
        }


        public override void StateChanged()
        {
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


        public override void Draw()
        {
            Console.SetCursorPosition(center ? Console.WindowWidth / 2 - buttonText.Length / 2 : col, row);
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
            Console.Write(buttonText);
        }
    }
}
