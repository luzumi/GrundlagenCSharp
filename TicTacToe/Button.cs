using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe
{
    class Button
    {
        public Point Position { get; set; }
        public FieldState Field;

        public Button( Point position )
        {
            this.Position = position;
        }
    }
}
