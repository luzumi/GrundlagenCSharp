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
        protected private int row;
        protected private bool center;
        protected Action OnStateChanged;
        protected ButtonStates states;
        
        public UiElement(int pRow, bool pCentered )
        {
            row = pRow;
            center = pCentered;
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

        public virtual void ProcessKey(ConsoleKeyInfo pKeyInfo) { }

        public abstract void Draw();
    }
}
