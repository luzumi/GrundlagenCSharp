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
