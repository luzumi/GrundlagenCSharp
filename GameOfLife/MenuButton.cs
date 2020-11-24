using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class MenuButton
    {
        public menuButtonNames MenuButtonNames;
        public string MenueText { get; }       
        
        public MenuButton(string pMenueText)
        {
            MenueText = pMenueText;
        }
    }
    
}
