using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    abstract class Scene
    {
        protected List<UiElement> uiElements;
        protected List<UiElement> uiTextBoxes;

        protected sbyte activeButton;
        protected int offset = Console.WindowWidth / 2 - GameLogic.size.col;



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

        public abstract void Update();

        public abstract void Activate();
    }
}
