using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GameOfLife
{
    class FieldButton
    {
        public menuButtonNames MenuButtonNames;
        public string MenueText { get; }       
        
        public FieldButton(string pMenueText)
        {
            MenueText = pMenueText;
        }
    }
}
