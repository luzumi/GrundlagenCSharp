using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    abstract class Scene
    {
        protected List<UiElement> uiElements;

        protected sbyte activeButton;
        protected int offset = Console.WindowWidth / 2 - GameLogic.size.col;


        /// <summary>
        /// Zählt ausgewählten Button mit und setzt den jeweils aktuellen auf Selected
        /// </summary>
        public virtual sbyte ActiveButtonID
        {
            get { return activeButton; }
            set
            {
                if (activeButton != value)
                {
                    uiElements[activeButton].State = ButtonStates.Available;
                    Program.NeedsRedraw.Add(uiElements[activeButton]);
                }

                activeButton = value;
                // TODO: replace with search for next active button
                if (activeButton < 0)
                {
                    activeButton = (sbyte)(uiElements.Count - 1);
                }
                else if (activeButton == uiElements.Count)
                {
                    activeButton = 0;
                }

                uiElements[activeButton].State = ButtonStates.Selected;
                Program.NeedsRedraw.Add(uiElements[activeButton]);
            }
        }

        /// <summary>
        /// definiert eine aktuellisierte Scene
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// setzt Einstellungen für eine neu aktivierte Scene
        /// </summary>
        public abstract void Activate();
    }
}
