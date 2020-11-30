using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    abstract class UiElement : IDrawable
    {
        private protected int row;
        private protected bool center;
        protected Action OnStateChanged;
        protected ButtonStates states;
        protected ConsoleColor colorSelected;
        protected ConsoleColor colorUnSelected;
        protected ConsoleColor colorActive;
        protected ConsoleColor colorInactive;
        protected ConsoleColor currentForeground = ConsoleColor.DarkYellow;
        protected ConsoleColor currentBackground;
        public char[] content;
        protected Action method;

        
        public UiElement(int pRow, bool pCentered )
        {
            row = pRow;
            center = pCentered;
        }

        public override string ToString()
        {
            return new string(content).Trim();
        }

        public ButtonStates State
        {
            get { return states; }
            set 
                {
                states = value;
                OnStateChanged?.Invoke();
                }
        }


        protected virtual void SetColors()
        {
            colorSelected = ConsoleColor.Gray;
            colorUnSelected = ConsoleColor.Blue;
            colorActive = ConsoleColor.Green;
            colorInactive = ConsoleColor.DarkGreen;
        }

        public virtual void StateChanged()
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
            }
        }

        /// <summary>
        /// verarbteitet Usereingaben
        /// </summary>
        /// <param name="pKeyInfo">gedrückte Taste</param>
        public virtual void ProcessKey(ConsoleKeyInfo pKeyInfo) { }


        /// <summary>
        /// zeichnet aktuelle Scene
        /// </summary>
        public abstract void Draw();
    }
}
